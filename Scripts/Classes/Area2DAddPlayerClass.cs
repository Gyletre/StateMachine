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
				playerStateMachine.classManager.AddClass(classToAdd);
			}
		}
	}
	
}
