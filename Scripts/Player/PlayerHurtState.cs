using System;
using Godot;
using Animation;

namespace StateMachine;

public class PlayerHurtState : PlayerBaseState
{
    public PlayerHurtState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SwitchAnimation(AnimationType.Hurt, OnEnd: ReturnToMove);
        stateMachine.invincibilityTime = stateMachine.invincibility;
    }

    public override void Tick(double delta)
    {
    }

    public override void Exit()
    {
    }

    private void ReturnToMove()
    {
        if (stateMachine.hp <= 0)
        {
            stateMachine.SwitchState(new PlayerDieState(stateMachine));
            return;
        }
        stateMachine.SwitchState(new PlayerMoveState(stateMachine));
    }
}
