using Animation;
using Core;
using Godot;
using System;

namespace StateMachine
{
	public partial class PlayerStateMachine : StateMachine, Player
	{
		[Export] public InputReader inputReader;
		[Export] public float speed { get; private set; } = 50;
		[Export] public Animator animator;
		[Export] public HitBoxManager hitBoxManager;
		[Export] public double invincibility = 1;

		public double invincibilityTime = 0;
		public int damage = 5;
		public int hp = 100;
		public override void _Ready()
		{
			SwitchState(new PlayerMoveState(this));
		}
		public override void _Process(double delta)
		{
			invincibilityTime = Mathf.Max(0, invincibilityTime - delta * slowrate);
		}

		public void TakeDamage(int amount)
		{
			if (invincibilityTime > 0) return;
			hp -= amount;
			SwitchState(new PlayerHurtState(this));

		}

		public void SlowDown(float rate)
		{
			slowrate = rate;
			animator.animationSpeed = rate;
		}
	}

}

public interface Player
{
	public void TakeDamage(int amount);
}