using Godot;
using System.Collections.Generic;

namespace SharpTrace;

public struct CapsuleSweep3D
{
    public Vector3 origin = default;
    public Vector3 target = default;
    public bool collideWithAreas = false;
    public bool collideWithBodies = true;
    public uint collisionMask = 1;
    public float margin = 0f;
    public int maxResults = 1;

    public float radius = 0.5f;
    public float height = 2f;

    private List<Rid> exclusions = null;
    private int exclusionCount = 0;

    public CapsuleSweep3D() { }

    public CapsuleSweep3D From(Vector3 origin)
    {
        this.origin = origin;
        return this;
    }

    public CapsuleSweep3D To(Vector3 target)
    {
        this.target = target;
        return this;
    }

    public CapsuleSweep3D Overlap(Vector3 position)
    {
        this.origin = position;
        this.target = position;
        return this;
    }

    public CapsuleSweep3D HitAreas(bool collideWithAreas)
    {
        this.collideWithAreas = collideWithAreas;
        return this;
    }

    public CapsuleSweep3D HitBodies(bool collideWithBodies)
    {
        this.collideWithBodies = collideWithBodies;
        return this;
    }

    public CapsuleSweep3D Mask(uint mask)
    {
        collisionMask = mask;
        return this;
    }

    public CapsuleSweep3D Radius(float radius)
    {
        this.radius = radius;
        return this;
    }

    public CapsuleSweep3D Height(float height)
    {
        this.height = height;
        return this;
    }

    public CapsuleSweep3D MaxResults(int maxResults)
    {
        this.maxResults = maxResults;
        return this;
    }

    public CapsuleSweep3D Margin(float margin)
    {
        this.margin = margin;
        return this;
    }

    public CapsuleSweep3D Exclude(Rid rid)
    {
        exclusions ??= new();
        exclusions.Add(rid);
        ++exclusionCount;
        return this;
    }

    public CapsuleSweep3D Exclude(CollisionObject3D obj)
    {
        return Exclude(obj.GetRid());
    }

    public CapsuleSweep3D ClearExclusions()
    {
        exclusions.Clear();
        exclusionCount = 0;
        return this;
    }

    public readonly SweepResult3D Trace()
    {
        GlobalShapeCast3D.ClearExceptions();

        for (int i = 0; i < exclusionCount; i++)
            GlobalShapeCast3D.AddExceptionRid(exclusions[i]);

        GlobalShapeCast3D.Origin = origin;
        GlobalShapeCast3D.TargetPosition = target;
        GlobalShapeCast3D.CollideWithAreas = collideWithAreas;
        GlobalShapeCast3D.CollideWithBodies = collideWithBodies;
        GlobalShapeCast3D.CollisionMask = collisionMask;
        GlobalShapeCast3D.MaxResults = maxResults;
        GlobalShapeCast3D.Margin = margin;
        GlobalShapeCast3D.TraceCapsule(radius, height, out var result);
        return result;
    }
}
