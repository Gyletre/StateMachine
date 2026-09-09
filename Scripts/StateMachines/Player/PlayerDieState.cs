using Game.UI;
using Godot;

namespace Game.StateMachine.PlayerState;

public class PlayerDieState : PlayerBaseState
{
    public PlayerDieState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }
    public override void Enter()
    {
        stateMachine.animator.SwitchAnimation(AnimationType.Death, OnEnd: GameOver);
        stateMachine.GetChild<CollisionShape2D>(0, true).Free();
    }
    public override void Tick(double delta) { }
    public override void Exit() { }
    private void GameOver()
    {
        TextMessageWriter.Print("Game over");
        stateMachine.QueueFree();
    }

}