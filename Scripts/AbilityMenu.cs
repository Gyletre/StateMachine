using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class AbilityMenu : Control
{
	[Export] ItemList abilityList;
	Action[] abilities = new Action[10];
	public void AddAbility(string name, Action ability)
	{
		int index = abilityList.AddItem(name);
		abilities[index] = ability;

	}
	public override void _Ready()
	{
		abilityList.ItemClicked += UseAbility;
	}


	private void UseAbility(long index, Vector2 atPosition, long mouseButtonIndex)
	{
		GD.Print(mouseButtonIndex, " mouse button pressed on ability of index " + index);
		if (mouseButtonIndex == 0)
			abilities[index]?.Invoke();
	}
}
