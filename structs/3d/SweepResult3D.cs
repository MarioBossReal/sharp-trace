namespace SharpTrace;

public struct SweepResult3D
{
    public bool IsColliding;
    public int CollisionCount;
    public float SafeFraction;
    public float UnsafeFraction;
    public TraceResult3D[] Collisions;
}
