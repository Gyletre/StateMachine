using Godot;

namespace Game.StateMachine.PlayerState
{
    public class PlayerAttackingState : PlayerBaseState
    {
        bool validAttack;
        public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
        }

        public override void Enter()
        {
            GD.Print("Entered attacking state");
            validAttack = false;
            stateMachine.animator.SwitchAnimation(AnimationType.MeleeAttack, OnHit, OnAnimationEnded);
        }

        public override void Tick(double delta) { }

        public override void Exit()
        {
            stateMachine.inputReader.IsAttacking();
            if (validAttack)
            {
                stateMachine.roundData.combatActions--;
            }
        }
        private void OnHit(int direction, float time)
        {
            stateMachine.hitBoxManager.MoveHitBox(direction, time, TargetHit);
        }

        private void TargetHit(Node2D body)
        {
            if (body is Enemy e)
            {
                validAttack = true;
                e.TakeDamage(stateMachine.GetAttackDamage(0));
            }
        }


        void OnAnimationEnded()
        {
            stateMachine.ReturnToMovementState();
        }
    }
}