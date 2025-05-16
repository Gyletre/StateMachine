
using Godot;

[GlobalClass]
public partial class SceneConfig : Resource
{
    [Export(PropertyHint.File)] public string scene;
    [Export] public Vector2 playerPos;
}