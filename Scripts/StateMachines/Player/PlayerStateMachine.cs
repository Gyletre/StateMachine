using Core;
using Godot;
using Classes;

namespace StateMachine
{
	public partial class PlayerStateMachine : StateMachine
	{

		[Export] public InputReader inputReader = new InputReader();
		[Export] public AnimationPlayer animator;
		[Export] public float speed { get; private set; } = 200;
		public PlayerClassManager classManager { get; private set; } = new PlayerClassManager();
		public bool canMove = false;
		public bool attackFinished = false;


		public override void _Ready()
		{

			classManager.AddClass(ClassList.Warrior);
			SwitchState(new PlayerMoveState(this));


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