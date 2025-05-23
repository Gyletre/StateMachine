using Godot;
using System;

namespace Game;

public partial class HitBoxManager : Area2D
{
	[Export] CollisionShape2D collider;
	[Export] HitBox hitBox;
	Action<Node2D> TargetHit;
	public override void _Ready()
	{
		BodyEntered += OnHit;
		collider.Disabled = true;
	}
	public void MoveHitBox(int direction, float duration, Action<Node2D> TargetHit)
	{
		collider.Position = hitBox.positions[direction];
		var thing = (CircleShape2D)collider.Shape;
		thing.Radius = hitBox.radius;
		collider.Shape = thing;
		this.TargetHit = TargetHit;
		collider.Disabled = false;
		GetTree().CreateTimer(duration).Timeout += () =>
		{
			collider.Disabled = true;
		};
	}
	public Vector2[] GetHitBoxLocations()
	{
		return hitBox.positions;
	}
	public float GetHitBoxRadius()
	{
		return hitBox.radius;
	}

	private void OnHit(Node2D body)
	{
		TargetHit?.Invoke(body);
	}

}
