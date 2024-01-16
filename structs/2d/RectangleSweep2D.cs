using Godot;
using System.Collections.Generic;

namespace SharpTrace;

public struct RectangleSweep2D
{
    public Vector2 origin = default;
    public Vector2 target = default;
    public bool collideWithAreas = false;
    public bool collideWithBodies = true;
    public uint collisionMask = 1;
    public float margin = 0f;
    public int maxResults = 1;

    public Vector2 size = Vector2.One;

    private List<Rid> exclusions = null;
    private int exclusionCount = 0;

    public RectangleSweep2D() { }

    public RectangleSweep2D From(Vector2 origin)
    {
        this.origin = origin;
        return this;
    }

    public RectangleSweep2D To(Vector2 target)
    {
        this.target = target;
        return this;
    }

    public RectangleSweep2D Overlap(Vector2 position)
    {
        this.origin = position;
        this.target = position;
        return this;
    }

    public RectangleSweep2D HitAreas(bool collideWithAreas)
    {
        this.collideWithAreas = collideWithAreas;
        return this;
    }

    public RectangleSweep2D HitBodies(bool collideWithBodies)
    {
        this.collideWithBodies = collideWithBodies;
        return this;
    }

    public RectangleSweep2D Mask(uint mask)
    {
        collisionMask = mask;
        return this;
    }

    public RectangleSweep2D Size(Vector2 size)
    {
        this.size = size;
        return this;
    }

    public RectangleSweep2D MaxResults(int maxResults)
    {
        this.maxResults = maxResults;
        return this;
    }

    public RectangleSweep2D Margin(float margin)
    {
        this.margin = margin;
        return this;
    }
    public RectangleSweep2D Exclude(Rid rid)
    {
        exclusions ??= new();
        exclusions.Add(rid);
        ++exclusionCount;
        return this;
    }

    public RectangleSweep2D Exclude(CollisionObject2D obj)
    {
        return Exclude(obj.GetRid());
    }

    public RectangleSweep2D ClearExclusions()
    {
        exclusions.Clear();
        exclusionCount = 0;
        return this;
    }

    public readonly SweepResult2D Trace()
    {
        GlobalShapeCast2D.ClearExceptions();

        for (int i = 0; i < exclusionCount; i++)
            GlobalShapeCast2D.AddExceptionRid(exclusions[i]);

        GlobalShapeCast2D.Origin = origin;
        GlobalShapeCast2D.GlobalTargetPosition = target;
        GlobalShapeCast2D.CollideWithAreas = collideWithAreas;
        GlobalShapeCast2D.CollideWithBodies = collideWithBodies;
        GlobalShapeCast2D.CollisionMask = collisionMask;
        GlobalShapeCast2D.MaxResults = maxResults;
        GlobalShapeCast2D.Margin = margin;
        GlobalShapeCast2D.TraceRectangle(size, out var result);
        return result;
    }
}
