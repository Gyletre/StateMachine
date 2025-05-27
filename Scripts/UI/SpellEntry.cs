using Godot;
using System;

namespace Game.UI;

public partial class SpellEntry : Node2D
{
	[Export] public Texture2D image;
	[Export] public string spellName;
	[Export] public Spell spellIdentity;
	TileButton equipButton;

	public Action OnEquipSpell;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetChild<Sprite2D>(2).Texture = image;
		GetChild<Label>(3).Text = spellName;
		OnEquipSpell += EquipSpellOnSlot;
		GetChild<TileButton>(-1).OnPressed += EquipSpellOnSlot;
	}

	private void EquipSpellOnSlot()
	{
		GD.Print("Press 1, 2 or 3 to equip spell on that button");
		Action<int>[] events = SceneManager.player.GetAbilityKeys();
		for (int i = 0; i < events.Length; i++)
		{
			events[i] += EquipSpell;
		}
	}

	private void EquipSpell(int slot)
	{
		SceneManager.player.EquipSpell(slot, spellIdentity);
		Action<int>[] events = SceneManager.player.GetAbilityKeys();
		for (int i = 0; i < events.Length; i++)
		{
			events[i] -= EquipSpell;
		}

	}
}
