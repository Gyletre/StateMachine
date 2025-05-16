using System;
using Godot;
using Animation;

namespace StateMachine
{
    public class PlayerAttackingState : PlayerBaseState
    {
        public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            stateMachine.animator.SwitchAnimation(AnimationType.MeleeAttack, OnHit, OnAnimationEnded);
        }

        public override void Tick(double delta) { }

        public override void Exit()
        {
            stateMachine.inputReader.IsAttacking();
        }
        private void OnHit(int direction, float time)
        {
            stateMachine.hitBoxManager.MoveHitBox(direction, time, TargetHit);
        }

        private void TargetHit(Node2D body)
        {
            if (body is Enemy e)
            {
                e.TakeDamage(stateMachine.damage);
            }
        }


        void OnAnimationEnded()
        {
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
    }
}