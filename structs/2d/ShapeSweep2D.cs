using Godot;
using System.Collections.Generic;

namespace SharpTrace;

public struct ShapeSweep2D
{
    public Vector2 origin = default;
    public Vector2 target = default;
    public bool collideWithAreas = false;
    public bool collideWithBodies = true;
    public uint collisionMask = 1;
    public float margin = 0f;
    public int maxResults = 1;

    public Shape2D shape = null;

    private List<Rid> exclusions = null;
    private int exclusionCount = 0;

    public ShapeSweep2D() { }

    public ShapeSweep2D From(Vector2 origin)
    {
        this.origin = origin;
        return this;
    }

    public ShapeSweep2D To(Vector2 target)
    {
        this.target = target;
        return this;
    }

    public ShapeSweep2D Overlap(Vector2 position)
    {
        this.origin = position;
        this.target = position;
        return this;
    }

    public ShapeSweep2D HitAreas(bool collideWithAreas)
    {
        this.collideWithAreas = collideWithAreas;
        return this;
    }

    public ShapeSweep2D HitBodies(bool collideWithBodies)
    {
        this.collideWithBodies = collideWithBodies;
        return this;
    }

    public ShapeSweep2D Mask(uint mask)
    {
        collisionMask = mask;
        return this;
    }

    public ShapeSweep2D MaxResults(int maxResults)
    {
        this.maxResults = maxResults;
        return this;
    }

    public ShapeSweep2D Margin(float margin)
    {
        this.margin = margin;
        return this;
    }

    public ShapeSweep2D Exclude(Rid rid)
    {
        exclusions ??= new();
        exclusions.Add(rid);
        ++exclusionCount;
        return this;
    }

    public ShapeSweep2D Exclude(CollisionObject2D obj)
    {
        return Exclude(obj.GetRid());
    }

    public ShapeSweep2D ClearExclusions()
    {
        exclusions.Clear();
        exclusionCount = 0;
        return this;
    }

    public ShapeSweep2D Shape(Shape2D shape)
    {
        this.shape = shape;
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
        GlobalShapeCast2D.Trace(shape, out var result);
        return result;
    }
}
