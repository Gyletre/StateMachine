using Godot;
using System;

using Game;
public interface ISaveable
{
    public ISaveData SaveState();
    public void LoadState(ISaveData state);
}
