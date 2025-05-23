using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class AbilityMenu : Control
{
	[Export] ItemList abilityList;

	public void AddAbility(string name, Action ability)
	{

	}
	public override void _Ready()
	{
		abilityList.ItemClicked += UseAbility;
	}


	private void UseAbility(long index, Vector2 atPosition, long mouseButtonIndex)
	{

	}
}
