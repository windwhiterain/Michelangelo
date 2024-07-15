
namespace Michelangelo.Math;
using Float=float;
using Int=int;
using Bool=bool;

public record struct Float2
(
    Float x,
Float y
){

public static Float2 operator +(Float2 a, Float2 b)=>
    new(a.x + b.x,a.y + b.y);

public static Float2 operator +(Float2 a, Float b)=>
    new(a.x + b,a.y + b);

public static Float2 operator +(Float b, Float2 a)=>
    new(a.x + b,a.y + b);

public static Float2 operator -(Float2 a, Float2 b)=>
    new(a.x - b.x,a.y - b.y);

public static Float2 operator -(Float2 a, Float b)=>
    new(a.x - b,a.y - b);

public static Float2 operator -(Float b, Float2 a)=>
    new(a.x - b,a.y - b);

public static Float2 operator *(Float2 a, Float2 b)=>
    new(a.x * b.x,a.y * b.y);

public static Float2 operator *(Float2 a, Float b)=>
    new(a.x * b,a.y * b);

public static Float2 operator *(Float b, Float2 a)=>
    new(a.x * b,a.y * b);

public Float2 Max(Float2 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y));

public Float2 Max(Float b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b));

public Float2 Min(Float2 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y));

public Float2 Min(Float b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b));

public static Float2 operator +(Float2 a, Int2 b)=>
    new(a.x + b.x,a.y + b.y);

public static Float2 operator +(Float2 a, Int b)=>
    new(a.x + b,a.y + b);

public static Float2 operator +(Int b, Float2 a)=>
    new(a.x + b,a.y + b);

public static Float2 operator -(Float2 a, Int2 b)=>
    new(a.x - b.x,a.y - b.y);

public static Float2 operator -(Float2 a, Int b)=>
    new(a.x - b,a.y - b);

public static Float2 operator -(Int b, Float2 a)=>
    new(a.x - b,a.y - b);

public static Float2 operator *(Float2 a, Int2 b)=>
    new(a.x * b.x,a.y * b.y);

public static Float2 operator *(Float2 a, Int b)=>
    new(a.x * b,a.y * b);

public static Float2 operator *(Int b, Float2 a)=>
    new(a.x * b,a.y * b);

public Float2 Max(Int2 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y));

public Float2 Max(Int b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b));

public Float2 Min(Int2 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y));

public Float2 Min(Int b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b));

public static Bool2 operator >(Float2 a, Float2 b)=>
    new(a.x > b.x,a.y > b.y);

public static Bool2 operator >(Float2 a, Float b)=>
    new(a.x > b,a.y > b);

public static Bool2 operator >(Float b, Float2 a)=>
    new(a.x > b,a.y > b);

public static Bool2 operator <(Float2 a, Float2 b)=>
    new(a.x < b.x,a.y < b.y);

public static Bool2 operator <(Float2 a, Float b)=>
    new(a.x < b,a.y < b);

public static Bool2 operator <(Float b, Float2 a)=>
    new(a.x < b,a.y < b);

public static Bool2 operator >=(Float2 a, Float2 b)=>
    new(a.x >= b.x,a.y >= b.y);

public static Bool2 operator >=(Float2 a, Float b)=>
    new(a.x >= b,a.y >= b);

public static Bool2 operator >=(Float b, Float2 a)=>
    new(a.x >= b,a.y >= b);

public static Bool2 operator <=(Float2 a, Float2 b)=>
    new(a.x <= b.x,a.y <= b.y);

public static Bool2 operator <=(Float2 a, Float b)=>
    new(a.x <= b,a.y <= b);

public static Bool2 operator <=(Float b, Float2 a)=>
    new(a.x <= b,a.y <= b);

public static Bool2 operator >(Float2 a, Int2 b)=>
    new(a.x > b.x,a.y > b.y);

public static Bool2 operator >(Float2 a, Int b)=>
    new(a.x > b,a.y > b);

public static Bool2 operator >(Int b, Float2 a)=>
    new(a.x > b,a.y > b);

public static Bool2 operator <(Float2 a, Int2 b)=>
    new(a.x < b.x,a.y < b.y);

public static Bool2 operator <(Float2 a, Int b)=>
    new(a.x < b,a.y < b);

public static Bool2 operator <(Int b, Float2 a)=>
    new(a.x < b,a.y < b);

public static Bool2 operator >=(Float2 a, Int2 b)=>
    new(a.x >= b.x,a.y >= b.y);

public static Bool2 operator >=(Float2 a, Int b)=>
    new(a.x >= b,a.y >= b);

public static Bool2 operator >=(Int b, Float2 a)=>
    new(a.x >= b,a.y >= b);

public static Bool2 operator <=(Float2 a, Int2 b)=>
    new(a.x <= b.x,a.y <= b.y);

public static Bool2 operator <=(Float2 a, Int b)=>
    new(a.x <= b,a.y <= b);

public static Bool2 operator <=(Int b, Float2 a)=>
    new(a.x <= b,a.y <= b);

public static Float2 operator /(Float2 a, Float2 b)=>
    new(a.x / b.x,a.y / b.y);

public static Float2 operator /(Float2 a, Float b)=>
    new(a.x / b,a.y / b);

public static Float2 operator /(Float b, Float2 a)=>
    new(a.x / b,a.y / b);

public static Float2 operator /(Float2 a, Int2 b)=>
    new(a.x / b.x,a.y / b.y);

