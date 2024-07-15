using System.Numerics;

namespace Michelangelo.Math;

public static class General
{

    static public float Max(float a, float b) => System.Math.Max(a, b);
    static public int Max(int a, int b) => System.Math.Max(a, b);
    static public float Abs(this float _) => System.Math.Abs(_);
    static public int Abs(this int _) => System.Math.Abs(_);
    public const double PI = System.Math.PI;
}
public record struct Sphere
(
    Float3 origin,
    float radius
);
public record struct AxisPlane
(
    int axis,
    float constant
);
public record struct Ray
(
    Float3 origin,
    Float3 direction
)
{
    public bool Intersect(AxisPlane axisPlane, out Float3 intersection)
    {
        var delta = axisPlane.constant - origin[axisPlane.axis];
        var axisDirection = direction[axisPlane.axis];
        intersection = origin + direction * delta / axisDirection;
        return axisDirection != 0 && delta * axisDirection >= 0;
    }
}
public record struct Range
(
    float min,
    float max
)
{
    public bool Contain(float _) => _ >= min && _ <= max;
}
public record struct AxisBox3D
(
    Float3 min,
    Float3 max
)
{
    public static AxisBox3D FromSphere(Sphere _) =>
        new(_.origin - Float3.Diagonal(_.radius), _.origin + Float3.Diagonal(_.radius));
    public Range this[int _] => new(min[_], max[_]);
    public void Expand(float _)
    {
        min -= Float3.Diagonal(_);
        max += Float3.Diagonal(_);
    }
    public bool Contain(AxisBox3D _) =>

        (max >= _.max).All && (min <= _.min).All;
    public bool Contain(Float3 _) =>

        (max >= _).All && (min <= _).All;

    public bool Contain(Float3 _, IEnumerable<int> axis)
    {
        var self = this;
        return axis.All(a => self[a].Contain(_[a]));
    }

    public void Encapsulate(AxisBox3D _)
    {
        max = max.Max(_.max);
        min = min.Min(_.min);
    }
    public bool Intersect(AxisBox3D _) =>

        (max > _.min).All && (_.max > min).All;
    bool IntersectAxis(Ray ray, int axis)
    {
        var plane0 = new AxisPlane(axis, min[axis]);
        var plane1 = new AxisPlane(axis, max[axis]);
        bool intersected0 = ray.Intersect(plane0, out var intersection0);
        bool intersected1 = ray.Intersect(plane0, out var intersection1);
        if (!(intersected0 && intersected0)) { return false; }
        var otherAxis = Float3.OtherAxis(axis);
        return Contain(intersection0, otherAxis) || Contain(intersection1, otherAxis);
    }
    public bool Intersect(Ray _) =>
        Contain(_.origin) || IntersectAxis(_, 0) || IntersectAxis(_, 1) || IntersectAxis(_, 2);



    public Float3 Size => max - min;
    public float SurfaceArea => 2 * (Size.x * Size.y + Size.x * Size.z + Size.y * Size.z);
}
