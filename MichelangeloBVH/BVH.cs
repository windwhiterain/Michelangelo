using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Michelangelo.Math;
using static Michelangelo.Math.General;
namespace Michelangelo.SpatialAccessor;

/// <summary>
/// This is a port of Erin Catto/box2d's b2_dynamic_tree from C++ to C# (https://box2d.org/files/ErinCatto_DynamicBVH_GDC2019.pdf), intended for use in Unity. 
/// It's a very faithful translation with a couple of additional C# creature comforts for usability.
/// 
/// A bounding volume hierarchy is a sorted hierarchy of axis aligned bounding volumes (AABBs) in which closer AABBs are closer on the tree.
/// They are a common type of spatial optimization, often used to accelerate things like raycasts and physics queries.
/// </summary>
public class BVH<T> where T : BVH<T>.Client
{
    public abstract record class Client
    {
        Float3? previousPosition;
        public abstract AxisBox3D Box { get; }
        protected abstract Float3 Position { get; }
        public Float3 Displacement
        {
            get
            {
                if (previousPosition.HasValue)
                {
                    var position = Position;
                    var ret = Position - previousPosition.Value;
                    previousPosition = position;
                    return ret;
                }
                else
                {
                    previousPosition = Position;
                    return new();
                }
            }
        }
    }
    #region Consts

    private const int NULL_NODE = -1;
    private const float AABB_EXTENSION = 0.1f;
    private const float AABB_MULTIPLIER = 2f;
    private const int DEFAULT_CAPACITY = 16;

    #endregion

    #region Fields

    private int m_root = NULL_NODE;
    public int RootID => m_root;

    public bool RootIsNull => m_root == NULL_NODE;

    private Node[] m_nodes;
    private int m_nodeCount;
    private int m_nodeCapacity;

    private int m_freeList;

#pragma warning disable IDE0052 // Remove unread private members
    private int m_insertionCount = 0;
#pragma warning restore IDE0052 // Remove unread private members

    private readonly Stack<int> m_growableStack = new Stack<int>(256);
    private Dictionary<T, int> m_keys;

    #endregion

    #region Constructor

    public BVH() : this(DEFAULT_CAPACITY) { }

    public BVH(int capacity)
    {
        m_root = NULL_NODE;
        m_nodeCapacity = capacity;
        m_nodeCount = 0;

        m_keys = new Dictionary<T, int>(m_nodeCapacity);
        m_nodes = new Node[m_nodeCapacity];

        // Build a linked list for the free list.
        for (int i = 0; i < m_nodeCapacity - 1; i++)
        {
            m_nodes[i].Next = i + 1;
            m_nodes[i].Height = -1;
        }

        m_nodes[m_nodeCapacity - 1].Next = NULL_NODE;
        m_nodes[m_nodeCapacity - 1].Height = -1;
        m_freeList = 0;

        m_insertionCount = 0;
    }

    #endregion

    #region Original Public Methods

    /// <summary>
    /// Add the given object to the tree, and return a unique key which can be used to refer to the object again later.
    /// </summary>
    public int Add(T clientObject)
    {
        int key = AllocateNode();

        AxisBox3D newBounds = clientObject.Box;
        newBounds.Expand(AABB_EXTENSION);

        m_nodes[key].AABB = newBounds;
        m_nodes[key].Height = 0;
        m_nodes[key].Object = clientObject;

        InsertLeaf(key);

        m_keys.Add(clientObject, key);

        return key;
    }

    /// <summary>
    /// Destroy a proxy. This asserts if the id is invalid.
    /// </summary>
    public void Remove(T clientObject)
    {
        Debug.Assert(m_keys.TryGetValue(clientObject, out int key));
        Debug.Assert(0 <= key && key < m_nodeCapacity);
        Debug.Assert(m_nodes[key].IsLeaf);

        m_keys.Remove(clientObject);

        RemoveLeaf(key);
        FreeNode(key);
    }

