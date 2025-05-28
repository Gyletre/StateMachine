using Godot;
using System;
using System.Linq;

namespace Game;

public partial class SavingSystem : Node
{
    public void SaveGame()
    {
        ISaveable[] saveables = (ISaveable[])GetTree().GetNodesInGroup("saveable").Cast<ISaveable>();
        foreach (ISaveable save in saveables)
        {

        }
    }
}
