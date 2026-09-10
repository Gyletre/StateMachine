using Godot;
using System;

namespace Game.Camera
{
	public partial class MapConstrictCamera : Node
	{
		[Export] bool activeRestrict;
		public override void _Ready()
		{
			if (activeRestrict)
				SetCameraLimits();

		}
		public void SetCameraLimits()
		{
			Vector2I tl = (Vector2I)GetChild<Node2D>(0).GlobalPosition;
			Vector2I br = (Vector2I)GetChild<Node2D>(1).GlobalPosition;
			Camera.instance.SetCameraConstraints(tl, br);
		}
	}
}

