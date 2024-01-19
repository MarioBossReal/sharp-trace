using Godot;

namespace SharpTrace;

public struct TraceResult2D
{
    public bool IsColliding;
    public GodotObject Collider;
    public Rid ColliderRid;
    public int ColliderShape;
    public Vector2 Normal;
    public Vector2 Point;
    public Vector2 Origin;
}
