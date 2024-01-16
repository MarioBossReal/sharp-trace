using Godot;
using System.Collections.Generic;

namespace SharpTrace;

public struct Ray2D
{
    public Vector2 origin = default;
    public Vector2 target = default;
    public bool collideWithAreas = false;
    public bool collideWithBodies = true;
    public uint collisionMask = 1;
    public bool hitFromInside = false;

    private List<Rid> exclusions = null;
    private int exclusionCount = 0;

    public Ray2D() { }

    public Ray2D From(Vector2 origin)
    {
        this.origin = origin;
        return this;
    }

    public Ray2D To(Vector2 target)
    {
        this.target = target;
        return this;
    }

    public Ray2D HitAreas(bool collideWithAreas)
    {
        this.collideWithAreas = collideWithAreas;
        return this;
    }

    public Ray2D HitBodies(bool collideWithBodies)
    {
        this.collideWithBodies = collideWithBodies;
        return this;
    }

    public Ray2D Mask(uint mask)
    {
        collisionMask = mask;
        return this;
    }

    public Ray2D HitFromInside(bool hitFromInside)
    {
        this.hitFromInside = hitFromInside;
        return this;
    }

    public Ray2D Exclude(Rid rid)
    {
        exclusions ??= new();
        exclusions.Add(rid);
        ++exclusionCount;
        return this;
    }

    public Ray2D Exclude(CollisionObject2D obj)
    {
        return Exclude(obj.GetRid());
    }

    public Ray2D ClearExclusions()
    {
        exclusions.Clear();
        exclusionCount = 0;
        return this;
    }

    public readonly TraceResult2D Trace()
    {
        GlobalRayCast2D.ClearExceptions();

        for (int i = 0; i < exclusionCount; i++)
            GlobalRayCast2D.AddExceptionRid(exclusions[i]);

        GlobalRayCast2D.Origin = origin;
        GlobalRayCast2D.GlobalTargetPosition = target;
        GlobalRayCast2D.CollideWithAreas = collideWithAreas;
        GlobalRayCast2D.CollideWithBodies = collideWithBodies;
        GlobalRayCast2D.CollisionMask = collisionMask;
        GlobalRayCast2D.HitFromInside = hitFromInside;
        GlobalRayCast2D.Trace(out var result);
        return result;
    }

}
