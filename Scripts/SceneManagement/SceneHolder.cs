
using Godot;
using System;
namespace Game.SceneManagement;

using Game.Saving;

public partial class SceneHolder : Node2D, ISaveable
{
    [Export] SceneConfig lastScene;
    public Vector2 LoadLastScene()
    {
        LoadScene(lastScene);
        return lastScene.playerPos;
    }
    public void LoadScene(SceneConfig config)
    {
        if (GetChildren().Count > 0) GetChild(0).Free();
        GD.Print($"Scene to load at {config.scene}");
        var scene = ResourceLoader.Load<PackedScene>(config.scene).Instantiate();
        lastScene = config;
        AddChild(scene);
    }

    public void LoadState(ISaveData state)
    {
        if (state is SceneData data)
        {
            lastScene = data.sceneConfig;
        }
    }

    public ISaveData SaveState()
    {
        SceneData data = new();
        data.sceneConfig = lastScene;
        return data;
    }

    internal LevelIdentifier GetCurrentLevelID()
    {
        if (GetChildren().Count > 0) return GetChild<Level>(0).id;
        return LevelIdentifier.ForestTown;
    }
}
public class SceneData : ISaveData
{
    public SceneConfig sceneConfig { get; set; }
}