public static Float2 operator /(Float2 a, Int b)=>
    new(a.x / b,a.y / b);

public static Float2 operator /(Int b, Float2 a)=>
    new(a.x / b,a.y / b);

    public Float this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 2);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Float2 Diagonal(Float _)
    {
        return new(_,_);
    }
    public static Float2 Basis(int axis,Float value)
    {
        var ret = new Float2();
        ret[axis] = value;
        return ret;
    }
}

public record struct Float3
(
    Float x,
Float y,
Float z
){

public static Float3 operator +(Float3 a, Float3 b)=>
    new(a.x + b.x,a.y + b.y,a.z + b.z);

public static Float3 operator +(Float3 a, Float b)=>
    new(a.x + b,a.y + b,a.z + b);

public static Float3 operator +(Float b, Float3 a)=>
    new(a.x + b,a.y + b,a.z + b);

public static Float3 operator -(Float3 a, Float3 b)=>
    new(a.x - b.x,a.y - b.y,a.z - b.z);

public static Float3 operator -(Float3 a, Float b)=>
    new(a.x - b,a.y - b,a.z - b);

public static Float3 operator -(Float b, Float3 a)=>
    new(a.x - b,a.y - b,a.z - b);

public static Float3 operator *(Float3 a, Float3 b)=>
    new(a.x * b.x,a.y * b.y,a.z * b.z);

public static Float3 operator *(Float3 a, Float b)=>
    new(a.x * b,a.y * b,a.z * b);

public static Float3 operator *(Float b, Float3 a)=>
    new(a.x * b,a.y * b,a.z * b);

public Float3 Max(Float3 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y),System.Math.Max(z,b.z));

public Float3 Max(Float b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b),System.Math.Max(z,b));

public Float3 Min(Float3 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y),System.Math.Min(z,b.z));

public Float3 Min(Float b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b),System.Math.Min(z,b));

public static Float3 operator +(Float3 a, Int3 b)=>
    new(a.x + b.x,a.y + b.y,a.z + b.z);

public static Float3 operator +(Float3 a, Int b)=>
    new(a.x + b,a.y + b,a.z + b);

public static Float3 operator +(Int b, Float3 a)=>
    new(a.x + b,a.y + b,a.z + b);

public static Float3 operator -(Float3 a, Int3 b)=>
    new(a.x - b.x,a.y - b.y,a.z - b.z);

public static Float3 operator -(Float3 a, Int b)=>
    new(a.x - b,a.y - b,a.z - b);

public static Float3 operator -(Int b, Float3 a)=>
    new(a.x - b,a.y - b,a.z - b);

public static Float3 operator *(Float3 a, Int3 b)=>
    new(a.x * b.x,a.y * b.y,a.z * b.z);

public static Float3 operator *(Float3 a, Int b)=>
    new(a.x * b,a.y * b,a.z * b);

public static Float3 operator *(Int b, Float3 a)=>
    new(a.x * b,a.y * b,a.z * b);

public Float3 Max(Int3 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y),System.Math.Max(z,b.z));

public Float3 Max(Int b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b),System.Math.Max(z,b));

public Float3 Min(Int3 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y),System.Math.Min(z,b.z));

public Float3 Min(Int b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b),System.Math.Min(z,b));

public static Bool3 operator >(Float3 a, Float3 b)=>
    new(a.x > b.x,a.y > b.y,a.z > b.z);

public static Bool3 operator >(Float3 a, Float b)=>
    new(a.x > b,a.y > b,a.z > b);

public static Bool3 operator >(Float b, Float3 a)=>
    new(a.x > b,a.y > b,a.z > b);

public static Bool3 operator <(Float3 a, Float3 b)=>
    new(a.x < b.x,a.y < b.y,a.z < b.z);

public static Bool3 operator <(Float3 a, Float b)=>
    new(a.x < b,a.y < b,a.z < b);

public static Bool3 operator <(Float b, Float3 a)=>
    new(a.x < b,a.y < b,a.z < b);

public static Bool3 operator >=(Float3 a, Float3 b)=>
    new(a.x >= b.x,a.y >= b.y,a.z >= b.z);

public static Bool3 operator >=(Float3 a, Float b)=>
    new(a.x >= b,a.y >= b,a.z >= b);

public static Bool3 operator >=(Float b, Float3 a)=>
    new(a.x >= b,a.y >= b,a.z >= b);

public static Bool3 operator <=(Float3 a, Float3 b)=>
    new(a.x <= b.x,a.y <= b.y,a.z <= b.z);

public static Bool3 operator <=(Float3 a, Float b)=>
    new(a.x <= b,a.y <= b,a.z <= b);

public static Bool3 operator <=(Float b, Float3 a)=>
    new(a.x <= b,a.y <= b,a.z <= b);

public static Bool3 operator >(Float3 a, Int3 b)=>
    new(a.x > b.x,a.y > b.y,a.z > b.z);

public static Bool3 operator >(Float3 a, Int b)=>
    new(a.x > b,a.y > b,a.z > b);

public static Bool3 operator >(Int b, Float3 a)=>
    new(a.x > b,a.y > b,a.z > b);

public static Bool3 operator <(Float3 a, Int3 b)=>
    new(a.x < b.x,a.y < b.y,a.z < b.z);

public static Bool3 operator <(Float3 a, Int b)=>
    new(a.x < b,a.y < b,a.z < b);

public static Bool3 operator <(Int b, Float3 a)=>
    new(a.x < b,a.y < b,a.z < b);

