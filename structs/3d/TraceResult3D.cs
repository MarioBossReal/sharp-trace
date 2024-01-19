using Godot;

namespace SharpTrace;

public struct TraceResult3D
{
    public bool IsColliding;
    public GodotObject Collider;
    public Rid ColliderRid;
    public int ColliderShape;
    public Vector3 Normal;
    public Vector3 Point;
    public Vector3 Origin;
}
