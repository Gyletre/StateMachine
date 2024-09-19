using System.Reflection.Metadata;
using Godot;
using Classes;

namespace StateMachine{
    public class PlayerAttackingState : PlayerBaseState
    {
        
        public PlayerAttackingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void Enter()
        {
            stateMachine.animator.Play("attack");
            stateMachine.attackFinished = false;
            GD.Print("Attack: " + stateMachine.classManager.GetStat(StatType.Attack) + " Magic: " + stateMachine.classManager.GetStat(StatType.Magic));
            
        }
        public override void Tick(double delta)
        {
            if (!stateMachine.animator.IsPlaying()){
                stateMachine.SwitchState(new PlayerMoveState(stateMachine));
            }
            
            
        }
        public override void Exit()
        {
            
        }
        
        
    }
}