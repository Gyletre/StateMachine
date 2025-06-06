using Godot;

namespace Game;

public partial class Level : Node2D
{
    [Export] public LevelIdentifier id;
}
public enum LevelIdentifier
{
    ForestTown,
    CastleTown,
    Plains,
    Desert,
    DesertTown,
    Castle,
    Inn,
    Shop,
}