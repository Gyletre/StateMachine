using Godot;
using System;

namespace Scene;

public partial class Door : Node2D
{
	[Export] SceneConfig toEnter;
	[Export] Interactable interactableArea;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		interactableArea.OnInteract += UseDoor;
	}

	private void UseDoor()
	{
		GD.Print("DoorTriggered");
		SceneManager manager = GetNode<SceneManager>("/root");
		manager.LoadMap(toEnter);
	}
}
