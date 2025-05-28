using System;
using Godot;
using Game.UI;

namespace Game.StateMachine.EnemyState;

public class EnemyDieState : EnemyBaseState
{
    public EnemyDieState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SwitchAnimation(AnimationType.Death, OnEnd: Die);
        stateMachine.invulnerable = true;
    }

    public override void Tick(double delta) { }

    public override void Exit() { }


    private void Die()
    {
        TextMessageWriter.Print(stateMachine.Name + " is dead");
        stateMachine.QueueFree();
    }
}