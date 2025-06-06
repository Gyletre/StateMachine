using Godot;
using System;
using Game.UI;

namespace Game.StateMachine.PlayerState
{
    public class PlayerJumpingState : PlayerBaseState
    {
        public PlayerJumpingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;

        }

        public override void Enter()
        {
            stateMachine.animator.SwitchAnimation(AnimationType.Jump, OnEnd: LandOnGround);
        }

        private void LandOnGround()
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }


        public override void Tick(double delta)
        {
            Move(delta);
        }

        public override void Exit()
        {

        }



    }
}