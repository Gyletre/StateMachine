using Godot;
using Animation;
using System;

namespace StateMachine;

public partial class EnemyStateMachine : StateMachine, Enemy
{
	[Export] public Area2D hitbox;
	[Export] public Area2D detectionArea;
	[Export] public HitBoxManager hitBoxManager;
	[Export] public Animator animator;
	[Export] public int speed = 70;
	[Export] public double attackCooldown = 1;
	[Export] public double invincibility = 0.6;
	[Export] public int hp = 50;
	public int attack = 5;
	public double attackCooldownTime = 0;
	public double invincibilityTime = 0;

	public bool detectedPlayer = false;
	public Node2D player;


	public void TakeDamage(int amount)
	{
		if (invincibilityTime > 0.1 || amount < 1) return;
		GD.Print("Took " + amount + " damage.");
		hp -= amount;
		SwitchState(new EnemyHurtState(this));
	}

	public void SlowDown(float rate)
	{
		slowrate = rate;
		animator.animationSpeed = rate;
	}

	public override void _Ready()
	{
		hitbox.BodyEntered += OnCollideWithPlayer;
		detectionArea.BodyEntered += HuntPlayer;
		detectionArea.BodyExited += StopHuntingPlayer;
		SwitchState(new EnemyMoveState(this));
	}
	public override void _Process(double delta)
	{
		attackCooldownTime = Mathf.Max(0, attackCooldownTime - (delta * slowrate));
		invincibilityTime = Mathf.Max(0, attackCooldownTime - (delta * slowrate));
	}
	private void HuntPlayer(Node2D body)
	{
		if (body is Player)
		{
			detectedPlayer = true;
			player = body;
		}
	}
	private void StopHuntingPlayer(Node2D body)
	{
		if (body is Player)
		{
			detectedPlayer = false;
			player = null;
		}
	}
	private void OnCollideWithPlayer(Node2D body)
	{
		if (body is Player p)
		{
			p.TakeDamage(attack - Mathf.CeilToInt(attack / 2));
		}
	}

}
