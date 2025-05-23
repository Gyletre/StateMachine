using Godot;
using System;

public partial class MapConstrictCamera : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Camera.instance.SetCameraConstraints((Vector2I)GetChild<Node2D>(0).Position,
											 (Vector2I)GetChild<Node2D>(1).Position);
	}
}
