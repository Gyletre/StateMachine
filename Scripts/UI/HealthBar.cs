using Godot;
using System;

namespace Game.UI;

public partial class HealthBar : Node2D
{
	[Export] Node2D healthBarFill;
	[Export] float visibilityTime = 2f;
	[Export] float fadeTime = 1f;
	int max = 1;
	int current = 1;
	float currentVisibilityTime = 0;

	public override void _Ready()
	{
		var color = Modulate;
		color.A = 0;
		Modulate = color;
	}
	public override void _Process(double delta)
	{
		if (currentVisibilityTime > 0)
		{
			currentVisibilityTime -= (float)delta;

		}
		else if (Modulate.A > 0)
		{
			var color = Modulate;
			color.A -= (float)(delta / fadeTime);
			Modulate = color;
		}
	}

	public void UpdateMaxHP(int maxHP)
	{
		max = maxHP;
		current = max;
		Scale = new Vector2(Scale.X * (1 + (maxHP - 100) * 0.003f), Scale.Y * (1 + (maxHP - 100) * 0.001f));
	}
	public void UpdateHealthBar(int currentHP)
	{
		var color = Modulate;
		color.A = 1;
		Modulate = color;
		current = currentHP;
		healthBarFill.Scale = new Vector2(MathF.Max((float)current / max, 0), 1);
		currentVisibilityTime = visibilityTime;
	}
}