    /// <summary>
    /// Destroy a proxy. This asserts if the id is invalid.
    /// </summary>
    public void Remove(int key)
    {
        Debug.Assert(0 <= key && key < m_nodeCapacity);
        Debug.Assert(m_nodes[key].IsLeaf);

        Node node = m_nodes[key];

        m_keys.Remove(node.Object);

        RemoveLeaf(key);
        FreeNode(key);
    }
    /// <summary>
    /// Move a proxy with a swept bounds. If the proxy has moved outside of its padded bounds,
    /// then the proxy is removed from the tree and re-inserted. Otherwise
    /// the function returns immediately.
    /// </summary>
    /// <returns>true if the object was re-inserted.</returns>
    public bool Update(T clientObject)
    {
        Debug.Assert(m_keys.TryGetValue(clientObject, out int key));
        Debug.Assert(0 <= key && key < m_nodeCapacity);
        Debug.Assert(m_nodes[key].IsLeaf);

        AxisBox3D aabb = clientObject.Box;
        Michelangelo.Math.Float3 displacement = clientObject.Displacement;

        // Extend AABB
        AxisBox3D fatAABB = aabb;
        fatAABB.Expand(AABB_EXTENSION);

        // Predict AABB movement
        Michelangelo.Math.Float3 d = AABB_MULTIPLIER * displacement;

        if (d.x < 0f)
            fatAABB.min += d.x * Float3.Basis(0,1);
        else
            fatAABB.max += d.x * Float3.Basis(0,1);

        if (d.y < 0f)
            fatAABB.min += d.y * Float3.Basis(1,1);
        else
            fatAABB.max += d.y * Float3.Basis(1,1);

        if (d.z < 0f)
            fatAABB.min += d.z * Float3.Basis(2,1);
        else
            fatAABB.max += d.z * Float3.Basis(2,1);

        AxisBox3D treeAABB = m_nodes[key].AABB;
        if (treeAABB.Contain(aabb))
        {
            Float3 r = AABB_EXTENSION * Float3.Diagonal(1);

            // The tree AABB still contains the object, but it might be too large.
            // Perhaps the object was moving fast but has since gone to sleep.
            // The huge AABB is larger than the new fat AABB.
            Float3 min = fatAABB.min - 4f * r;
            Float3 max = fatAABB.max + 4f * r;

            AxisBox3D hugeAABB = new AxisBox3D(min, max);

            if (hugeAABB.Contain(treeAABB))
            {
                // The tree AABB contains the object AABB and the tree AABB is
                // not too large. No tree update needed.
                return false;
            }

            // Otherwise the tree AABB is huge and needs to be shrunk
        }

        RemoveLeaf(key);

        m_nodes[key].AABB = fatAABB;

        InsertLeaf(key);

        return true;
    }

    /// <summary>
    /// Validate this tree. For testing.
    /// </summary>
    public void Validate()
    {
        ValidateStructure(m_root);
        ValidateMetrics(m_root);

        int freeCount = 0;
        int freeIndex = m_freeList;
        while (freeIndex != NULL_NODE)
        {
            Debug.Assert(0 <= freeIndex && freeIndex < m_nodeCapacity);
            freeIndex = m_nodes[freeIndex].Next;
            ++freeCount;
        }

        Debug.Assert(GetHeight() == ComputeHeight());

        Debug.Assert(m_nodeCount + freeCount == m_nodeCapacity);
    }

    /// <summary>
    /// Compute the height of the binary tree in O(N) time. Should not be
    /// called often.
    /// </summary>
    public int GetHeight() => m_root == NULL_NODE ? 0 : m_nodes[m_root].Height;

    /// <summary>
    /// Get the maximum balance of an node in the tree. The balance is the difference
    /// in height of the two children of a node.
    /// </summary>
    public int GetMaxBalance()
    {
        int maxBalance = 0;
        for (int i = 0; i < m_nodeCapacity; i++)
        {
            if (m_nodes[i].Height <= 1)
                continue;

            Debug.Assert(!m_nodes[i].IsLeaf);

            int child1 = m_nodes[i].Child1;
            int child2 = m_nodes[i].Child2;
            int balance = (m_nodes[child2].Height - m_nodes[child1].Height).Abs();
            maxBalance = Max(maxBalance, balance);
        }

        return maxBalance;
    }

