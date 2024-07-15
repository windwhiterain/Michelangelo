using System.Numerics;
using System.Reflection.Metadata;
using Michelangelo.Math;
using Michelangelo.SpatialAccessor;

namespace Michelangelo.Geometry.Mesh;
public interface INetTopology
{
    Vertex AddVertex();
    int vertexCount { get; }
    (Edge edge, HalfEdge halfEdge) AddEdge(Vertex a, Vertex b);
    int edgeCount { get; }
}
public interface IMeshTopology : INetTopology
{
    Face AddFace(params HalfEdge[] edges);
    int faceCount { get; }
    IEnumerable<Vertex> Vertices(Face face);
}
public interface IMesh : IMeshTopology
{
    Vertex AddVertex(Float3 position);
    IEnumerable<Float3> Positions(Face face);
}
public class HalfNetTopology : INetTopology
{
    record struct VertexData(
        HalfEdge halfEdge
    )
    {
        public static VertexData Default => new(IIndex<HalfEdge>.Null);
    };
    DynamicArray<Vertex, VertexData> vertexDatas = new(VertexData.Default);
    public int vertexCount => vertexDatas.Count;
    record struct HalfEdgeData
    (
        HalfEdge pair,
        HalfEdge next,
        Vertex to,
        Edge edge
    );
    DynamicArray<HalfEdge, HalfEdgeData> halfEdgeDatas = new(default);
    protected int halfEdgeCount => halfEdgeDatas.Count;
    record struct DedgeData(
        HalfEdge HalfEdge
    );
    DynamicArray<Edge, DedgeData> edgeDatas = new(default);
    public int edgeCount => edgeDatas.Count;
    public Vertex AddVertex()
    {
        return vertexDatas.Add(new(new(-1)));
    }
    public (Edge edge, HalfEdge halfEdge) AddEdge(Vertex a, Vertex b)
    {
        Edge edge = new(edgeCount);
        HalfEdge ab = new(halfEdgeCount);
        HalfEdge ba = new(halfEdgeCount + 1);
        ab = halfEdgeDatas.Add(new(ba, vertexDatas[a].halfEdge.NullOr(ab), b, edge));
        ba = halfEdgeDatas.Add(new(ab, vertexDatas[b].halfEdge.NullOr(ba), a, edge));
        vertexDatas[a].halfEdge = ab;
        vertexDatas[b].halfEdge = ba;
        return (edgeDatas.Add(new(ab)), ab);
    }
    public HalfEdge Next(HalfEdge halfEdge) => halfEdgeDatas[halfEdge].next;
    public Vertex To(HalfEdge halfEdge) => halfEdgeDatas[halfEdge].to;
}
public class HalfMeshTopology : HalfNetTopology, IMeshTopology
{
    protected record struct HalfEdgeData
    (
        HalfFace halfFace
    )
    {
        public static HalfEdgeData Default => new(IIndex<HalfFace>.Null);
    }
    protected DynamicArray<HalfEdge, HalfEdgeData> halfEdgeDatas = new(HalfEdgeData.Default);
    protected record struct HalfFaceData(
        HalfFace next,
        HalfFace neighbor,
        HalfEdge halfEdge
    );
    protected DynamicArray<HalfFace, HalfFaceData> halfFaceDatas = new(default);
    protected int halfFaceCount => halfFaceDatas.Count;
    protected record struct FaceData
    (
        HalfFace halfFace
    );
    protected DynamicArray<Face, FaceData> faceDatas = new(default);
    public int faceCount => faceDatas.Count;
    public Face AddFace(params HalfEdge[] halfEdges)
    {
        HalfFace previous = new(halfFaceCount + halfEdges.Length - 1);
        foreach (var halfEdge in halfEdges)
        {
            HalfFace halfFace = new(halfFaceCount);
            halfFace = halfFaceDatas.Add(new(previous, halfEdgeDatas[halfEdge].halfFace.NullOr(halfFace), halfEdge));
            halfEdgeDatas[halfEdge].halfFace = halfFace;
            previous = halfFace;
        }
        return faceDatas.Add(new(previous));
    }
    public IEnumerable<Vertex> Vertices(Face face)
    {
        var loop = faceDatas[face].halfFace;
        var start = loop;
        if (loop.IsNull()) { yield break; }
        do
        {
            var halfFaceData = halfFaceDatas[loop];
            yield return To(halfFaceData.halfEdge);
            loop = halfFaceData.next;
        } while (loop != start);
    }
}

public class HalfMesh : HalfMeshTopology, IMesh
{
    record struct VertexData(
        Float3 position
    );
    DynamicArray<Vertex, VertexData> vertexDatas = new(default);

    public IEnumerable<Float3> Positions(Face face) => Vertices(face).Select(Position);
    public Float3 Position(Vertex vertex) => vertexDatas[vertex].position;

    public Vertex AddVertex(Float3 position)
    {
        var ret = AddVertex();
        vertexDatas[ret].position = position;
        VertexSpatialAccessor.Add(new(this, ret));
        return ret;
    }
    public Vertex Vertex(Ray _)
    {
        if (VertexSpatialAccessor.Raycast(_, out var hit))
        {
            return hit.Object.vertex;
        }
        return IIndex<Vertex>.Null;
    }
    record class VertexClient
    (
        HalfMesh mesh,
        Vertex vertex
    ) : BVH<VertexClient>.Client
    {
        protected override Float3 Position => mesh.Position(vertex);

        public override AxisBox3D Box =>
            AxisBox3D.FromSphere(new(Position, 1));
    }
    BVH<VertexClient> VertexSpatialAccessor = new();
}