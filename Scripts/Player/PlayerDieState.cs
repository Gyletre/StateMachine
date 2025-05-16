using System;
using Godot;
using Animation;

namespace StateMachine;

public class PlayerDieState : PlayerBaseState
{
    public PlayerDieState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        stateMachine.animator.SwitchAnimation(AnimationType.Death, OnEnd: GameOver);
        stateMachine.invincibilityTime = 5;
    }



    public override void Tick(double delta)
    {

    }

    public override void Exit()
    {

    }
    private void GameOver()
    {
        GD.Print("Game over");
        stateMachine.QueueFree();
    }

}