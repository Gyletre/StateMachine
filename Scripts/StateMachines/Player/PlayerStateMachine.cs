using Godot;
using System;
using System.Collections.Generic;
using Game.UI;
using Game.Animation;
using Game.SceneManagement;

namespace Game.StateMachine.PlayerState
{
	public partial class PlayerStateMachine : StateMachine, Player, PlayerManageable, ISaveable
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
			healthBar.UpdateMaxHP(maxHP);
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

		public ISaveData SaveState()
		{
			PlayerData playerSaveData = new()
			{
				knownSpells = knownSpells,
				hp = hp
			};
			playerSaveData.SetPos(SceneManager.GetCurrentLevelIdentifier(), GlobalPosition);
			GD.Print(playerSaveData.knownSpells + " " + playerSaveData.hp);
			return playerSaveData;

		}

		public void LoadState(ISaveData state)
		{
			if (state is PlayerData playerSaveData)
			{
				knownSpells = playerSaveData.knownSpells;
				hp = playerSaveData.hp;
				if (playerSaveData.scenePositions.ContainsKey(Enum.GetName(SceneManager.GetCurrentLevelIdentifier())))
					GlobalPosition = playerSaveData.GetPos(SceneManager.GetCurrentLevelIdentifier());

			}
		}
	}
	public class PlayerData : ISaveData
	{
		public List<Spell> knownSpells { get; set; }
		public int hp { get; set; }
		public Dictionary<string, float[]> scenePositions = new();
		public Vector2 GetPos(LevelIdentifier levelIdentifier)
		{
			string key = Enum.GetName(levelIdentifier);
			return new Vector2(scenePositions[key][0], scenePositions[key][1]);
		}
		public void SetPos(LevelIdentifier levelIdentifier, Vector2 pos)
		{
			string key = Enum.GetName(levelIdentifier);
			scenePositions[key] = [pos.X, pos.Y];
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
		public Vector2 GetGlobalPos();
		public void SetGlobalPos(Vector2 pos);
		public void SlowDown(float rate);
		public List<Spell> GetKnownSpells();
		public void LearnSpell(Spell spell);
		public Action<int>[] GetAbilityKeys();
		public void EquipSpell(int num, Spell spell);
		public event Action OnSpellAdded;
	}
}