public static Bool3 operator >=(Float3 a, Int3 b)=>
    new(a.x >= b.x,a.y >= b.y,a.z >= b.z);

public static Bool3 operator >=(Float3 a, Int b)=>
    new(a.x >= b,a.y >= b,a.z >= b);

public static Bool3 operator >=(Int b, Float3 a)=>
    new(a.x >= b,a.y >= b,a.z >= b);

public static Bool3 operator <=(Float3 a, Int3 b)=>
    new(a.x <= b.x,a.y <= b.y,a.z <= b.z);

public static Bool3 operator <=(Float3 a, Int b)=>
    new(a.x <= b,a.y <= b,a.z <= b);

public static Bool3 operator <=(Int b, Float3 a)=>
    new(a.x <= b,a.y <= b,a.z <= b);

public static Float3 operator /(Float3 a, Float3 b)=>
    new(a.x / b.x,a.y / b.y,a.z / b.z);

public static Float3 operator /(Float3 a, Float b)=>
    new(a.x / b,a.y / b,a.z / b);

public static Float3 operator /(Float b, Float3 a)=>
    new(a.x / b,a.y / b,a.z / b);

public static Float3 operator /(Float3 a, Int3 b)=>
    new(a.x / b.x,a.y / b.y,a.z / b.z);

public static Float3 operator /(Float3 a, Int b)=>
    new(a.x / b,a.y / b,a.z / b);

public static Float3 operator /(Int b, Float3 a)=>
    new(a.x / b,a.y / b,a.z / b);

    public Float this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,
2=>z,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;
case 2:z=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 3);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Float3 Diagonal(Float _)
    {
        return new(_,_,_);
    }
    public static Float3 Basis(int axis,Float value)
    {
        var ret = new Float3();
        ret[axis] = value;
        return ret;
    }
}

public record struct Float4
(
    Float x,
Float y,
Float z,
Float w
){

public static Float4 operator +(Float4 a, Float4 b)=>
    new(a.x + b.x,a.y + b.y,a.z + b.z,a.w + b.w);

public static Float4 operator +(Float4 a, Float b)=>
    new(a.x + b,a.y + b,a.z + b,a.w + b);

public static Float4 operator +(Float b, Float4 a)=>
    new(a.x + b,a.y + b,a.z + b,a.w + b);

public static Float4 operator -(Float4 a, Float4 b)=>
    new(a.x - b.x,a.y - b.y,a.z - b.z,a.w - b.w);

public static Float4 operator -(Float4 a, Float b)=>
    new(a.x - b,a.y - b,a.z - b,a.w - b);

public static Float4 operator -(Float b, Float4 a)=>
    new(a.x - b,a.y - b,a.z - b,a.w - b);

public static Float4 operator *(Float4 a, Float4 b)=>
    new(a.x * b.x,a.y * b.y,a.z * b.z,a.w * b.w);

public static Float4 operator *(Float4 a, Float b)=>
    new(a.x * b,a.y * b,a.z * b,a.w * b);

public static Float4 operator *(Float b, Float4 a)=>
    new(a.x * b,a.y * b,a.z * b,a.w * b);

public Float4 Max(Float4 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y),System.Math.Max(z,b.z),System.Math.Max(w,b.w));

public Float4 Max(Float b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b),System.Math.Max(z,b),System.Math.Max(w,b));

public Float4 Min(Float4 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y),System.Math.Min(z,b.z),System.Math.Min(w,b.w));

public Float4 Min(Float b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b),System.Math.Min(z,b),System.Math.Min(w,b));

public static Float4 operator +(Float4 a, Int4 b)=>
    new(a.x + b.x,a.y + b.y,a.z + b.z,a.w + b.w);

public static Float4 operator +(Float4 a, Int b)=>
    new(a.x + b,a.y + b,a.z + b,a.w + b);

public static Float4 operator +(Int b, Float4 a)=>
    new(a.x + b,a.y + b,a.z + b,a.w + b);

public static Float4 operator -(Float4 a, Int4 b)=>
    new(a.x - b.x,a.y - b.y,a.z - b.z,a.w - b.w);

public static Float4 operator -(Float4 a, Int b)=>
    new(a.x - b,a.y - b,a.z - b,a.w - b);

public static Float4 operator -(Int b, Float4 a)=>
    new(a.x - b,a.y - b,a.z - b,a.w - b);

public static Float4 operator *(Float4 a, Int4 b)=>
    new(a.x * b.x,a.y * b.y,a.z * b.z,a.w * b.w);

public static Float4 operator *(Float4 a, Int b)=>
    new(a.x * b,a.y * b,a.z * b,a.w * b);

public static Float4 operator *(Int b, Float4 a)=>
    new(a.x * b,a.y * b,a.z * b,a.w * b);

public Float4 Max(Int4 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y),System.Math.Max(z,b.z),System.Math.Max(w,b.w));

public Float4 Max(Int b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b),System.Math.Max(z,b),System.Math.Max(w,b));

public Float4 Min(Int4 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y),System.Math.Min(z,b.z),System.Math.Min(w,b.w));

public Float4 Min(Int b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b),System.Math.Min(z,b),System.Math.Min(w,b));

public static Bool4 operator >(Float4 a, Float4 b)=>
    new(a.x > b.x,a.y > b.y,a.z > b.z,a.w > b.w);

