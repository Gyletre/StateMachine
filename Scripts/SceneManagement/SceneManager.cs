using Godot;
using System;
using System.Collections.Generic;
using Game.Saving;

namespace Game.SceneManagement;

public partial class SceneManager : Node2D, SaveableManager
{
	[Export] SceneHolder sceneHolder;
	static SceneManager instance;
	public static Fader fader;
	public static PlayerManageable player;
	public static List<Enemy> enemies = new();
	public static Action OnSceneChanged;
	private static List<SaveableEntity> saveables = new();
	private static SavingSystem savingSystem;
	private static SceneConfig sceneToLoad;
	bool paused = false;
	float enemySpeed = 1f;
	float playerSpeed = 1f;
	bool enemiesDefeated = false;

	public static LevelIdentifier GetCurrentLevelIdentifier()
	{
		return instance.sceneHolder.GetCurrentLevelID();
	}
	public static void LoadMap(SceneConfig sceneToLoad)
	{
		SceneManager.sceneToLoad = sceneToLoad;
		fader.FadeOut(ChangeScene);
	}
	private static void ChangeScene()
	{
		savingSystem.Save();
		instance.sceneHolder.LoadScene(sceneToLoad);
		OnSceneChanged?.Invoke();
		savingSystem.Load();
		player.SetGlobalPos(sceneToLoad.playerPos);
		savingSystem.Save();
		fader.FadeIn();
	}
	public void Pause()
	{
		GetTree().Paused = true;
		paused = true;
	}
	public void Unpause()
	{
		GetTree().Paused = false;
		paused = false;
	}
	public override void _EnterTree()
	{
		instance = this;
	}

	public override void _Ready()
	{
		savingSystem = SavingSystem.instance;
		savingSystem.Load();
		player.SetGlobalPos(instance.sceneHolder.LoadLastScene());
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			if (paused) Unpause();
			else Pause();
		}
		if (enemies.Count == 0)
		{
			EnemiesDefeated();
		}
	}
	
	private void EnemiesDefeated()
	{
		if (!enemiesDefeated)
		{
			enemiesDefeated = true;
		}
	}

	public List<SaveableEntity> GetSaveables()
	{
		return saveables;
	}

	public void AddSaveable(SaveableEntity entity)
	{
		saveables.Add(entity);
	}

	public void RemoveSaveable(SaveableEntity entity)
	{
		saveables.Remove(entity);
	}
}