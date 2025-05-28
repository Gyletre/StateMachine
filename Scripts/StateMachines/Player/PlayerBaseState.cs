using Godot;
namespace Game.StateMachine.PlayerState
{
    public abstract class PlayerBaseState : State
    {
        protected PlayerStateMachine stateMachine;
        public PlayerBaseState(PlayerStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        protected void Move(double delta)
        {
            stateMachine.animator.SwitchDirection(stateMachine.inputReader.moveDirection);

            stateMachine.Velocity = Vector2.Zero;
            stateMachine.Velocity = stateMachine.inputReader.moveDirection * stateMachine.speed * (float)delta * 100;
            if (stateMachine.Velocity != Vector2.Zero) stateMachine.animator.SwitchAnimation(AnimationType.Walking);
            else stateMachine.animator.SwitchAnimation(AnimationType.Idle);
            stateMachine.MoveAndSlide();
        }
    }
}