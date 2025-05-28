using Godot;
using System;
using System.Collections.Generic;
using Game.UI;
using Game.Animation;

namespace Game.StateMachine.PlayerState
{
	public partial class PlayerStateMachine : StateMachine, Player, PlayerManageable
	{
		[Export] public InputReader inputReader;
		[Export] public Animator animator;
		[Export] public HitBoxManager hitBoxManager;
		[Export] public HealthBar healthBar;
		[Export] public float speed { get; private set; } = 50;
		[Export] public double invincibility = 1;
		[Export] public int maxHP = 100;

		public Action<int> OnDamageTaken;
		public double invincibilityTime = 0;
		public int baseAttack = 5;
		public int damageMultiplier = 1;
		public int hp;
		public List<Spell> knownSpells = new();
		public Spell[] spellsEquipped = new Spell[3];

		public event Action OnSpellAdded;

		public override void _Ready()
		{
			knownSpells.Add(Spell.Heal);
			hp = maxHP;
			SceneManager.player = this;
			healthBar.UpdateMaxHP(hp);
			OnDamageTaken += healthBar.UpdateHealthBar;
			SwitchState(new PlayerMoveState(this));
		}
		public override void _Process(double delta)
		{
			invincibilityTime = Mathf.Max(0, invincibilityTime - delta * slowrate);
		}
		public override void _ExitTree()
		{
			Camera.Camera.instance.mode = Camera.CameraMode.Cutscene;
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
		public int GetAttack()
		{
			return baseAttack * damageMultiplier;
		}
		public void EquipSpell(int num, Spell spell)
		{
			spellsEquipped[num] = spell;
		}

		public List<Spell> GetKnownSpells()
		{
			return knownSpells;
		}

		public void LearnSpell(Spell spell)
		{
			TextMessageWriter.Print("Learned " + Enum.GetName(spell));
			knownSpells.Add(spell);
			OnSpellAdded?.Invoke();
		}

		public Vector2 GetGlobalPos()
		{
			return GlobalPosition;
		}

		public void SetGlobalPos(Vector2 pos)
		{
			GlobalPosition = pos;
		}

		public Action<int>[] GetAbilityKeys()
		{
			return inputReader.abilities;
		}
	}

}
namespace Game
{
	public interface Player
	{
		public void TakeDamage(int amount);
	}
	public interface PlayerManageable
	{
		public List<Spell> GetKnownSpells();
		public Vector2 GetGlobalPos();
		public void SetGlobalPos(Vector2 pos);
		public void LearnSpell(Spell spell);
		public void SlowDown(float rate);
		public Action<int>[] GetAbilityKeys();
		public void EquipSpell(int num, Spell spell);
		public event Action OnSpellAdded;
	}
}
