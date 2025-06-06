using Godot;
using System;
using System.Collections.Generic;
using Game.Saving;

namespace Game.SceneManagement;

public partial class SceneManager : Node2D, SaveableManager
{
	[Export] Node sceneHolder;
	static SceneManager instance;
	public static Fader fader;
	public static PlayerManageable player;
	public static List<Enemy> enemies = new();
	public static Action OnAllEnemiesDefeated;
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
		return instance.sceneHolder.GetChild<Level>(0).id;
	}
	public static void LoadMap(SceneConfig sceneToLoad)
	{
		SceneManager.sceneToLoad = sceneToLoad;
		fader.FadeOut(ChangeScene);
	}

	private static void ChangeScene()
	{
		savingSystem.Save();
		instance.sceneHolder.GetChild(0).Free();
		var scene = ResourceLoader.Load<PackedScene>(sceneToLoad.scene).Instantiate();
		instance.sceneHolder.AddChild(scene);
		instance.enemiesDefeated = false;
		savingSystem.Load();
		player.SetGlobalPos(sceneToLoad.playerPos);
		savingSystem.Save();
		fader.FadeIn();
	}

	public void SlowEnemies(float rate, float duration)
	{
		if (rate > 0) enemySpeed = rate;

		foreach (Enemy e in enemies)
		{
			e.SlowDown(rate);
		}
		if (duration > 0)
		{
			GetTree().CreateTimer(duration).Timeout += () =>
			{
				SlowEnemies(1, -1);
			};
		}

	}
	public void SlowPlayer(float rate, float duration)
	{
		if (rate > 0) playerSpeed = rate;
		player.SlowDown(rate);
		if (duration > 0)
		{
			GetTree().CreateTimer(duration).Timeout += () =>
			{
				SlowPlayer(1, -1);
			};
		}
	}
	public void Pause()
	{
		SlowPlayer(0, -1);
		SlowEnemies(0, -1);
		paused = true;
	}
	public void Unpause()
	{
		SlowPlayer(playerSpeed, -1);
		SlowEnemies(enemySpeed, -1);
		paused = false;
	}
	public override void _Ready()
	{
		savingSystem = SavingSystem.instance;
		instance = this;
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
			OnAllEnemiesDefeated?.Invoke();
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
