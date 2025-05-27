using Game.Quest;
using Godot;
using System;
using System.Collections.Generic;

namespace Game;

public partial class SceneManager : Node2D
{
	[Export] Node sceneHolder;
	static SceneManager instance;
	public static PlayerManageable player;
	public static List<Enemy> enemies = new();
	public static List<Npc> npcs = new();
	public static Action OnAllEnemiesDefeated;
	bool paused = false;
	float enemySpeed = 1f;
	float playerSpeed = 1f;
	public static LevelIdentifier GetCurrentLevelIdentifier()
	{
		return instance.sceneHolder.GetChild<Level>(0).id;
	}
	public static void LoadMap(SceneConfig sceneToLoad)
	{
		foreach (Node n in instance.sceneHolder.GetChildren())
		{
			n.QueueFree();
		}

		instance.sceneHolder.AddChild(ResourceLoader.Load<PackedScene>(sceneToLoad.scene).Instantiate<Node2D>());
		player.SetGlobalPos(sceneToLoad.playerPos);
		instance.enemiesDefeated = false;
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

	bool enemiesDefeated = false;
	private void EnemiesDefeated()
	{
		if (!enemiesDefeated)
		{
			enemiesDefeated = true;
			OnAllEnemiesDefeated?.Invoke();
			GD.Print("no more enemies");
		}
	}
}