public static Bool4 operator >(Float4 a, Float b)=>
    new(a.x > b,a.y > b,a.z > b,a.w > b);

public static Bool4 operator >(Float b, Float4 a)=>
    new(a.x > b,a.y > b,a.z > b,a.w > b);

public static Bool4 operator <(Float4 a, Float4 b)=>
    new(a.x < b.x,a.y < b.y,a.z < b.z,a.w < b.w);

public static Bool4 operator <(Float4 a, Float b)=>
    new(a.x < b,a.y < b,a.z < b,a.w < b);

public static Bool4 operator <(Float b, Float4 a)=>
    new(a.x < b,a.y < b,a.z < b,a.w < b);

public static Bool4 operator >=(Float4 a, Float4 b)=>
    new(a.x >= b.x,a.y >= b.y,a.z >= b.z,a.w >= b.w);

public static Bool4 operator >=(Float4 a, Float b)=>
    new(a.x >= b,a.y >= b,a.z >= b,a.w >= b);

public static Bool4 operator >=(Float b, Float4 a)=>
    new(a.x >= b,a.y >= b,a.z >= b,a.w >= b);

public static Bool4 operator <=(Float4 a, Float4 b)=>
    new(a.x <= b.x,a.y <= b.y,a.z <= b.z,a.w <= b.w);

public static Bool4 operator <=(Float4 a, Float b)=>
    new(a.x <= b,a.y <= b,a.z <= b,a.w <= b);

public static Bool4 operator <=(Float b, Float4 a)=>
    new(a.x <= b,a.y <= b,a.z <= b,a.w <= b);

public static Bool4 operator >(Float4 a, Int4 b)=>
    new(a.x > b.x,a.y > b.y,a.z > b.z,a.w > b.w);

public static Bool4 operator >(Float4 a, Int b)=>
    new(a.x > b,a.y > b,a.z > b,a.w > b);

public static Bool4 operator >(Int b, Float4 a)=>
    new(a.x > b,a.y > b,a.z > b,a.w > b);

public static Bool4 operator <(Float4 a, Int4 b)=>
    new(a.x < b.x,a.y < b.y,a.z < b.z,a.w < b.w);

public static Bool4 operator <(Float4 a, Int b)=>
    new(a.x < b,a.y < b,a.z < b,a.w < b);

public static Bool4 operator <(Int b, Float4 a)=>
    new(a.x < b,a.y < b,a.z < b,a.w < b);

public static Bool4 operator >=(Float4 a, Int4 b)=>
    new(a.x >= b.x,a.y >= b.y,a.z >= b.z,a.w >= b.w);

public static Bool4 operator >=(Float4 a, Int b)=>
    new(a.x >= b,a.y >= b,a.z >= b,a.w >= b);

public static Bool4 operator >=(Int b, Float4 a)=>
    new(a.x >= b,a.y >= b,a.z >= b,a.w >= b);

public static Bool4 operator <=(Float4 a, Int4 b)=>
    new(a.x <= b.x,a.y <= b.y,a.z <= b.z,a.w <= b.w);

public static Bool4 operator <=(Float4 a, Int b)=>
    new(a.x <= b,a.y <= b,a.z <= b,a.w <= b);

public static Bool4 operator <=(Int b, Float4 a)=>
    new(a.x <= b,a.y <= b,a.z <= b,a.w <= b);

public static Float4 operator /(Float4 a, Float4 b)=>
    new(a.x / b.x,a.y / b.y,a.z / b.z,a.w / b.w);

public static Float4 operator /(Float4 a, Float b)=>
    new(a.x / b,a.y / b,a.z / b,a.w / b);

public static Float4 operator /(Float b, Float4 a)=>
    new(a.x / b,a.y / b,a.z / b,a.w / b);

public static Float4 operator /(Float4 a, Int4 b)=>
    new(a.x / b.x,a.y / b.y,a.z / b.z,a.w / b.w);

public static Float4 operator /(Float4 a, Int b)=>
    new(a.x / b,a.y / b,a.z / b,a.w / b);

public static Float4 operator /(Int b, Float4 a)=>
    new(a.x / b,a.y / b,a.z / b,a.w / b);

    public Float this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,
2=>z,
3=>w,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;
case 2:z=value;break;
case 3:w=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 4);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Float4 Diagonal(Float _)
    {
        return new(_,_,_,_);
    }
    public static Float4 Basis(int axis,Float value)
    {
        var ret = new Float4();
        ret[axis] = value;
        return ret;
    }
}

public record struct Int2
(
    Int x,
Int y
){

public static Float2 operator +(Int2 a, Float2 b)=>
    new(a.x + b.x,a.y + b.y);

public static Float2 operator +(Int2 a, Float b)=>
    new(a.x + b,a.y + b);

public static Float2 operator +(Float b, Int2 a)=>
    new(a.x + b,a.y + b);

public static Float2 operator -(Int2 a, Float2 b)=>
    new(a.x - b.x,a.y - b.y);

public static Float2 operator -(Int2 a, Float b)=>
    new(a.x - b,a.y - b);

public static Float2 operator -(Float b, Int2 a)=>
    new(a.x - b,a.y - b);

public static Float2 operator *(Int2 a, Float2 b)=>
    new(a.x * b.x,a.y * b.y);

public static Float2 operator *(Int2 a, Float b)=>
    new(a.x * b,a.y * b);

public static Float2 operator *(Float b, Int2 a)=>
    new(a.x * b,a.y * b);

public Float2 Max(Float2 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y));

