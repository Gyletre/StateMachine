using System;
using Godot;

namespace StateMachine;

public class EnemyHurtState : EnemyBaseState
{
    public EnemyHurtState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SwitchAnimation(AnimationType.Hurt, OnEnd: OnEnd);
    }

    public override void Tick(double delta) { }

    public override void Exit() { }


    private void OnEnd()
    {
        if (stateMachine.hp <= 0)
        {
            stateMachine.SwitchState(new EnemyDieState(stateMachine));
            return;
        }
        stateMachine.SwitchState(new EnemyMoveState(stateMachine));
    }
}