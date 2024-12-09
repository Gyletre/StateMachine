using Core;
using Godot;
using Game.Classes;
using System;
using Interface;

namespace StateMachine
{
	public partial class PlayerStateMachine : StateMachine
	{

		[Export] public InputReader inputReader = new InputReader();
		[Export] public AnimationTree animationTree;
		[Export] public float speed { get; private set; } = 200;
		public PlayerClassManager classManager { get; private set; } = new PlayerClassManager();
		public bool canMove = false;
		public bool attackFinished = false;


		public override void _Ready()
		{

			classManager.AddClass(ClassList.Warrior);
			SwitchState(new PlayerMoveState(this));
			GetNode<Area2D>("MeleeHitBox").BodyEntered += OnAttack;


		}

		private void OnAttack(Node2D body)
		{
			if (body is Enemy e)
			{
				e.TakeDamage(this, classManager.GetStat(StatType.Attack));
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			Velocity = Vector2.Zero;
			if (canMove)
			{
				Velocity = inputReader.moveDirection * speed;
			}
			MoveAndSlide();
		}
	}
}