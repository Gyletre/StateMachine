using Godot;
using System;
namespace Game;

public interface Enemy
{
	public void TakeDamage(int amount);
	public void SlowDown(float rate);
}
