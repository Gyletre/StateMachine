using Godot;
using System;

namespace Game.Saving;

[Tool]
public partial class SaveableEntity : Node
{
    [Export] string uniqueIdentifier = "";
    ISaveable parent;
    public override void _Ready()
    {
        if (Engine.IsEditorHint()) return;
        SavingSystem.manager.AddSaveable(this);
        parent = GetParent<ISaveable>();
    }
    public string GetUniqueIdentifier()
    {
        return uniqueIdentifier;
    }
    public ISaveData CaptureState()
    {
        return parent.SaveState();
    }
    public void RestoreState(ISaveData save)
    {
        parent.LoadState(save);
    }
    public override void _ExitTree()
    {
        if (Engine.IsEditorHint()) return;
        SavingSystem.manager.RemoveSaveable(this);
    }
    public override void _Process(double delta)
    {
        if (!string.IsNullOrEmpty(uniqueIdentifier)) return;
        uniqueIdentifier = Guid.NewGuid().ToString();
    }
}
