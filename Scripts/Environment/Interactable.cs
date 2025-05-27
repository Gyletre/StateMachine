using Godot;
using System;

namespace Game;

public partial class Interactable : Area2D
{
	public Action OnInteract;
	[Export] public Label text;

	bool isActive;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		BodyEntered += EnableInteract;
		BodyExited += DisableInteract;
		text.Visible = false;
	}
	public void SetInteractText(string action)
	{
		text.Text = "Press E to " + action;
		text.Position = new Vector2(-text.Size.X / 2, text.Position.Y);
	}

	private void EnableInteract(Node2D body)
	{
		if (body is Player p)
		{
			text.Visible = true;
		}
	}


	private void DisableInteract(Node2D body)
	{
		if (body is Player p)
		{
			text.Visible = false;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (!text.Visible) return;
		if (Input.IsActionJustPressed("interact"))
		{
			OnInteract?.Invoke();
		}
	}
}
