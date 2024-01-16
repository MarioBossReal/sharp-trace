using Godot;

namespace SharpTrace;

public sealed partial class GlobalShapeCast3D : ShapeCast3D
{
    private static ShapeCast3D _instance;
    private static GlobalShapeCast3D _castedInstance;

    public static new bool CollideWithAreas { get => _instance.CollideWithAreas; set => _instance.CollideWithAreas = value; }
    public static new bool CollideWithBodies { get => _instance.CollideWithBodies; set => _instance.CollideWithBodies = value; }
    public static new uint CollisionMask { get => _instance.CollisionMask; set => _instance.CollisionMask = value; }
    public static new float Margin { get => _instance.Margin; set => _instance.Margin = value; }
    public static new int MaxResults { get => _instance.MaxResults; set => _instance.MaxResults = value; }
    public static Vector3 Origin { get => _instance.GlobalPosition; set => _instance.GlobalPosition = value; }
    public static Vector3 GlobalTargetPosition { get => _castedInstance._globalTargetPosition; set => _castedInstance._globalTargetPosition = value; }

    private BoxShape3D _internalBoxShape;
    private SphereShape3D _internalSphereShape;
    private CapsuleShape3D _internalCapsuleShape;

    private Vector3 _globalTargetPosition;

    public override void _Ready()
    {
        if (_instance != null)
        {
            GD.Print("Only one instance of type GlobalShapeCast3D may exist. Freeing object...");
            QueueFree();
            return;
        }

        Enabled = false;
        Basis = Basis.Identity;

        _internalBoxShape = new();
        _internalSphereShape = new();
        _internalCapsuleShape = new();

        _instance ??= this;
        _castedInstance ??= this;
    }

    public static bool Trace(out SweepResult3D result)
    {
        result = new();

        _instance.TargetPosition = GlobalTargetPosition - Origin;

        _instance.ForceShapecastUpdate();

        result.IsColliding = _instance.IsColliding();

        if (!result.IsColliding)
            return false;

        result.CollisionCount = _instance.GetCollisionCount();
        result.SafeFraction = _instance.GetClosestCollisionSafeFraction();
        result.UnsafeFraction = _instance.GetClosestCollisionUnsafeFraction();
        result.SafeEndPoint = Origin + (result.SafeFraction * _instance.TargetPosition);
        result.UnsafeEndPoint = Origin + (result.UnsafeFraction * _instance.TargetPosition);

        var traceResults = new TraceResult3D[result.CollisionCount];

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

    public static bool Trace(Shape3D shape, out SweepResult3D result)
    {
        _instance.Shape = shape;
        return Trace(out result);
    }

    public static bool TraceBox(Vector3 size, out SweepResult3D result)
    {
        _castedInstance._internalBoxShape.Size = size;
        _instance.Shape = _castedInstance._internalBoxShape;
        return Trace(out result);
    }

    public static bool TraceSphere(float radius, out SweepResult3D result)
    {
        _castedInstance._internalSphereShape.Radius = radius;
        _instance.Shape = _castedInstance._internalSphereShape;
        return Trace(out result);
    }

    public static bool TraceCapsule(float radius, float height, out SweepResult3D result)
    {
        _castedInstance._internalCapsuleShape.Radius = radius;
        _castedInstance._internalCapsuleShape.Height = height;
        _instance.Shape = _castedInstance._internalCapsuleShape;
        return Trace(out result);
    }

    public static new void AddException(CollisionObject3D node)
    {
        _instance.AddException(node);
    }

    public static new void AddExceptionRid(Rid rid)
    {
        _instance.AddExceptionRid(rid);
    }

    public static new void RemoveException(CollisionObject3D node)
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