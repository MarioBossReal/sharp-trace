using Godot;

namespace SharpTrace;

public sealed partial class GlobalShapeCast2D : ShapeCast2D
{
    private static ShapeCast2D _instance;
    private static GlobalShapeCast2D _castedInstance;

    public static new bool CollideWithAreas { get => _instance.CollideWithAreas; set => _instance.CollideWithAreas = value; }
    public static new bool CollideWithBodies { get => _instance.CollideWithBodies; set => _instance.CollideWithBodies = value; }
    public static new uint CollisionMask { get => _instance.CollisionMask; set => _instance.CollisionMask = value; }
    public static new float Margin { get => _instance.Margin; set => _instance.Margin = value; }
    public static new int MaxResults { get => _instance.MaxResults; set => _instance.MaxResults = value; }
    public static new Vector2 TargetPosition { get => _instance.TargetPosition; set => _instance.TargetPosition = value; }
    public static Vector2 Origin { get => _instance.GlobalPosition; set => _instance.GlobalPosition = value; }

    private RectangleShape2D _internalRectangleShape;
    private CircleShape2D _internalCircleShape;
    private CapsuleShape2D _internalCapsuleShape;

    public override void _Ready()
    {
        if (_instance != null)
        {
            GD.Print("Only one instance of type GlobalShapeCast2D may exist. Freeing object...");
            QueueFree();
            return;
        }

        Enabled = false;
        RotationDegrees = 0;

        _internalRectangleShape = new();
        _internalCircleShape = new();
        _internalCapsuleShape = new();

        _instance ??= this;
        _castedInstance ??= this;
    }

    public static bool Trace(out SweepResult2D result)
    {
        result = new();

        _instance.ForceShapecastUpdate();

        result.IsColliding = _instance.IsColliding();

        if (!result.IsColliding)
            return false;

        result.CollisionCount = _instance.GetCollisionCount();
        result.SafeFraction = _instance.GetClosestCollisionSafeFraction();
        result.UnsafeFraction = _instance.GetClosestCollisionUnsafeFraction();

        var traceResults = new TraceResult2D[result.CollisionCount];

        for (int i = 0; i < result.CollisionCount; i++)
        {
            traceResults[i].IsColliding = true;
            traceResults[i].Collider = _instance.GetCollider(i);
            traceResults[i].ColliderRid = _instance.GetColliderRid(i);
            traceResults[i].ColliderShape = _instance.GetColliderShape(i);
            traceResults[i].Point = _instance.GetCollisionPoint(i);
            traceResults[i].Normal = _instance.GetCollisionNormal(i);
        }

        result.Collisions = traceResults;

        return true;
    }

    public static bool Trace(Shape2D shape, out SweepResult2D result)
    {
        _instance.Shape = shape;
        return Trace(out result);
    }

    public static bool TraceRectangle(Vector2 size, out SweepResult2D result)
    {
        _castedInstance._internalRectangleShape.Size = size;
        _instance.Shape = _castedInstance._internalRectangleShape;
        return Trace(out result);
    }

    public static bool TraceCircle(float radius, out SweepResult2D result)
    {
        _castedInstance._internalCircleShape.Radius = radius;
        _instance.Shape = _castedInstance._internalCircleShape;
        return Trace(out result);
    }

    public static bool TraceCapsule(float radius, float height, out SweepResult2D result)
    {
        _castedInstance._internalCapsuleShape.Radius = radius;
        _castedInstance._internalCapsuleShape.Height = height;
        _instance.Shape = _castedInstance._internalCapsuleShape;
        return Trace(out result);
    }

    public static new void AddException(CollisionObject2D node)
    {
        _instance.AddException(node);
    }

    public static new void AddExceptionRid(Rid rid)
    {
        _instance.AddExceptionRid(rid);
    }

    public static new void RemoveException(CollisionObject2D node)
    {
        _instance.RemoveException(node);
    }

    public static new void RemoveExceptionRid(Rid rid)
    {
        _instance.RemoveExceptionRid(rid);
    }

    public static new void ClearExceptions()
    {
        _instance.ClearExceptions();
    }

    public static new bool GetCollisionMaskValue(int layerNumber)
    {
        return _instance.GetCollisionMaskValue(layerNumber);
    }

    public static new void SetCollisionMaskValue(int layerNumber, bool value)
    {
        _instance.SetCollisionMaskValue(layerNumber, value);
    }
}