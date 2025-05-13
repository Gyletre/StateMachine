using System;
using Godot;
using Animation;

namespace StateMachine
{
    public class PlayerAttackingState : PlayerBaseState
    {
        public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
            GD.Print("Attack");
            stateMachine.animator.SwitchAnimation(AnimationType.Attack0, OnAnimationEnded);
        }
        public override void Tick(double delta)
        {

        }
        public override void Exit()
        {
            stateMachine.inputReader.IsAttacking();
        }

        void OnAnimationEnded()
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }


    }
}