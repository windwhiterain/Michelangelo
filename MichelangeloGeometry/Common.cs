namespace Michelangelo;
public interface IIndex
{
    int index { get; set; }
}
public interface IIndex<Self> : IIndex where Self : IIndex<Self>
{
    abstract static Self New(int index);
    static Self Null => Self.New(-1);
    static IEnumerable<Self> Each(int count) => Enumerable.Range(0, count).Select(Self.New);
}
public static class Index
{
    public static bool IsNull<Self>(this Self _) where Self : IIndex<Self> => _.index == -1;
    public static Self NullOr<Self>(this Self _, Self replaceNull) where Self : IIndex<Self> => _.IsNull() ? replaceNull : _;
    public static IEnumerable<Self> Each<Self>(this int count) where Self : IIndex<Self> => IIndex<Self>.Each(count);
}
public record struct Vertex(int index = -1) : IIndex<Vertex>
{
    public int index { get; set; } = index;

    public static Vertex New(int index) => new(index);
}
public record struct HalfEdge(int index = -1) : IIndex<HalfEdge>
{
    public int index { get; set; } = index;
    public static HalfEdge New(int index) => new(index);
}
public record struct Edge(int index = -1) : IIndex<Edge>
{
    public int index { get; set; } = index;
    public static Edge New(int index) => new(index);
}
public record struct HalfFace(int index = -1) : IIndex<HalfFace>
{
    public int index { get; set; } = index;
    public static HalfFace New(int index) => new(index);
}
public record struct Face(int index = -1) : IIndex<Face>
{
    public int index { get; set; } = index;
    public static Face New(int index) => new(index);
}
public class DynamicArray<K, V>(V defualtValue) where K : IIndex<K>
{
    V[] array = Enumerable.Repeat<V>(defualtValue, 8).ToArray();
    int count = 0;
    public int Count => count;
    public void IncludeIndex(int index)
    {
        while (index >= array.Length)
        {
            var newArray = Enumerable.Repeat<V>(defualtValue, array.Length * 2).ToArray();
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            array = newArray;
        }
        count = Math.Max(index + 1, count);
    }
    public K Add(V item)
    {
        int index = count;
        IncludeIndex(index);
        array[index] = item;
        return K.New(index);
    }
    public ref V this[K index]
    {
        get
        {
            IncludeIndex(index.index);
            if (index.index > array.Length || index.index < 0)
            {
                Console.WriteLine($"{index.index} out of 0..{array.Length}");
            }
            return ref array[index.index];
        }
    }
    public ref V this[int index]
    {
        get
        {
            IncludeIndex(index);
            if (index > array.Length || index < 0)
            {
                Console.WriteLine($"{index} out of 0..{array.Length}");
            }
            return ref array[index];
        }
    }
    public delegate void Operator(ref V _);
    public delegate void Operator<T1>(ref V _, ref T1 a1);
    public delegate void Operator<T1, T2>(ref V _, ref T1 a1, ref T2 a2);
    public delegate void Operator<T1, T2, T3>(ref V _, ref T1 a1, ref T2 a2, ref T3 a3);
    public void ForEach(Operator op)
    {
        for (int i = 0; i < count; i++)
        {
            op(ref this[i]);
        }
    }
    public void ForEach<T1>(Operator<T1> op, DynamicArray<K, T1> a1)
    {
        for (int i = 0; i < count; i++)
        {
            op(ref this[i], ref a1[i]);
        }
    }
    public void ForEach<T1, T2>(Operator<T1, T2> op, DynamicArray<K, T1> a1, DynamicArray<K, T2> a2)
    {
        for (int i = 0; i < count; i++)
        {
            op(ref this[i], ref a1[i], ref a2[i]);
        }
    }
    public void ForEach<T1, T2, T3>(Operator<T1, T2, T3> op, DynamicArray<K, T1> a1, DynamicArray<K, T2> a2, DynamicArray<K, T3> a3)
    {
        for (int i = 0; i < count; i++)
        {
            op(ref this[i], ref a1[i], ref a2[i], ref a3[i]);
        }
    }
}