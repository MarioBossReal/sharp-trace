using Godot;
using System.Collections.Generic;

namespace SharpTrace;

public struct Ray3D
{
    public Vector3 origin = default;
    public Vector3 target = default;
    public bool collideWithAreas = false;
    public bool collideWithBodies = true;
    public uint collisionMask = 1;
    public bool hitFromInside = false;
    public bool hitBackFaces = false;

    private List<Rid> exclusions = null;
    private int exclusionCount = 0;

    public Ray3D() { }

    public Ray3D From(Vector3 origin)
    {
        this.origin = origin;
        return this;
    }

    public Ray3D To(Vector3 target)
    {
        this.target = target;
        return this;
    }

    public Ray3D HitAreas(bool collideWithAreas)
    {
        this.collideWithAreas = collideWithAreas;
        return this;
    }

    public Ray3D HitBodies(bool collideWithBodies)
    {
        this.collideWithBodies = collideWithBodies;
        return this;
    }

    public Ray3D Mask(uint mask)
    {
        collisionMask = mask;
        return this;
    }

    public Ray3D HitFromInside(bool hitFromInside)
    {
        this.hitFromInside = hitFromInside;
        return this;
    }

    public Ray3D HitBackFaces(bool hitBackFaces)
    {
        this.hitBackFaces = hitBackFaces;
        return this;
    }

    public Ray3D Exclude(Rid rid)
    {
        exclusions ??= new();
        exclusions.Add(rid);
        ++exclusionCount;
        return this;
    }

    public Ray3D Exclude(CollisionObject3D obj)
    {
        return Exclude(obj.GetRid());
    }

    public Ray3D ClearExclusions()
    {
        exclusions.Clear();
        exclusionCount = 0;
        return this;
    }

    public readonly TraceResult3D Trace()
    {
        GlobalRayCast3D.ClearExceptions();

        for (int i = 0; i < exclusionCount; i++)
            GlobalRayCast3D.AddExceptionRid(exclusions[i]);

        GlobalRayCast3D.Origin = origin;
        GlobalRayCast3D.GlobalTargetPosition = target;
        GlobalRayCast3D.CollideWithAreas = collideWithAreas;
        GlobalRayCast3D.CollideWithBodies = collideWithBodies;
        GlobalRayCast3D.CollisionMask = collisionMask;
        GlobalRayCast3D.HitFromInside = hitFromInside;
        GlobalRayCast3D.HitBackFaces = hitBackFaces;
        GlobalRayCast3D.Trace(out var result);
        return result;
    }

}
