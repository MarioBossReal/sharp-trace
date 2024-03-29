using Godot;

namespace SharpTrace;

public sealed partial class GlobalRayCast2D : RayCast2D
{
    private static RayCast2D _instance;
    private static GlobalRayCast2D _castedInstance;
    public static new bool CollideWithAreas { get => _instance.CollideWithAreas; set => _instance.CollideWithAreas = value; }
    public static new bool CollideWithBodies { get => _instance.CollideWithBodies; set => _instance.CollideWithBodies = value; }
    public static new uint CollisionMask { get => _instance.CollisionMask; set => _instance.CollisionMask = value; }
    public static new bool HitFromInside { get => _instance.HitFromInside; set => _instance.HitFromInside = value; }
    public static Vector2 Origin { get => _instance.GlobalPosition; set => _instance.GlobalPosition = value; }
    public static Vector2 GlobalTargetPosition { get => _castedInstance._globalTargetPosition; set => _castedInstance._globalTargetPosition = value; }

    private Vector2 _globalTargetPosition;

    public override void _Ready()
    {
        if (_instance != null)
        {
            GD.Print("Only one instance of type GlobalRayCast2D may exist. Freeing object...");
            QueueFree();
            return;
        }

        Enabled = false;
        RotationDegrees = 0;
        _instance ??= this;
        _castedInstance ??= this;
    }

    public static bool Trace(out TraceResult2D result)
    {
        result = new TraceResult2D();

        _instance.TargetPosition = _instance.ToLocal(GlobalTargetPosition);

        _instance.ForceRaycastUpdate();

        result.IsColliding = _instance.IsColliding();
        result.Origin = Origin;

        if (!result.IsColliding)
        {
            result.Normal = Vector2.Zero;
            result.Point = GlobalTargetPosition;
            return false;
        }

        result.Collider = _instance.GetCollider();
        result.ColliderRid = _instance.GetColliderRid();
        result.ColliderShape = _instance.GetColliderShape();
        result.Normal = _instance.GetCollisionNormal();
        result.Point = _instance.GetCollisionPoint();

        return true;
    }

    public static bool Trace(Vector2 origin, Vector2 target, out TraceResult2D result)
    {
        Origin = origin;
        GlobalTargetPosition = target;

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
