using Godot;
using Game.Animation;
using System;
using Game.UI;
using Game.SceneManagement;

namespace Game.StateMachine.EnemyState;

public partial class EnemyStateMachine : StateMachine, Enemy, ISaveable
{
	[Export] public Area2D hitbox;
	[Export] public Area2D detectionArea;
	[Export] public HitBoxManager hitBoxManager;
	[Export] public Animator animator;
	[Export] public int speed = 70;
	[Export] public double attackCooldown = 1;
	[Export] public int maxHp = 50;
	[Export] public HealthBar healthBar;
	[Export] public int attack = 5;
	public Action<int> OnDamageTaken;
	public double attackCooldownTime = 0;
	public int hp;

	public bool detectedPlayer = false;
	public Node2D player;
	public bool invulnerable;


	public void TakeDamage(int amount)
	{
		hp -= amount;
		if (amount < 1 || invulnerable)
		{
			OnDamageTaken?.Invoke(hp);
			return;
		}
		SwitchState(new EnemyHurtState(this));
	}

	public void SlowDown(float rate)
	{
		slowrate = rate;
		animator.animationSpeed = rate;
	}

	public override void _Ready()
	{
		SceneManager.enemies.Add(this);
		healthBar.UpdateMaxHP(maxHp);
		hp = maxHp;
		OnDamageTaken += healthBar.UpdateHealthBar;

		hitbox.BodyEntered += OnCollideWithPlayer;
		detectionArea.BodyEntered += HuntPlayer;
		detectionArea.BodyExited += StopHuntingPlayer;
		SwitchState(new EnemyMoveState(this));
	}
	public override void _Process(double delta)
	{
		attackCooldownTime = Mathf.Max(0, attackCooldownTime - (delta * slowrate));
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

	public ISaveData SaveState()
	{
		EnemyData data = new();
		data.hp = hp;
		return data;
	}

	public void LoadState(ISaveData state)
	{
		if (state is EnemyData data)
		{
			hp = data.hp;
			if (hp <= 0)
			{
				SwitchState(new EnemyDieState(this));
				Visible = false;
			}
		}


	}
}
[Serializable]
public class EnemyData : ISaveData
{
	public int hp { get; set; }
}
