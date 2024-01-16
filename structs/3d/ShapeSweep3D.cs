using Godot;
using System.Collections.Generic;

namespace SharpTrace;

public struct ShapeSweep3D
{
    public Vector3 origin = default;
    public Vector3 target = default;
    public bool collideWithAreas = false;
    public bool collideWithBodies = true;
    public uint collisionMask = 1;
    public float margin = 0f;
    public int maxResults = 1;

    public Shape3D shape = null;

    private List<Rid> exclusions = null;
    private int exclusionCount = 0;

    public ShapeSweep3D() { }

    public ShapeSweep3D From(Vector3 origin)
    {
        this.origin = origin;
        return this;
    }

    public ShapeSweep3D To(Vector3 target)
    {
        this.target = target;
        return this;
    }

    public ShapeSweep3D Overlap(Vector3 position)
    {
        this.origin = position;
        this.target = position;
        return this;
    }

    public ShapeSweep3D HitAreas(bool collideWithAreas)
    {
        this.collideWithAreas = collideWithAreas;
        return this;
    }

    public ShapeSweep3D HitBodies(bool collideWithBodies)
    {
        this.collideWithBodies = collideWithBodies;
        return this;
    }

    public ShapeSweep3D Mask(uint mask)
    {
        collisionMask = mask;
        return this;
    }

    public ShapeSweep3D MaxResults(int maxResults)
    {
        this.maxResults = maxResults;
        return this;
    }

    public ShapeSweep3D Margin(float margin)
    {
        this.margin = margin;
        return this;
    }

    public ShapeSweep3D Exclude(Rid rid)
    {
        exclusions ??= new();
        exclusions.Add(rid);
        ++exclusionCount;
        return this;
    }

    public ShapeSweep3D Exclude(CollisionObject3D obj)
    {
        return Exclude(obj.GetRid());
    }

    public ShapeSweep3D ClearExclusions()
    {
        exclusions.Clear();
        exclusionCount = 0;
        return this;
    }

    public ShapeSweep3D Shape(Shape3D shape)
    {
        this.shape = shape;
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
        GlobalShapeCast3D.Trace(shape, out var result);
        return result;
    }
}
