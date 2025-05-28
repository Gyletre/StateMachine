using Godot;
using System;

public interface ISaveable
{
    public object[] SaveState();
    public void LoadState(object[] state);
}
