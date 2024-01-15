using Godot;

namespace SharpTrace;

public static class Trace3D
{
    public static Ray3D Ray() => new();
    public static BoxSweep3D Box() => new();
    public static SphereSweep3D Sphere() => new();
    public static CapsuleSweep3D Capsule() => new();
    public static ShapeSweep3D Sweep() => new();

    public static Ray3D Ray(Vector3 from, Vector3 to)
        => Ray().From(from).To(to);

    public static BoxSweep3D Box(Vector3 from, Vector3 to)
        => Box().From(from).To(to);

    public static SphereSweep3D Sphere(Vector3 from, Vector3 to)
        => Sphere().From(from).To(to);

    public static CapsuleSweep3D Capsule(Vector3 from, Vector3 to)
        => Capsule().From(from).To(to);

    public static BoxSweep3D Box(Vector3 overlapPosition)
        => Box().Overlap(overlapPosition);

    public static SphereSweep3D Sphere(Vector3 overlapPosition)
        => Sphere().Overlap(overlapPosition);

    public static CapsuleSweep3D Capsule(Vector3 overlapPosition)
        => Capsule().Overlap(overlapPosition);

    public static ShapeSweep3D Sweep(Shape3D shape)
        => Sweep().Shape(shape);

    public static ShapeSweep3D Sweep(Vector3 from, Vector3 to)
        => Sweep().From(from).To(to);

    public static ShapeSweep3D Sweep(Shape3D shape, Vector3 from, Vector3 to)
        => Sweep().Shape(shape).From(from).To(to);

    public static ShapeSweep3D Sweep(Vector3 overlapPosition)
        => Sweep().Overlap(overlapPosition);

    public static ShapeSweep3D Sweep(Shape3D shape, Vector3 overlapPosition)
        => Sweep().Shape(shape).Overlap(overlapPosition);
}