public Float2 Max(Float b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b));

public Float2 Min(Float2 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y));

public Float2 Min(Float b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b));

public static Int2 operator +(Int2 a, Int2 b)=>
    new(a.x + b.x,a.y + b.y);

public static Int2 operator +(Int2 a, Int b)=>
    new(a.x + b,a.y + b);

public static Int2 operator +(Int b, Int2 a)=>
    new(a.x + b,a.y + b);

public static Int2 operator -(Int2 a, Int2 b)=>
    new(a.x - b.x,a.y - b.y);

public static Int2 operator -(Int2 a, Int b)=>
    new(a.x - b,a.y - b);

public static Int2 operator -(Int b, Int2 a)=>
    new(a.x - b,a.y - b);

public static Int2 operator *(Int2 a, Int2 b)=>
    new(a.x * b.x,a.y * b.y);

public static Int2 operator *(Int2 a, Int b)=>
    new(a.x * b,a.y * b);

public static Int2 operator *(Int b, Int2 a)=>
    new(a.x * b,a.y * b);

public Int2 Max(Int2 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y));

public Int2 Max(Int b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b));

public Int2 Min(Int2 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y));

public Int2 Min(Int b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b));

public static Bool2 operator >(Int2 a, Float2 b)=>
    new(a.x > b.x,a.y > b.y);

public static Bool2 operator >(Int2 a, Float b)=>
    new(a.x > b,a.y > b);

public static Bool2 operator >(Float b, Int2 a)=>
    new(a.x > b,a.y > b);

public static Bool2 operator <(Int2 a, Float2 b)=>
    new(a.x < b.x,a.y < b.y);

public static Bool2 operator <(Int2 a, Float b)=>
    new(a.x < b,a.y < b);

public static Bool2 operator <(Float b, Int2 a)=>
    new(a.x < b,a.y < b);

public static Bool2 operator >=(Int2 a, Float2 b)=>
    new(a.x >= b.x,a.y >= b.y);

public static Bool2 operator >=(Int2 a, Float b)=>
    new(a.x >= b,a.y >= b);

public static Bool2 operator >=(Float b, Int2 a)=>
    new(a.x >= b,a.y >= b);

public static Bool2 operator <=(Int2 a, Float2 b)=>
    new(a.x <= b.x,a.y <= b.y);

public static Bool2 operator <=(Int2 a, Float b)=>
    new(a.x <= b,a.y <= b);

public static Bool2 operator <=(Float b, Int2 a)=>
    new(a.x <= b,a.y <= b);

public static Bool2 operator >(Int2 a, Int2 b)=>
    new(a.x > b.x,a.y > b.y);

public static Bool2 operator >(Int2 a, Int b)=>
    new(a.x > b,a.y > b);

public static Bool2 operator >(Int b, Int2 a)=>
    new(a.x > b,a.y > b);

public static Bool2 operator <(Int2 a, Int2 b)=>
    new(a.x < b.x,a.y < b.y);

public static Bool2 operator <(Int2 a, Int b)=>
    new(a.x < b,a.y < b);

public static Bool2 operator <(Int b, Int2 a)=>
    new(a.x < b,a.y < b);

public static Bool2 operator >=(Int2 a, Int2 b)=>
    new(a.x >= b.x,a.y >= b.y);

public static Bool2 operator >=(Int2 a, Int b)=>
    new(a.x >= b,a.y >= b);

public static Bool2 operator >=(Int b, Int2 a)=>
    new(a.x >= b,a.y >= b);

public static Bool2 operator <=(Int2 a, Int2 b)=>
    new(a.x <= b.x,a.y <= b.y);

public static Bool2 operator <=(Int2 a, Int b)=>
    new(a.x <= b,a.y <= b);

public static Bool2 operator <=(Int b, Int2 a)=>
    new(a.x <= b,a.y <= b);

public static Float2 operator /(Int2 a, Float2 b)=>
    new(a.x / b.x,a.y / b.y);

public static Float2 operator /(Int2 a, Float b)=>
    new(a.x / b,a.y / b);

public static Float2 operator /(Float b, Int2 a)=>
    new(a.x / b,a.y / b);

public static Float2 operator /(Int2 a, Int2 b)=>
    new(a.x / b.x,a.y / b.y);

public static Float2 operator /(Int2 a, Int b)=>
    new(a.x / b,a.y / b);

public static Float2 operator /(Int b, Int2 a)=>
    new(a.x / b,a.y / b);

    public Int this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 2);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Int2 Diagonal(Int _)
    {
        return new(_,_);
    }
    public static Int2 Basis(int axis,Int value)
    {
        var ret = new Int2();
        ret[axis] = value;
        return ret;
    }
}

public record struct Int3
(
    Int x,
Int y,
Int z
){

public static Float3 operator +(Int3 a, Float3 b)=>
    new(a.x + b.x,a.y + b.y,a.z + b.z);

public static Float3 operator +(Int3 a, Float b)=>
    new(a.x + b,a.y + b,a.z + b);

public static Float3 operator +(Float b, Int3 a)=>
    new(a.x + b,a.y + b,a.z + b);

public static Float3 operator -(Int3 a, Float3 b)=>
    new(a.x - b.x,a.y - b.y,a.z - b.z);

public static Float3 operator -(Int3 a, Float b)=>
    new(a.x - b,a.y - b,a.z - b);

public static Float3 operator -(Float b, Int3 a)=>
    new(a.x - b,a.y - b,a.z - b);

public static Float3 operator *(Int3 a, Float3 b)=>
    new(a.x * b.x,a.y * b.y,a.z * b.z);

public static Float3 operator *(Int3 a, Float b)=>
    new(a.x * b,a.y * b,a.z * b);

public static Float3 operator *(Float b, Int3 a)=>
    new(a.x * b,a.y * b,a.z * b);

public Float3 Max(Float3 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y),System.Math.Max(z,b.z));

