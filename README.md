# sharp-trace
A C# utility for performant and dead simple ray casting and shape casting in Godot 4.

## Installation
Clone and add the root folder to your project directory.

Build the game.

Add SharpTrace.cs to your project's autoload and enable it.

## Usage
Usage should be pretty self-explanatory. Use the two static classes ```Trace3D``` and ```Trace2D``` for 3D and 2D physics queries respectively.

Trace parameters can be cached and then reused / edited then reused.


## Example
```cs
var result = Trace3D.Ray(GlobalPosition, GlobalPosition + Vector3.Forward)
    .Exclude(this)
    .Trace();

// or

result = Trace3D.Ray()
    .From(GlobalPosition)
    .To(GlobalPosition + Vector3.Forward)
    .Exclude(this)
    .Trace();

if (result.IsColliding)
{
    var point = result.Point;
    var normal = result.Normal;
    var collider = result.Collider;
}

// cached sphere sweep parameters
var params2 = Trace3D.Sphere()
    .From(GlobalPosition)
    .To(GlobalPosition + Vector3.Forward)
    .Radius(0.5f)
    .MaxResults(3)
    .HitAreas(true)
    .HitBodies(false)
    .Margin(0.01f)
    .Mask(1);

var result2 = params2.Trace();

params2 = params2.Radius(0.1f);

var result3 = params2.Trace();

if (result3.IsColliding)
{
    var collisions = result2.Collisions;
    var count = result2.CollisionCount;
    var safeFrac = result2.SafeFraction;
    var unsafeFrac = result2.UnsafeFraction;

    for (int i = 0; i < count; i++)
    {
        var collision = collisions[i];
        var point = collision.Point;
        // ...
    }
}
```
