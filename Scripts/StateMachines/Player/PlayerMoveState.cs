using System.Diagnostics;
using System.Linq;
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
            if(stateMachine.classManager.abilities.Count > 0){
                stateMachine.inputReader.ability1 += stateMachine.classManager.abilities.First();
            }
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
            stateMachine.classManager.GainExp(100);
        }

        
    }
}