using Godot;
using Animation;
[GlobalClass]
public partial class HitBox : Resource
{
    [Export] public Vector2[] positions;
    [Export] public float radius;
}