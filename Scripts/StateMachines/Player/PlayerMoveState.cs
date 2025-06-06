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
            Move(delta);
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
            Spell spell = stateMachine.spellsEquipped[spellNo];
            if (spell == Spell.None) return;
            stateMachine.SwitchState(new PlayerSpellCastingState(stateMachine, spell));
        }
        private void Jump()
        {
            stateMachine.SwitchState(new PlayerJumpingState(stateMachine));
        }

    }
}