public Float3 Max(Float b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b),System.Math.Max(z,b));

public Float3 Min(Float3 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y),System.Math.Min(z,b.z));

public Float3 Min(Float b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b),System.Math.Min(z,b));

public static Int3 operator +(Int3 a, Int3 b)=>
    new(a.x + b.x,a.y + b.y,a.z + b.z);

public static Int3 operator +(Int3 a, Int b)=>
    new(a.x + b,a.y + b,a.z + b);

public static Int3 operator +(Int b, Int3 a)=>
    new(a.x + b,a.y + b,a.z + b);

public static Int3 operator -(Int3 a, Int3 b)=>
    new(a.x - b.x,a.y - b.y,a.z - b.z);

public static Int3 operator -(Int3 a, Int b)=>
    new(a.x - b,a.y - b,a.z - b);

public static Int3 operator -(Int b, Int3 a)=>
    new(a.x - b,a.y - b,a.z - b);

public static Int3 operator *(Int3 a, Int3 b)=>
    new(a.x * b.x,a.y * b.y,a.z * b.z);

public static Int3 operator *(Int3 a, Int b)=>
    new(a.x * b,a.y * b,a.z * b);

public static Int3 operator *(Int b, Int3 a)=>
    new(a.x * b,a.y * b,a.z * b);

public Int3 Max(Int3 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y),System.Math.Max(z,b.z));

public Int3 Max(Int b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b),System.Math.Max(z,b));

public Int3 Min(Int3 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y),System.Math.Min(z,b.z));

public Int3 Min(Int b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b),System.Math.Min(z,b));

public static Bool3 operator >(Int3 a, Float3 b)=>
    new(a.x > b.x,a.y > b.y,a.z > b.z);

public static Bool3 operator >(Int3 a, Float b)=>
    new(a.x > b,a.y > b,a.z > b);

public static Bool3 operator >(Float b, Int3 a)=>
    new(a.x > b,a.y > b,a.z > b);

public static Bool3 operator <(Int3 a, Float3 b)=>
    new(a.x < b.x,a.y < b.y,a.z < b.z);

public static Bool3 operator <(Int3 a, Float b)=>
    new(a.x < b,a.y < b,a.z < b);

public static Bool3 operator <(Float b, Int3 a)=>
    new(a.x < b,a.y < b,a.z < b);

public static Bool3 operator >=(Int3 a, Float3 b)=>
    new(a.x >= b.x,a.y >= b.y,a.z >= b.z);

public static Bool3 operator >=(Int3 a, Float b)=>
    new(a.x >= b,a.y >= b,a.z >= b);

public static Bool3 operator >=(Float b, Int3 a)=>
    new(a.x >= b,a.y >= b,a.z >= b);

public static Bool3 operator <=(Int3 a, Float3 b)=>
    new(a.x <= b.x,a.y <= b.y,a.z <= b.z);

public static Bool3 operator <=(Int3 a, Float b)=>
    new(a.x <= b,a.y <= b,a.z <= b);

public static Bool3 operator <=(Float b, Int3 a)=>
    new(a.x <= b,a.y <= b,a.z <= b);

public static Bool3 operator >(Int3 a, Int3 b)=>
    new(a.x > b.x,a.y > b.y,a.z > b.z);

public static Bool3 operator >(Int3 a, Int b)=>
    new(a.x > b,a.y > b,a.z > b);

public static Bool3 operator >(Int b, Int3 a)=>
    new(a.x > b,a.y > b,a.z > b);

public static Bool3 operator <(Int3 a, Int3 b)=>
    new(a.x < b.x,a.y < b.y,a.z < b.z);

public static Bool3 operator <(Int3 a, Int b)=>
    new(a.x < b,a.y < b,a.z < b);

public static Bool3 operator <(Int b, Int3 a)=>
    new(a.x < b,a.y < b,a.z < b);

public static Bool3 operator >=(Int3 a, Int3 b)=>
    new(a.x >= b.x,a.y >= b.y,a.z >= b.z);

public static Bool3 operator >=(Int3 a, Int b)=>
    new(a.x >= b,a.y >= b,a.z >= b);

public static Bool3 operator >=(Int b, Int3 a)=>
    new(a.x >= b,a.y >= b,a.z >= b);

public static Bool3 operator <=(Int3 a, Int3 b)=>
    new(a.x <= b.x,a.y <= b.y,a.z <= b.z);

public static Bool3 operator <=(Int3 a, Int b)=>
    new(a.x <= b,a.y <= b,a.z <= b);

public static Bool3 operator <=(Int b, Int3 a)=>
    new(a.x <= b,a.y <= b,a.z <= b);

public static Float3 operator /(Int3 a, Float3 b)=>
    new(a.x / b.x,a.y / b.y,a.z / b.z);

public static Float3 operator /(Int3 a, Float b)=>
    new(a.x / b,a.y / b,a.z / b);

public static Float3 operator /(Float b, Int3 a)=>
    new(a.x / b,a.y / b,a.z / b);

