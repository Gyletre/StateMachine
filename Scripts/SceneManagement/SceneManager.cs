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
	public void LoadMap(SceneConfig sceneToLoad)
	{
		foreach (Node n in sceneHolder.GetChildren())
		{
			n.QueueFree();
		}
		sceneHolder.AddChild(sceneToLoad.scene.Instantiate<Node2D>());
		player.GlobalPosition = sceneToLoad.playerPos;
	}
	public void SlowEnemies(float rate, float duration)
	{
		GD.Print("This is not implemented yet");
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
		player.SlowDown(rate);
		if (duration > 0)
		{
			GetTree().CreateTimer(duration).Timeout += () =>
			{
				SlowPlayer(1, -1);
			};
		}
	}
}
