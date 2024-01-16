using Godot;
using System.Collections.Generic;

namespace SharpTrace;

public struct BoxSweep3D
{
    public Vector3 origin = default;
    public Vector3 target = default;
    public bool collideWithAreas = false;
    public bool collideWithBodies = true;
    public uint collisionMask = 1;
    public float margin = 0f;
    public int maxResults = 1;

    public Vector3 size = Vector3.One;

    private List<Rid> exclusions = null;
    private int exclusionCount = 0;

    public BoxSweep3D() { }

    public BoxSweep3D From(Vector3 origin)
    {
        this.origin = origin;
        return this;
    }

    public BoxSweep3D To(Vector3 target)
    {
        this.target = target;
        return this;
    }

    public BoxSweep3D Overlap(Vector3 position)
    {
        this.origin = position;
        this.target = position;
        return this;
    }

    public BoxSweep3D HitAreas(bool collideWithAreas)
    {
        this.collideWithAreas = collideWithAreas;
        return this;
    }

    public BoxSweep3D HitBodies(bool collideWithBodies)
    {
        this.collideWithBodies = collideWithBodies;
        return this;
    }

    public BoxSweep3D Mask(uint mask)
    {
        collisionMask = mask;
        return this;
    }

    public BoxSweep3D Size(Vector3 size)
    {
        this.size = size;
        return this;
    }

    public BoxSweep3D MaxResults(int maxResults)
    {
        this.maxResults = maxResults;
        return this;
    }

    public BoxSweep3D Margin(float margin)
    {
        this.margin = margin;
        return this;
    }

    public BoxSweep3D Exclude(Rid rid)
    {
        exclusions ??= new();
        exclusions.Add(rid);
        ++exclusionCount;
        return this;
    }

    public BoxSweep3D Exclude(CollisionObject3D obj)
    {
        return Exclude(obj.GetRid());
    }

    public BoxSweep3D ClearExclusions()
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
        GlobalShapeCast3D.GlobalTargetPosition = target;
        GlobalShapeCast3D.CollideWithAreas = collideWithAreas;
        GlobalShapeCast3D.CollideWithBodies = collideWithBodies;
        GlobalShapeCast3D.CollisionMask = collisionMask;
        GlobalShapeCast3D.MaxResults = maxResults;
        GlobalShapeCast3D.Margin = margin;
        GlobalShapeCast3D.TraceBox(size, out var result);
        return result;
    }
}
