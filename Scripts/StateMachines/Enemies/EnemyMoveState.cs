using Godot;
using Game.Combat;


namespace Game.StateMachine.EnemyState;

public class EnemyMoveState : EnemyBaseState
{

    public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        GD.Print("Enter move state");
        stateMachine.animator.SwitchAnimation(AnimationType.Idle);
        stateMachine.StartTurn = null;
    }
    public override void Tick(double delta)
    {
        if (stateMachine.player != null)
        {
            CombatManager.StartCombat();
        }
        else
        {
            stateMachine.animator.SwitchAnimation(AnimationType.Idle);
        }

    }



    public override void Exit()
    {

    }
}