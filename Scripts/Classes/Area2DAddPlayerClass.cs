using Godot;
using StateMachine;
using System;
using Classes;

public partial class Area2DAddPlayerClass : Area2D
{
	[Export] ClassList classToAdd;


    public override void _Ready()
    {
        foreach (var c in GetChildren()){
			if (c is Label l){
				l.Text = Enum.GetName(classToAdd.GetType(),classToAdd);
			}
		}
    }
    public void OnBodyEntered(Node2D body){
		if(body.IsInGroup("Player")){
			if (body is PlayerStateMachine playerStateMachine){
				playerStateMachine.classManager.AddClass(GetClassFromTag(classToAdd, playerStateMachine));
			}
		}
	}
	private Class GetClassFromTag(ClassList tag, PlayerStateMachine psm){
		switch(tag){
			case ClassList.Mage:
				return new Mage(psm);
			case ClassList.Warrior:
				return new Warrior(psm);
			case ClassList.Cleric:
				return new Cleric(psm);
			case ClassList.Archer:
				return new Archer(psm);
			case ClassList.Sniper:
				return new Sniper(psm);
			case ClassList.Paladin:
				return new Paladin(psm);
			case ClassList.BattleMage:
				return new BattleMage(psm);

		
		}
		return new Class();
		
	}
}
