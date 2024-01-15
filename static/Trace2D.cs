using Godot;

namespace SharpTrace;

public static class Trace2D
{
    public static Ray2D Ray() => new();
    public static RectangleSweep2D Rectangle() => new();
    public static CircleSweep2D Circle() => new();
    public static CapsuleSweep2D Capsule() => new();
    public static ShapeSweep2D Sweep() => new();

    public static Ray2D Ray(Vector2 from, Vector2 to)
        => Ray().From(from).To(to);

    public static RectangleSweep2D Rectangle(Vector2 from, Vector2 to)
        => Rectangle().From(from).To(to);

    public static CircleSweep2D Circle(Vector2 from, Vector2 to)
        => Circle().From(from).To(to);

    public static CapsuleSweep2D Capsule(Vector2 from, Vector2 to)
        => Capsule().From(from).To(to);

    public static RectangleSweep2D Rectangle(Vector2 overlapPosition)
        => Rectangle().Overlap(overlapPosition);

    public static CircleSweep2D Circle(Vector2 overlapPosition)
        => Circle().Overlap(overlapPosition);

    public static CapsuleSweep2D Capsule(Vector2 overlapPosition)
        => Capsule().Overlap(overlapPosition);

    public static ShapeSweep2D Sweep(Shape2D shape)
        => Sweep().Shape(shape);

    public static ShapeSweep2D Sweep(Vector2 from, Vector2 to)
        => Sweep().From(from).To(to);

    public static ShapeSweep2D Sweep(Shape2D shape, Vector2 from, Vector2 to)
        => Sweep().Shape(shape).From(from).To(to);

    public static ShapeSweep2D Sweep(Vector2 overlapPosition)
        => Sweep().Overlap(overlapPosition);

    public static ShapeSweep2D Sweep(Shape2D shape, Vector2 overlapPosition)
        => Sweep().Shape(shape).Overlap(overlapPosition);
}
