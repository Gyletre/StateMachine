using Core;
using Godot;
using System;

namespace StateMachine{
	public partial class PlayerStateMachine :  StateMachine
	{
		[Export] public InputReader inputReader = new InputReader();
		[Export] public float speed {get; private set;} = 50;
		public bool canMove = false;
		
		public override void _PhysicsProcess(double delta)
		{
			Velocity = Vector2.Zero;
			if(canMove){
				Velocity = inputReader.moveDirection * speed;
			}
			MoveAndSlide();
		}
		public override void _Ready(){
			SwitchState(new PlayerMoveState(this));
		}
	}

}