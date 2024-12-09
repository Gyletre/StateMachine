using System.Reflection.Metadata;
using Godot;
using Game.Classes;

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
            //start attack

        }
        public override void Tick(double delta)
        {
            //exit when animation is done
        }
        public override void Exit()
        {
            
        }


    }
}