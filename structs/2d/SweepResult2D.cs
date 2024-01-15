namespace SharpTrace;

public struct SweepResult2D
{
    public bool IsColliding;
    public int CollisionCount;
    public float SafeFraction;
    public float UnsafeFraction;
    public TraceResult2D[] Collisions;
}
