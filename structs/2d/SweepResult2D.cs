using Godot;

namespace SharpTrace;

public struct SweepResult2D
{
    public bool IsColliding;
    public int CollisionCount;
    public float SafeFraction;
    public float UnsafeFraction;
    public Vector2 SafeEndPoint;
    public Vector2 UnsafeEndPoint;
    public TraceResult2D[] Collisions;
}
