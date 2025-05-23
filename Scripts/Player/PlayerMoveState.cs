using Godot;
using System;

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
            stateMachine.animator.SwitchAnimation(AnimationType.Idle);
            stateMachine.inputReader.JumpEvent += Jump;
            for (int i = 0; i < stateMachine.inputReader.abilities.Length; i++)
            {
                stateMachine.inputReader.abilities[i] += ActivateAbility;
            }
        }
        public override void Tick(double delta)
        {
            stateMachine.animator.SwitchDirection(stateMachine.inputReader.moveDirection);

            stateMachine.Velocity = Vector2.Zero;
            stateMachine.Velocity = stateMachine.inputReader.moveDirection * stateMachine.speed * (float)delta * 100;
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
            for (int i = 0; i < stateMachine.inputReader.abilities.Length; i++)
            {
                stateMachine.inputReader.abilities[i] -= ActivateAbility;
            }
        }
        private void ActivateAbility(int spellNo)
        {
            GD.Print("Ability button pressed");
            Spell? spell = stateMachine.spellsEquipped[spellNo];
            if (spell == null) return;
            stateMachine.SwitchState(new PlayerSpellCastingState(stateMachine, (Spell)spell));
        }
        private void Jump()
        {
            GD.Print("Jump!");
        }


    }
}