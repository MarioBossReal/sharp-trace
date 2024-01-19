using Godot;

namespace SharpTrace;

public struct SweepResult3D
{
    public bool IsColliding;
    public int CollisionCount;
    public float SafeFraction;
    public float UnsafeFraction;
    public Vector3 Origin;
    public Vector3 SafeEndPoint;
    public Vector3 UnsafeEndPoint;
    public TraceResult3D[] Collisions;
}
