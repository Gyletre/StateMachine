using Godot;
using StateMachine;
using System;

public partial class Firebolt : Node2D
{
	public Node2D insantiator;
	public int damage = 0;
	public Vector2 direction;
	public int speed = 500;
	public override void _Ready()
	{
		if (insantiator is PlayerStateMachine psm)
		{
			damage = psm.classManager.GetStat(Classes.StatType.Magic);
		}
	}
	public override void _Process(double delta)
	{
		Position += speed * direction * (float)delta;
	}
	public void OnCollision(Node2D body)
	{
		// if (body is IEnemy e) e.damage(damage) 
		// disappear
	}
}
