using System;
using Godot;

namespace Game.StateMachine.PlayerState;

public class PlayerHurtState : PlayerBaseState
{
    public PlayerHurtState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SwitchAnimation(AnimationType.Hurt, OnEnd: ReturnToMove);
        stateMachine.OnDamageTaken?.Invoke(stateMachine.hp);
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
        stateMachine.ReturnToMovementState();
    }
}
