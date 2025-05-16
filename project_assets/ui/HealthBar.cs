using Godot;
using System;

public partial class HealthBar : Node2D
{
	[Export] Node2D healthBarFill;
	[Export] float visibilityTime = 2f;
	[Export] float fadeTime = 1f;
	int max = 1;
	int current = 1;

	public override void _Ready()
	{
		healthBarFill.Visible = false;
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
	}

	public void UpdateMaxHP(int maxHP)
	{
		max = maxHP;
		current = max;
	}
	public void UpdateHealthBar(int currentHP)
	{
		current = currentHP;
		healthBarFill.Scale = new Vector2(max / current, 1);
	}
}