public static Float3 operator /(Int3 a, Int3 b)=>
    new(a.x / b.x,a.y / b.y,a.z / b.z);

public static Float3 operator /(Int3 a, Int b)=>
    new(a.x / b,a.y / b,a.z / b);

public static Float3 operator /(Int b, Int3 a)=>
    new(a.x / b,a.y / b,a.z / b);

    public Int this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,
2=>z,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;
case 2:z=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 3);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Int3 Diagonal(Int _)
    {
        return new(_,_,_);
    }
    public static Int3 Basis(int axis,Int value)
    {
        var ret = new Int3();
        ret[axis] = value;
        return ret;
    }
}

public record struct Int4
(
    Int x,
Int y,
Int z,
Int w
){

public static Float4 operator +(Int4 a, Float4 b)=>
    new(a.x + b.x,a.y + b.y,a.z + b.z,a.w + b.w);

public static Float4 operator +(Int4 a, Float b)=>
    new(a.x + b,a.y + b,a.z + b,a.w + b);

public static Float4 operator +(Float b, Int4 a)=>
    new(a.x + b,a.y + b,a.z + b,a.w + b);

public static Float4 operator -(Int4 a, Float4 b)=>
    new(a.x - b.x,a.y - b.y,a.z - b.z,a.w - b.w);

public static Float4 operator -(Int4 a, Float b)=>
    new(a.x - b,a.y - b,a.z - b,a.w - b);

public static Float4 operator -(Float b, Int4 a)=>
    new(a.x - b,a.y - b,a.z - b,a.w - b);

public static Float4 operator *(Int4 a, Float4 b)=>
    new(a.x * b.x,a.y * b.y,a.z * b.z,a.w * b.w);

public static Float4 operator *(Int4 a, Float b)=>
    new(a.x * b,a.y * b,a.z * b,a.w * b);

public static Float4 operator *(Float b, Int4 a)=>
    new(a.x * b,a.y * b,a.z * b,a.w * b);

public Float4 Max(Float4 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y),System.Math.Max(z,b.z),System.Math.Max(w,b.w));

public Float4 Max(Float b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b),System.Math.Max(z,b),System.Math.Max(w,b));

public Float4 Min(Float4 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y),System.Math.Min(z,b.z),System.Math.Min(w,b.w));

public Float4 Min(Float b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b),System.Math.Min(z,b),System.Math.Min(w,b));

public static Int4 operator +(Int4 a, Int4 b)=>
    new(a.x + b.x,a.y + b.y,a.z + b.z,a.w + b.w);

public static Int4 operator +(Int4 a, Int b)=>
    new(a.x + b,a.y + b,a.z + b,a.w + b);

public static Int4 operator +(Int b, Int4 a)=>
    new(a.x + b,a.y + b,a.z + b,a.w + b);

public static Int4 operator -(Int4 a, Int4 b)=>
    new(a.x - b.x,a.y - b.y,a.z - b.z,a.w - b.w);

public static Int4 operator -(Int4 a, Int b)=>
    new(a.x - b,a.y - b,a.z - b,a.w - b);

public static Int4 operator -(Int b, Int4 a)=>
    new(a.x - b,a.y - b,a.z - b,a.w - b);

public static Int4 operator *(Int4 a, Int4 b)=>
    new(a.x * b.x,a.y * b.y,a.z * b.z,a.w * b.w);

public static Int4 operator *(Int4 a, Int b)=>
    new(a.x * b,a.y * b,a.z * b,a.w * b);

public static Int4 operator *(Int b, Int4 a)=>
    new(a.x * b,a.y * b,a.z * b,a.w * b);

public Int4 Max(Int4 b)=>
    new(System.Math.Max(x,b.x),System.Math.Max(y,b.y),System.Math.Max(z,b.z),System.Math.Max(w,b.w));

public Int4 Max(Int b)=>
    new(System.Math.Max(x,b),System.Math.Max(y,b),System.Math.Max(z,b),System.Math.Max(w,b));

public Int4 Min(Int4 b)=>
    new(System.Math.Min(x,b.x),System.Math.Min(y,b.y),System.Math.Min(z,b.z),System.Math.Min(w,b.w));

public Int4 Min(Int b)=>
    new(System.Math.Min(x,b),System.Math.Min(y,b),System.Math.Min(z,b),System.Math.Min(w,b));

public static Bool4 operator >(Int4 a, Float4 b)=>
    new(a.x > b.x,a.y > b.y,a.z > b.z,a.w > b.w);

public static Bool4 operator >(Int4 a, Float b)=>
    new(a.x > b,a.y > b,a.z > b,a.w > b);

public static Bool4 operator >(Float b, Int4 a)=>
    new(a.x > b,a.y > b,a.z > b,a.w > b);

public static Bool4 operator <(Int4 a, Float4 b)=>
    new(a.x < b.x,a.y < b.y,a.z < b.z,a.w < b.w);

public static Bool4 operator <(Int4 a, Float b)=>
    new(a.x < b,a.y < b,a.z < b,a.w < b);

public static Bool4 operator <(Float b, Int4 a)=>
    new(a.x < b,a.y < b,a.z < b,a.w < b);

public static Bool4 operator >=(Int4 a, Float4 b)=>
    new(a.x >= b.x,a.y >= b.y,a.z >= b.z,a.w >= b.w);

public static Bool4 operator >=(Int4 a, Float b)=>
    new(a.x >= b,a.y >= b,a.z >= b,a.w >= b);

