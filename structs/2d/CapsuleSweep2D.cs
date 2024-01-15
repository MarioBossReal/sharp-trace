using Godot;
using System.Collections.Generic;

namespace SharpTrace;

public struct CapsuleSweep2D
{
    public Vector2 origin = default;
    public Vector2 target = default;
    public bool collideWithAreas = false;
    public bool collideWithBodies = true;
    public uint collisionMask = 1;
    public float margin = 0f;
    public int maxResults = 1;

    public float radius = 5f;
    public float height = 20f;

    private List<Rid> exclusions = null;
    private int exclusionCount = 0;

    public CapsuleSweep2D() { }

    public CapsuleSweep2D From(Vector2 origin)
    {
        this.origin = origin;
        return this;
    }

    public CapsuleSweep2D To(Vector2 target)
    {
        this.target = target;
        return this;
    }

    public CapsuleSweep2D Overlap(Vector2 position)
    {
        this.origin = position;
        this.target = position;
        return this;
    }

    public CapsuleSweep2D HitAreas(bool collideWithAreas)
    {
        this.collideWithAreas = collideWithAreas;
        return this;
    }

    public CapsuleSweep2D HitBodies(bool collideWithBodies)
    {
        this.collideWithBodies = collideWithBodies;
        return this;
    }

    public CapsuleSweep2D Mask(uint mask)
    {
        collisionMask = mask;
        return this;
    }

    public CapsuleSweep2D Radius(float radius)
    {
        this.radius = radius;
        return this;
    }

    public CapsuleSweep2D Height(float height)
    {
        this.height = height;
        return this;
    }

    public CapsuleSweep2D MaxResults(int maxResults)
    {
        this.maxResults = maxResults;
        return this;
    }

    public CapsuleSweep2D Exclude(Rid rid)
    {
        exclusions ??= new();
        exclusions.Add(rid);
        ++exclusionCount;
        return this;
    }

    public CapsuleSweep2D Exclude(CollisionObject2D obj)
    {
        return Exclude(obj.GetRid());
    }

    public CapsuleSweep2D ClearExclusions()
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
        GlobalShapeCast2D.TargetPosition = target;
        GlobalShapeCast2D.CollideWithAreas = collideWithAreas;
        GlobalShapeCast2D.CollideWithBodies = collideWithBodies;
        GlobalShapeCast2D.CollisionMask = collisionMask;
        GlobalShapeCast2D.MaxResults = maxResults;
        GlobalShapeCast2D.TraceCapsule(radius, height, out var result);
        return result;
    }
}