    /// <summary>
    /// Get the ratio of the sum of the node areas to the root area.
    /// </summary>
    public float SurfaceAreatio()
    {
        if (m_root == NULL_NODE)
            return 0f;

        float rootArea = m_nodes[m_root].AABB.SurfaceArea;

        float totalArea = 0f;
        for (int i = 0; i < m_nodeCapacity; i++)
        {
            // Free node in pool, ignore
            if (m_nodes[i].Height < 0)
                continue;

            totalArea += m_nodes[i].AABB.SurfaceArea;
        }

        return totalArea / rootArea;
    }

    /// <summary>
    /// Build an optimal tree. Very expensive. For testing.
    /// </summary>
    public void RebuildBottomUp()
    {
        int[] nodes = new int[m_nodeCount];
        int count = 0;

        // Build array of leaves. Free the rest.
        for (int i = 0; i < m_nodeCapacity; i++)
        {
            // free node in pool, ignore
            if (m_nodes[i].Height < 0)
                continue;

            if (m_nodes[i].IsLeaf)
            {
                m_nodes[i].Parent = NULL_NODE;
                nodes[count] = i;
                ++count;
            }
            else
            {
                FreeNode(i);
            }
        }

        while (count > 1)
        {
            float minCost = float.MaxValue;
            int iMin = -1, jMin = -1;
            for (int i = 0; i < count; ++i)
            {
                AxisBox3D aabbi = m_nodes[nodes[i]].AABB;

                for (int j = i + 1; j < count; ++j)
                {
                    AxisBox3D aabbj = m_nodes[nodes[j]].AABB;
                    AxisBox3D b = aabbi;
                    b.Encapsulate(aabbj);

                    float cost = b.SurfaceArea;
                    if (cost < minCost)
                    {
                        iMin = i;
                        jMin = j;
                        minCost = cost;
                    }
                }
            }

            int index1 = nodes[iMin];
            int index2 = nodes[jMin];

            int parentIndex = AllocateNode();
            m_nodes[parentIndex].Child1 = index1;
            m_nodes[parentIndex].Child2 = index2;
            m_nodes[parentIndex].Height = 1 + Max(m_nodes[index1].Height, m_nodes[index2].Height);

            AxisBox3D newAABB = m_nodes[index1].AABB;
            newAABB.Encapsulate(m_nodes[index2].AABB);
            m_nodes[parentIndex].AABB = newAABB;

            m_nodes[parentIndex].Parent = NULL_NODE;

            m_nodes[index1].Parent = parentIndex;
            m_nodes[index2].Parent = parentIndex;

            nodes[jMin] = nodes[count - 1];
            nodes[iMin] = parentIndex;
            --count;
        }

        m_root = nodes[0];

        Validate();
    }

    /// <summary>
    /// Shift the world origin. Useful for large worlds.
    /// The shift formula is: position -= newOrigin
    /// </summary>
    /// <param name="newOrigin">The new origin with respect to the old origin</param>
    public void ShiftOrigin(Float3 newOrigin)
    {
        for (int i = 0; i < m_nodeCapacity; i++)
        {
            m_nodes[i].AABB.min -= newOrigin;
            m_nodes[i].AABB.max -= newOrigin;
        }
    }

    /// <summary>
    /// Enumerate over all leaf nodes which are fully or partially within the given bounds.
    /// </summary>
    public IEnumerable<Node> EnumerateOverlappingLeafNodes(AxisBox3D bounds)
    {
        m_growableStack.Clear();
        m_growableStack.Push(m_root);

        while (m_growableStack.Count > 0)
        {
            int nodeId = m_growableStack.Pop();

            if (nodeId == NULL_NODE)
                continue;

            if (m_nodes[nodeId].AABB.Intersect(bounds))
            {
                if (m_nodes[nodeId].IsLeaf)
                {
                    yield return m_nodes[nodeId];
                }
                else
                {
                    m_growableStack.Push(m_nodes[nodeId].Child1);
                    m_growableStack.Push(m_nodes[nodeId].Child2);
                }
            }
        }
    }

