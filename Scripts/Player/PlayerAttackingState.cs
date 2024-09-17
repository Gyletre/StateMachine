using System.Reflection.Metadata;
using Godot;

namespace StateMachine{
    public class PlayerAttackingState : PlayerBaseState
    {
        public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
            GD.Print("Attack");
            //start attack animation
        }
        public override void Tick(double delta)
        {
            //handle collision
            //return to testState after attack, or 
            
            stateMachine.SwitchState(new PlayerMoveState(stateMachine));
        }
        public override void Exit()
        {
            
        }

        
    }
}