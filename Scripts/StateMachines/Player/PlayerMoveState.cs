using Godot;
using Animation;

namespace StateMachine
{
    namespace StateMachine
    {
        public class PlayerMoveState : PlayerBaseState
        {
            public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
            {
                this.stateMachine = stateMachine;
            }

            public override void Enter()
            {
                stateMachine.animator.SwitchAnimation(AnimationType.Idle);
                stateMachine.inputReader.JumpEvent += Jump;
            }
            public override void Tick(double delta)
            {
                stateMachine.animator.SwitchDirection(stateMachine.inputReader.moveDirection);

                stateMachine.Velocity = Vector2.Zero;
                stateMachine.Velocity = stateMachine.inputReader.moveDirection * stateMachine.speed;
                if (stateMachine.Velocity != Vector2.Zero) stateMachine.animator.SwitchAnimation(AnimationType.Walking);
                else stateMachine.animator.SwitchAnimation(AnimationType.Idle);
                stateMachine.MoveAndSlide();
                if (stateMachine.inputReader.IsAttacking())
                {
                    stateMachine.SwitchState(new PlayerAttackingState(stateMachine));
                }
            }
            public override void Exit()
            {
                stateMachine.inputReader.JumpEvent -= Jump;
            }
            private void Jump()
            {
        private void Jump()
            {
                GD.Print("Jump!");
                stateMachine.classManager.GainExp(100);
            }



        }
    }