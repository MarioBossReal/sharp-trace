using Godot;

namespace SharpTrace;

public sealed partial class GlobalRayCast3D : RayCast3D
{
    private static RayCast3D _instance;
    private static GlobalRayCast3D _castedInstance;

    public static new bool CollideWithAreas { get => _instance.CollideWithAreas; set => _instance.CollideWithAreas = value; }
    public static new bool CollideWithBodies { get => _instance.CollideWithBodies; set => _instance.CollideWithBodies = value; }
    public static new uint CollisionMask { get => _instance.CollisionMask; set => _instance.CollisionMask = value; }
    public static new bool HitBackFaces { get => _instance.HitBackFaces; set => _instance.HitBackFaces = value; }
    public static new bool HitFromInside { get => _instance.HitFromInside; set => _instance.HitFromInside = value; }
    public static Vector3 Origin { get => _instance.GlobalPosition; set => _instance.GlobalPosition = value; }
    public static Vector3 GlobalTargetPosition { get => _castedInstance._globalTargetPosition; set => _castedInstance._globalTargetPosition = value; }

    private Vector3 _globalTargetPosition;

    public override void _Ready()
    {
        if (_instance != null)
        {
            GD.Print("Only one instance of type GlobalRayCast3D may exist. Freeing object...");
            QueueFree();
            return;
        }

        Enabled = false;
        Basis = Basis.Identity;
        _instance ??= this;
        _castedInstance ??= this;
    }

    public static bool Trace(out TraceResult3D result)
    {
        result = new();

        _instance.TargetPosition = _instance.ToLocal(GlobalTargetPosition);

        _instance.ForceRaycastUpdate();

        result.IsColliding = _instance.IsColliding();
        result.Origin = Origin;

        if (!result.IsColliding)
        {
            result.Normal = Vector3.Zero;
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

    public static bool Trace(Vector3 origin, Vector3 target, out TraceResult3D result)
    {
        Origin = origin;
        GlobalTargetPosition = target;

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
