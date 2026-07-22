using Godot;
using System;
using System.Diagnostics;

namespace Game.StateMachine.PlayerState
{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

        }

        public override void Enter()
        {
            stateMachine.StartTurn = null;
            GD.Print("Entered movement state");
            stateMachine.animator.SwitchAnimation(AnimationType.Idle);
            stateMachine.inputReader.JumpEvent += Jump;

        }
        public override void Tick(double delta)
        {
            Move(delta);
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
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

    }
}