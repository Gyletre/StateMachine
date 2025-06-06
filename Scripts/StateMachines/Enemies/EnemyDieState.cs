using System;
using Godot;
using Game.UI;
using Game.Animation;
using Game.SceneManagement;
using Game.Saving;

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
        SceneManager.enemies.Remove(stateMachine);
        foreach (Node child in stateMachine.GetChildren())
        {
            if (child is SaveableEntity || child is Animator) continue;
            child.QueueFree();
        }
    }

    public override void Tick(double delta) { }

    public override void Exit() { }


    private void Die()
    {
        stateMachine.Visible = false;
    }
}