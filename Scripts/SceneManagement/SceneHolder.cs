
using Godot;
using System;
namespace Game.SceneManagement;

using Game.Saving;

public partial class SceneHolder : Node2D, ISaveable
{
    [Export] SceneConfig lastScene;
    SceneConfig lastLoadedScene;
    public Vector2 LoadLastScene()
    {
        GD.Print($"lastScene.scene: {lastScene.scene}, lastScene.playerPos: {lastScene.playerPos}");
        LoadScene(lastScene);
        return lastScene.playerPos;
    }
    public void LoadScene(SceneConfig config)
    {

        if (GetChild(0) is not SaveableEntity) GetChild(0).QueueFree();

        GD.Print($"Scene to load at {config.scene}");
        var scene = ResourceLoader.Load<PackedScene>(config.scene).Instantiate();
        lastLoadedScene = config;
        AddChild(scene);
        MoveChild(scene, 0);
    }

    public void LoadState(ISaveData state)
    {
        if (state is SceneData data)
        {
            SceneConfig lS = new();
            lS.scene = data.ScenePath;
            lS.playerPos = new Vector2(data.PX, data.PY);
            lastScene = lS;
        }
    }

    public ISaveData SaveState()
    {
        SceneData data = new();
        data.ScenePath = lastLoadedScene.scene;
        data.PX = lastLoadedScene.playerPos[0];
        data.PY = lastLoadedScene.playerPos[1];
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
    public string ScenePath { get; set; }
    public float PX { get; set; }
    public float PY { get; set; }
}