    /// <summary>
    /// Get the padded bounds for an object.
    /// </summary>
    public AxisBox3D GetPaddedBounds(int key)
    {
        Debug.Assert(0 <= key && key < m_nodeCapacity);
        return m_nodes[key].AABB;
    }

    /// <summary>
    /// Get the padded bounds for an object.
    /// </summary>
    public AxisBox3D GetPaddedBounds(T clientObject)
    {
        Debug.Assert(m_keys.TryGetValue(clientObject, out int key));
        return GetPaddedBounds(key);
    }

    public bool Raycast(Ray ray, out Node hit) => Raycast(ray, m_root, out hit);

    private bool Raycast(Ray ray, int startPoint, out Node hit)
    {
        Debug.Assert(0 <= startPoint && startPoint < m_nodeCapacity);
        Debug.Assert(startPoint != NULL_NODE);

        hit = default;
        Node node = m_nodes[startPoint];

        if (node.IsLeaf)
        {
            hit = node;
            return true;
        }
        else
        {
            if (TryGetNode(node.Child1, out Node node1) && node1.AABB.Intersect(ray) && Raycast(ray, node.Child1, out hit))
                return true;

            if (TryGetNode(node.Child2, out Node node2) && node2.AABB.Intersect(ray) && Raycast(ray, node.Child2, out hit))
                return true;
        }

        return false;
    }

    #endregion

    #region New Public Methods

    /// <summary>
    /// Return the node at the given key, if it exists.
    /// </summary>
    public Node GetNode(int key)
    {
        Debug.Assert(0 <= key && key < m_nodeCapacity);
        return m_nodes[key];
    }

    /// <summary>
    /// Return the unique key for the given object, if it exists.
    /// </summary>
    public int GetKey(T clientObject)
    {
        Debug.Assert(m_keys.TryGetValue(clientObject, out int key));
        return key;
    }

    /// <summary>
    /// Return the node for the given object, if it exists.
    /// </summary>
    public Node GetNode(T clientObject)
    {
        Debug.Assert(m_keys.TryGetValue(clientObject, out int key));
        return GetNode(key);
    }

    /// <summary>
    /// Return the node at the given key, if it exists.
    /// </summary>
    public bool TryGetNode(int key, out Node node)
    {
        node = default(Node);

        if (key < 0 || key >= m_nodeCapacity || key == NULL_NODE)
            return false;

        node = m_nodes[key];

        return true;
    }

    /// <summary>
    /// Enumerate all nodes, both leaf and internal, and return both the nodes themselves and their depth in the tree.
    /// </summary>
    public IEnumerable<(Node, int)> EnumerateNodes()
    {
        foreach ((Node, int) node in EnumerateNodes(m_root, 0))
            yield return node;
    }

