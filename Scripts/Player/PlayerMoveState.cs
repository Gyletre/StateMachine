using System.Diagnostics;
using Godot;

namespace StateMachine{
    public class PlayerMoveState : PlayerBaseState
    {
        public PlayerMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
            stateMachine.inputReader.JumpEvent += Jump;
            stateMachine.canMove = true;
        }
        public override void Tick(double delta)
        {
            if(stateMachine.inputReader.isAttacking){
                stateMachine.SwitchState(new PlayerAttackingState(stateMachine));
            }
        }
        public override void Exit()
        {
            stateMachine.inputReader.JumpEvent -= Jump;
            stateMachine.canMove = false;
        }
        private void Jump() {
            GD.Print("Jump!");
        }

        
    }
}