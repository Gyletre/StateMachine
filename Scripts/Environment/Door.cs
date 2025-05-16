using Godot;
using System;

namespace Scene;

public partial class Door : Node2D
{
	[Export] SceneConfig toEnter;
	[Export] Interactable interactableArea;
	[Export] string action_name = "interact";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		interactableArea.OnInteract += UseDoor;
		interactableArea.text.Text = "Press E to " + action_name;
	}

	private void UseDoor()
	{
		GD.Print("DoorTriggered");
		SceneManager manager = GetNode<SceneManager>("/root/SceneManager");
		manager.LoadMap(toEnter);
	}
}
