using Godot;

namespace SharpTrace;

public sealed partial class SharpTrace : Node
{
    public override void _Ready()
    {
        AddChild(new GlobalRayCast3D(), false, InternalMode.Back);
        AddChild(new GlobalShapeCast3D(), false, InternalMode.Back);
        AddChild(new GlobalRayCast2D(), false, InternalMode.Back);
        AddChild(new GlobalShapeCast2D(), false, InternalMode.Back);
    }
}
