using System;
using Godot;

namespace Game.StateMachine.EnemyState;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SwitchAnimation(AnimationType.Attack, OnHit, OnAttackFinished);
        stateMachine.attackCooldownTime = stateMachine.attackCooldown;
        stateMachine.invulnerable = true;
    }

    public override void Tick(double delta) { }

    public override void Exit() { }


    private void OnHit(int direction, float time)
    {
        stateMachine.hitBoxManager.MoveHitBox(direction, time, TargetHit);
        stateMachine.invulnerable = false;
    }

    private void TargetHit(Node2D body)
    {
        if (body is Player p)
        {
            p.TakeDamage(stateMachine.attack);
        }
    }

    private void OnAttackFinished()
    {
        stateMachine.SwitchState(new EnemyMoveState(stateMachine));
    }
}