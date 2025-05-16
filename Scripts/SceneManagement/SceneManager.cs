using Godot;
using Godot.Collections;
using StateMachine;
using System;
using System.Linq;

namespace Scene;

public partial class SceneManager : Node2D
{
	[Export] Node sceneHolder;
	[Export] PlayerStateMachine player;
	bool paused = false;
	float enemySpeed = 1f;
	float playerSpeed = 1f;
	public void LoadMap(SceneConfig sceneToLoad)
	{
		foreach (Node n in sceneHolder.GetChildren())
		{
			n.QueueFree();
		}

		sceneHolder.AddChild(ResourceLoader.Load<PackedScene>(sceneToLoad.scene).Instantiate<Node2D>());
		player.GlobalPosition = sceneToLoad.playerPos;
	}


	public void SlowEnemies(float rate, float duration)
	{
		if (rate > 0) enemySpeed = rate;
		var enemies = GetTree().GetNodesInGroup("enemy");
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
	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("pause"))
		{
			if (paused) Unpause();
			else Pause();
		}

	}

}
