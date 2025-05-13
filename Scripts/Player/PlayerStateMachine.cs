using Animation;
using Core;
using Godot;
using System;

namespace StateMachine
{
	public partial class PlayerStateMachine : StateMachine
	{
		[Export] public InputReader inputReader;
		[Export] public float speed { get; private set; } = 50;
		[Export] public Animator animator;

		public override void _Ready()
		{
			SwitchState(new PlayerMoveState(this));
		}

	}

}