    private IEnumerable<(Node, int)> EnumerateNodes(int startPoint, int currentDepth)
    {
        Debug.Assert(0 <= startPoint && startPoint < m_nodeCapacity);
        Debug.Assert(startPoint != NULL_NODE);

        Node node = m_nodes[startPoint];

        yield return (node, currentDepth);

        if (!node.IsLeaf)
        {
            int child1 = node.Child1;

            if (child1 != NULL_NODE)
            {
                foreach ((Node, int) child1Node in EnumerateNodes(child1, currentDepth + 1))
                    yield return child1Node;
            }

            int child2 = node.Child2;

            if (child2 != NULL_NODE)
            {
                foreach ((Node, int) child2Node in EnumerateNodes(child2, currentDepth + 1))
                    yield return child2Node;
            }
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Allocate a node from the pool. Grow the pool if necessary.
    /// </summary>
    private int AllocateNode()
    {
        // Expand the node pool as needed.
        if (m_freeList == NULL_NODE)
        {
            Debug.Assert(m_nodeCount == m_nodeCapacity);

            // The free list is empty. Rebuild a bigger pool.
            Node[] oldNodes = m_nodes;
            m_nodeCapacity *= 2;

            m_nodes = new Node[m_nodeCapacity];

            for (int i = 0; i < oldNodes.Length; i++)
                m_nodes[i] = oldNodes[i];

            // Build a linked list for the free list.
            for (int i = m_nodeCount; i < m_nodeCapacity - 1; i++)
            {
                m_nodes[i].Next = i + 1;
                m_nodes[i].Height = -1;
            }

            m_nodes[m_nodeCapacity - 1].Next = NULL_NODE;
            m_nodes[m_nodeCapacity - 1].Height = -1;
            m_freeList = m_nodeCount;
        }

        // Peel a node off the free list.
        int nodeId = m_freeList;

        m_freeList = m_nodes[nodeId].Next;
        m_nodes[nodeId].Parent = NULL_NODE;
        m_nodes[nodeId].Child1 = NULL_NODE;
        m_nodes[nodeId].Child2 = NULL_NODE;
        m_nodes[nodeId].Height = 0;
        m_nodes[nodeId].Next = 0;
        m_nodes[nodeId].Object = null;

        ++m_nodeCount;

        return nodeId;
    }

    /// <summary>
    /// Return a node to the pool.
    /// </summary>
    private void FreeNode(int key)
    {
        Debug.Assert(0 <= key && key < m_nodeCapacity);
        Debug.Assert(0 < m_nodeCount);

        m_nodes[key].Next = m_freeList;
        m_nodes[key].Height = -1;
        m_freeList = key;
        --m_nodeCount;
    }

    private void InsertLeaf(int leaf)
    {
        ++m_insertionCount;

        if (m_root == NULL_NODE)
        {
            m_root = leaf;
            m_nodes[m_root].Parent = NULL_NODE;
            return;
        }

        // Find the best sibling for this node
        AxisBox3D leafAABB = m_nodes[leaf].AABB;
        int index = m_root;
        while (!m_nodes[index].IsLeaf)
        {
            int child1 = m_nodes[index].Child1;
            int child2 = m_nodes[index].Child2;

            float area = m_nodes[index].AABB.SurfaceArea;

            AxisBox3D combinedAABB = m_nodes[index].AABB;
            combinedAABB.Encapsulate(leafAABB);
            float combinedArea = combinedAABB.SurfaceArea;

            // Cost of creating a new parent for this node and the new leaf
            float cost = 2.0f * combinedArea;

            // Minimum cost of pushing the leaf further down the tree
            float inheritanceCost = 2.0f * (combinedArea - area);

            // Cost of descending into child1
            float cost1;
            if (m_nodes[child1].IsLeaf)
            {
                AxisBox3D aabb = leafAABB;
                aabb.Encapsulate(m_nodes[child1].AABB);
                cost1 = aabb.SurfaceArea + inheritanceCost;
            }
            else
            {
                AxisBox3D aabb = leafAABB;
                aabb.Encapsulate(m_nodes[child1].AABB);
                float oldArea = m_nodes[child1].AABB.SurfaceArea;
                float newArea = aabb.SurfaceArea;
                cost1 = (newArea - oldArea) + inheritanceCost;
            }

            // Cost of descending into child2
            float cost2;
            if (m_nodes[child2].IsLeaf)
            {
                AxisBox3D aabb = leafAABB;
                aabb.Encapsulate(m_nodes[child2].AABB);
                cost2 = aabb.SurfaceArea + inheritanceCost;
            }
            else
            {
                AxisBox3D aabb = leafAABB;
                aabb.Encapsulate(m_nodes[child2].AABB);
                float oldArea = m_nodes[child2].AABB.SurfaceArea;
                float newArea = aabb.SurfaceArea;
                cost2 = newArea - oldArea + inheritanceCost;
            }

            // Descend according to the minimum cost.
            if (cost < cost1 && cost < cost2)
                break;

            // Descend
            if (cost1 < cost2)
                index = child1;
            else
                index = child2;
        }

        int sibling = index;

        // Create a new parent.
        int oldParent = m_nodes[sibling].Parent;
        int newParent = AllocateNode();
        m_nodes[newParent].Parent = oldParent;
        m_nodes[newParent].Object = null;

        AxisBox3D newAABB = leafAABB;
        newAABB.Encapsulate(m_nodes[sibling].AABB);
        m_nodes[newParent].AABB = newAABB;

        m_nodes[newParent].Height = m_nodes[sibling].Height + 1;

        if (oldParent != NULL_NODE)
        {
            // The sibling was not the root.
            if (m_nodes[oldParent].Child1 == sibling)
            {
                m_nodes[oldParent].Child1 = newParent;
            }
            else
            {
                m_nodes[oldParent].Child2 = newParent;
            }

            m_nodes[newParent].Child1 = sibling;
            m_nodes[newParent].Child2 = leaf;
            m_nodes[sibling].Parent = newParent;
            m_nodes[leaf].Parent = newParent;
        }
        else
        {
            // The sibling was the root.
            m_nodes[newParent].Child1 = sibling;
            m_nodes[newParent].Child2 = leaf;
            m_nodes[sibling].Parent = newParent;
            m_nodes[leaf].Parent = newParent;
            m_root = newParent;
        }

        // Walk back up the tree fixing heights and AABBs
        index = m_nodes[leaf].Parent;
        while (index != NULL_NODE)
        {
            index = Balance(index);

            int child1 = m_nodes[index].Child1;
            int child2 = m_nodes[index].Child2;

            Debug.Assert(child1 != NULL_NODE);
            Debug.Assert(child2 != NULL_NODE);

            m_nodes[index].Height = 1 + Max(m_nodes[child1].Height, m_nodes[child2].Height);

            AxisBox3D aabb = m_nodes[child1].AABB;
            aabb.Encapsulate(m_nodes[child2].AABB);
            m_nodes[index].AABB = aabb;

            index = m_nodes[index].Parent;
        }

        //Validate();
    }

    private void RemoveLeaf(int leaf)
    {
        if (leaf == m_root)
        {
            m_root = NULL_NODE;
            return;
        }

        int parent = m_nodes[leaf].Parent;
        int grandParent = m_nodes[parent].Parent;
        int sibling;

        if (m_nodes[parent].Child1 == leaf)
            sibling = m_nodes[parent].Child2;
        else
            sibling = m_nodes[parent].Child1;

        if (grandParent != NULL_NODE)
        {
            // Destroy parent and connect sibling to grandParent.
            if (m_nodes[grandParent].Child1 == parent)
                m_nodes[grandParent].Child1 = sibling;
            else
                m_nodes[grandParent].Child2 = sibling;

            m_nodes[sibling].Parent = grandParent;
            FreeNode(parent);

            // Adjust ancestor bounds.
            int index = grandParent;
            while (index != NULL_NODE)
            {
                index = Balance(index);

                int child1 = m_nodes[index].Child1;
                int child2 = m_nodes[index].Child2;

                AxisBox3D aabb = m_nodes[child1].AABB;
                aabb.Encapsulate(m_nodes[child2].AABB);
                m_nodes[index].AABB = aabb;
                m_nodes[index].Height = 1 + Max(m_nodes[child1].Height, m_nodes[child2].Height);

                index = m_nodes[index].Parent;
            }
        }
        else
        {
            m_root = sibling;
            m_nodes[sibling].Parent = NULL_NODE;
            FreeNode(parent);
        }

        //Validate();
    }

    private int Balance(int iA)
    {
        Debug.Assert(iA != NULL_NODE);

        if (m_nodes[iA].IsLeaf || m_nodes[iA].Height < 2)
            return iA;

        int iB = m_nodes[iA].Child1;
        int iC = m_nodes[iA].Child2;

        Debug.Assert(0 <= iB && iB < m_nodeCapacity);
        Debug.Assert(0 <= iC && iC < m_nodeCapacity);

        int balance = m_nodes[iC].Height - m_nodes[iB].Height;

        // Rotate C up
        if (balance > 1)
        {
            int iF = m_nodes[iC].Child1;
            int iG = m_nodes[iC].Child2;

            Debug.Assert(0 <= iF && iF < m_nodeCapacity);
            Debug.Assert(0 <= iG && iG < m_nodeCapacity);

            // Swap A and C
            m_nodes[iC].Child1 = iA;
            m_nodes[iC].Parent = m_nodes[iA].Parent;
            m_nodes[iA].Parent = iC;

            // A's old parent should point to C
            if (m_nodes[iC].Parent != NULL_NODE)
            {
                if (m_nodes[m_nodes[iC].Parent].Child1 == iA)
                {
                    m_nodes[m_nodes[iC].Parent].Child1 = iC;
                }
                else
                {
                    Debug.Assert(m_nodes[m_nodes[iC].Parent].Child2 == iA);
                    m_nodes[m_nodes[iC].Parent].Child2 = iC;
                }
            }
            else
            {
                m_root = iC;
            }

            // Rotate
            if (m_nodes[iF].Height > m_nodes[iG].Height)
            {
                m_nodes[iC].Child2 = iF;
                m_nodes[iA].Child2 = iG;
                m_nodes[iG].Parent = iA;

                AxisBox3D bounds = m_nodes[iB].AABB;
                bounds.Encapsulate(m_nodes[iG].AABB);
                m_nodes[iA].AABB = bounds;

                bounds = m_nodes[iA].AABB;
                bounds.Encapsulate(m_nodes[iF].AABB);
                m_nodes[iC].AABB = bounds;

                m_nodes[iA].Height = 1 + Max(m_nodes[iB].Height, m_nodes[iG].Height);
                m_nodes[iC].Height = 1 + Max(m_nodes[iA].Height, m_nodes[iF].Height);
            }
            else
            {
                m_nodes[iC].Child2 = iG;
                m_nodes[iA].Child2 = iF;
                m_nodes[iF].Parent = iA;

                AxisBox3D bounds = m_nodes[iB].AABB;
                bounds.Encapsulate(m_nodes[iF].AABB);
                m_nodes[iA].AABB = bounds;

                bounds = m_nodes[iA].AABB;
                bounds.Encapsulate(m_nodes[iG].AABB);
                m_nodes[iC].AABB = bounds;

                m_nodes[iA].Height = 1 + Max(m_nodes[iB].Height, m_nodes[iF].Height);
                m_nodes[iC].Height = 1 + Max(m_nodes[iA].Height, m_nodes[iG].Height);
            }

            return iC;
        }

        // Rotate B up
        if (balance < -1)
        {
            int iD = m_nodes[iB].Child1;
            int iE = m_nodes[iB].Child2;

            Debug.Assert(0 <= iD && iD < m_nodeCapacity);
            Debug.Assert(0 <= iE && iE < m_nodeCapacity);

            // Swap A and B
            m_nodes[iB].Child1 = iA;
            m_nodes[iB].Parent = m_nodes[iA].Parent;
            m_nodes[iA].Parent = iB;

            // A's old parent should point to B
            if (m_nodes[iB].Parent != NULL_NODE)
            {
                if (m_nodes[m_nodes[iB].Parent].Child1 == iA)
                {
                    m_nodes[m_nodes[iB].Parent].Child1 = iB;
                }
                else
                {
                    Debug.Assert(m_nodes[m_nodes[iB].Parent].Child2 == iA);
                    m_nodes[m_nodes[iB].Parent].Child2 = iB;
                }
            }
            else
            {
                m_root = iB;
            }

            // Rotate
            if (m_nodes[iD].Height > m_nodes[iE].Height)
            {
                m_nodes[iB].Child2 = iD;
                m_nodes[iA].Child1 = iE;
                m_nodes[iE].Parent = iA;

                AxisBox3D bounds = m_nodes[iC].AABB;
                bounds.Encapsulate(m_nodes[iE].AABB);
                m_nodes[iA].AABB = bounds;

                bounds = m_nodes[iA].AABB;
                bounds.Encapsulate(m_nodes[iD].AABB);
                m_nodes[iB].AABB = bounds;

                m_nodes[iA].Height = 1 + Max(m_nodes[iC].Height, m_nodes[iE].Height);
                m_nodes[iB].Height = 1 + Max(m_nodes[iA].Height, m_nodes[iD].Height);
            }
            else
            {
                m_nodes[iB].Child2 = iE;
                m_nodes[iA].Child1 = iD;
                m_nodes[iD].Parent = iA;

                AxisBox3D bounds = m_nodes[iC].AABB;
                bounds.Encapsulate(m_nodes[iD].AABB);
                m_nodes[iA].AABB = bounds;

                bounds = m_nodes[iA].AABB;
                bounds.Encapsulate(m_nodes[iE].AABB);
                m_nodes[iB].AABB = bounds;

                m_nodes[iA].Height = 1 + Max(m_nodes[iC].Height, m_nodes[iD].Height);
                m_nodes[iB].Height = 1 + Max(m_nodes[iA].Height, m_nodes[iE].Height);
            }

            return iB;
        }

        return iA;
    }

    private int ComputeHeight()
    {
        int height = ComputeHeight(m_root);
        return height;
    }

    private int ComputeHeight(int nodeId)
    {
        Debug.Assert(0 <= nodeId && nodeId < m_nodeCapacity);

        if (m_nodes[nodeId].IsLeaf)
            return 0;

        int height1 = ComputeHeight(m_nodes[nodeId].Child1);
        int height2 = ComputeHeight(m_nodes[nodeId].Child2);
        return 1 + Max(height1, height2);
    }

    private void ValidateStructure(int index)
    {
        if (index == NULL_NODE)
            return;

        if (index == m_root)
            Debug.Assert(m_nodes[index].Parent == NULL_NODE);

        int child1 = m_nodes[index].Child1;
        int child2 = m_nodes[index].Child2;

        if (m_nodes[index].IsLeaf)
        {
            Debug.Assert(child1 == NULL_NODE);
            Debug.Assert(child2 == NULL_NODE);
            Debug.Assert(m_nodes[index].Height == 0);
            return;
        }

        Debug.Assert(0 <= child1 && child1 < m_nodeCapacity);
        Debug.Assert(0 <= child2 && child2 < m_nodeCapacity);

        Debug.Assert(m_nodes[child1].Parent == index);
        Debug.Assert(m_nodes[child2].Parent == index);

        ValidateStructure(child1);
        ValidateStructure(child2);
    }

    private void ValidateMetrics(int index)
    {
        if (index == NULL_NODE)
            return;

        //const b2TreeNode* node = m_nodes + index;

        int child1 = m_nodes[index].Child1;
        int child2 = m_nodes[index].Child2;

        if (m_nodes[index].IsLeaf)
        {
            Debug.Assert(child1 == NULL_NODE);
            Debug.Assert(child2 == NULL_NODE);
            Debug.Assert(m_nodes[index].Height == 0);
            return;
        }

        Debug.Assert(0 <= child1 && child1 < m_nodeCapacity);
        Debug.Assert(0 <= child2 && child2 < m_nodeCapacity);

        int height1 = m_nodes[child1].Height;
        int height2 = m_nodes[child2].Height;
        int height;
        height = 1 + Max(height1, height2);
        Debug.Assert(m_nodes[index].Height == height);

        AxisBox3D aabb = m_nodes[child1].AABB;
        aabb.Encapsulate(m_nodes[child2].AABB);

        //Debug.Assert(aabb.lowerBound == m_nodes[index].aabb.lowerBound);
        //Debug.Assert(aabb.upperBound == m_nodes[index].aabb.upperBound);

        Debug.Assert(aabb.min == m_nodes[index].AABB.min);
        Debug.Assert(aabb.max == m_nodes[index].AABB.max);

        ValidateMetrics(child1);
        ValidateMetrics(child2);
    }

    #endregion

    #region Subclasses

    public struct Node
    {
        public bool IsLeaf => Child1 == NULL_NODE;

        public int Child1;
        public int Child2;
        public int Height;
        public int Parent;
        public int Next;
        public AxisBox3D AABB;
        public T Object;
    }

    #endregion
}



