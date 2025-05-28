using Godot;
using System;

public interface Enemy
{
	public void TakeDamage(int amount);
	public void SlowDown(float rate);
}
