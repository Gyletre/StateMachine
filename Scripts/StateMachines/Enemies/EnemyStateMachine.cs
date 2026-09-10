using Godot;
using Game.Animation;
using System;
using Game.UI;
using Game.SceneManagement;
using Game.Combat;

namespace Game.StateMachine.EnemyState;

public partial class EnemyStateMachine : StateMachine, Enemy, ISaveable, CombatParticipant
{
	#region components
	[Export] public Area2D hitbox;
	[Export] public Area2D detectionArea;
	[Export] public HitBoxManager hitBoxManager;
	[Export] public Animator animator;
	[Export] public HealthBar healthBar;
	#endregion

	#region stats
	[Export] public int speed = 70;
	[Export] public int maxHp = 50;
	[Export] public int attack = 5;
	[Export] public int combatActionLimit = 2;
	#endregion

	public int combatActions = 0;
	public int hp;
	public Node2D player { get => staticPlayer; set => staticPlayer = value; }
	private static Node2D staticPlayer;
	public bool invulnerable;
	public Action<int> OnDamageTaken;

	#region combatParticipant
	public Action StartTurn { get; set; } = null;
	public Action EndTurn { get; set; } = null;
	public RoundData roundData { get; set; } = null;
	public bool IsAlive { get; set; } = true;

	public void BeginCombat()
	{
		SwitchState(new EnemyCombatState(this));
	}
	public void EndCombat()
	{
		SwitchState(new EnemyMoveState(this));
	}
	#endregion

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
	public override void _Ready()
	{
		SceneManager.enemies.Add(this);
		healthBar.UpdateMaxHP(maxHp);
		hp = maxHp;
		OnDamageTaken += healthBar.UpdateHealthBar;
		detectionArea.BodyEntered += TargetPlayer;
		SwitchState(new EnemyMoveState(this));
	}
	public override void _ExitTree()
	{
		SceneManager.enemies.Remove(this);
	}


	private void TargetPlayer(Node2D body)
	{
		if (body is Player)
		{
			player = body;
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
				if (SceneManager.enemies.Contains(this))
				{
					SceneManager.enemies.Remove(this);
				}
				QueueFree();
			}
		}
	}
}
[Serializable]
public class EnemyData : ISaveData
{
	public int hp { get; set; }
}