public static Bool4 operator >=(Float b, Int4 a)=>
    new(a.x >= b,a.y >= b,a.z >= b,a.w >= b);

public static Bool4 operator <=(Int4 a, Float4 b)=>
    new(a.x <= b.x,a.y <= b.y,a.z <= b.z,a.w <= b.w);

public static Bool4 operator <=(Int4 a, Float b)=>
    new(a.x <= b,a.y <= b,a.z <= b,a.w <= b);

public static Bool4 operator <=(Float b, Int4 a)=>
    new(a.x <= b,a.y <= b,a.z <= b,a.w <= b);

public static Bool4 operator >(Int4 a, Int4 b)=>
    new(a.x > b.x,a.y > b.y,a.z > b.z,a.w > b.w);

public static Bool4 operator >(Int4 a, Int b)=>
    new(a.x > b,a.y > b,a.z > b,a.w > b);

public static Bool4 operator >(Int b, Int4 a)=>
    new(a.x > b,a.y > b,a.z > b,a.w > b);

public static Bool4 operator <(Int4 a, Int4 b)=>
    new(a.x < b.x,a.y < b.y,a.z < b.z,a.w < b.w);

public static Bool4 operator <(Int4 a, Int b)=>
    new(a.x < b,a.y < b,a.z < b,a.w < b);

public static Bool4 operator <(Int b, Int4 a)=>
    new(a.x < b,a.y < b,a.z < b,a.w < b);

public static Bool4 operator >=(Int4 a, Int4 b)=>
    new(a.x >= b.x,a.y >= b.y,a.z >= b.z,a.w >= b.w);

public static Bool4 operator >=(Int4 a, Int b)=>
    new(a.x >= b,a.y >= b,a.z >= b,a.w >= b);

public static Bool4 operator >=(Int b, Int4 a)=>
    new(a.x >= b,a.y >= b,a.z >= b,a.w >= b);

public static Bool4 operator <=(Int4 a, Int4 b)=>
    new(a.x <= b.x,a.y <= b.y,a.z <= b.z,a.w <= b.w);

public static Bool4 operator <=(Int4 a, Int b)=>
    new(a.x <= b,a.y <= b,a.z <= b,a.w <= b);

public static Bool4 operator <=(Int b, Int4 a)=>
    new(a.x <= b,a.y <= b,a.z <= b,a.w <= b);

public static Float4 operator /(Int4 a, Float4 b)=>
    new(a.x / b.x,a.y / b.y,a.z / b.z,a.w / b.w);

public static Float4 operator /(Int4 a, Float b)=>
    new(a.x / b,a.y / b,a.z / b,a.w / b);

public static Float4 operator /(Float b, Int4 a)=>
    new(a.x / b,a.y / b,a.z / b,a.w / b);

public static Float4 operator /(Int4 a, Int4 b)=>
    new(a.x / b.x,a.y / b.y,a.z / b.z,a.w / b.w);

public static Float4 operator /(Int4 a, Int b)=>
    new(a.x / b,a.y / b,a.z / b,a.w / b);

public static Float4 operator /(Int b, Int4 a)=>
    new(a.x / b,a.y / b,a.z / b,a.w / b);

    public Int this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,
2=>z,
3=>w,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;
case 2:z=value;break;
case 3:w=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 4);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Int4 Diagonal(Int _)
    {
        return new(_,_,_,_);
    }
    public static Int4 Basis(int axis,Int value)
    {
        var ret = new Int4();
        ret[axis] = value;
        return ret;
    }
}

public record struct Bool2
(
    Bool x,
Bool y
){

public Bool All=>
    x&&y;

public Bool Any=>
    x||y;

    public Bool this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 2);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Bool2 Diagonal(Bool _)
    {
        return new(_,_);
    }
    public static Bool2 Basis(int axis,Bool value)
    {
        var ret = new Bool2();
        ret[axis] = value;
        return ret;
    }
}

public record struct Bool3
(
    Bool x,
Bool y,
Bool z
){

public Bool All=>
    x&&y&&z;

public Bool Any=>
    x||y||z;

    public Bool this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,
2=>z,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;
case 2:z=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 3);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Bool3 Diagonal(Bool _)
    {
        return new(_,_,_);
    }
    public static Bool3 Basis(int axis,Bool value)
    {
        var ret = new Bool3();
        ret[axis] = value;
        return ret;
    }
}

public record struct Bool4
(
    Bool x,
Bool y,
Bool z,
Bool w
){

public Bool All=>
    x&&y&&z&&w;

public Bool Any=>
    x||y||z||w;

    public Bool this[int _]{
        get{
            return _ switch
            {
                0=>x,
1=>y,
2=>z,
3=>w,

                _ => throw new Exception()
            };
        }
        set
        {
            switch (_)
            {
                case 0:x=value;break;
case 1:y=value;break;
case 2:z=value;break;
case 3:w=value;break;

                default:
                    throw new Exception();
            }
        }
    }
    public static IEnumerable<int> AllAxis => Enumerable.Range(0, 4);
    public static IEnumerable<int> OtherAxis(int axis) => AllAxis.Where(a => a != axis);
    public static Bool4 Diagonal(Bool _)
    {
        return new(_,_,_,_);
    }
    public static Bool4 Basis(int axis,Bool value)
    {
        var ret = new Bool4();
        ret[axis] = value;
        return ret;
    }
}

