using Godot;
using System;

namespace Game;

public partial class Door : Node2D
{
	[Export] SceneConfig toEnter;
	[Export] Interactable interactableArea;
	[Export] string actionName = "interact";
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		interactableArea.OnInteract += UseDoor;
		interactableArea.SetInteractText(actionName);
	}

	private void UseDoor()
	{
		GD.Print("DoorTriggered");
		SceneManager.LoadMap(toEnter);
	}
}
