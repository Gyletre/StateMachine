using Game.Combat;
using Game.UI;
using Godot;
using System;

namespace Game.StateMachine.EnemyState;

public partial class EnemyCombatState : EnemyBaseState
{
    Vector2[] hitBoxLocations;
    public EnemyCombatState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        GD.Print($"{stateMachine.Name} enter combat state");
        stateMachine.StartTurn = Activate;
        hitBoxLocations = stateMachine.hitBoxManager.GetHitBoxLocations();
        stateMachine.animator.SwitchAnimation(AnimationType.Idle);
    }

    private void Activate()
    {
        GD.Print(stateMachine.Name + " started round.");
        stateMachine.roundData = new RoundData(stateMachine.combatActionLimit,
                                               stateMachine.GlobalPosition,
                                               stateMachine.speed);
    }


    public override void Exit()
    {
    }

    public override void Tick(double delta)
    {
        if (stateMachine.roundData == null) return;
        int shortestDir;
        float shortestLen;
        Vector2 direction;

        GetClosestAttackDirection(out shortestDir, out shortestLen, out direction);

        stateMachine.animator.SwitchDirection(shortestDir);

        ApproachAndAttack(delta, shortestLen, direction);

    }

    private void ApproachAndAttack(double delta, float shortestLen, Vector2 direction)
    {
        if (shortestLen < stateMachine.hitBoxManager.GetHitBoxRadius())
        {
            stateMachine.roundData.combatActions--;
            stateMachine.SwitchState(new EnemyAttackState(stateMachine));
        }
        else
        {
            stateMachine.animator.SwitchAnimation(AnimationType.Walking);
            stateMachine.Velocity = direction.Normalized() * stateMachine.speed * (float)delta * 100;
            stateMachine.MoveAndSlide();
            if (stateMachine.GlobalPosition.DistanceTo(stateMachine.roundData.startPoint) > stateMachine.roundData.movementRange)
            {
                EndTurn();
            }
        }
        if (stateMachine.roundData.combatActions == 0)
        {
            EndTurn();
        }
    }


    private void EndTurn()
    {
        stateMachine.roundData = null;
        stateMachine.EndTurn.Invoke();
    }


    private void GetClosestAttackDirection(out int shortestDir, out float shortestLen, out Vector2 direction)
    {
        shortestDir = -1;
        shortestLen = 9001;
        direction = Vector2.Zero;
        for (int i = 0; i < hitBoxLocations.Length; i++)
        {
            Vector2 dir = stateMachine.player.GlobalPosition - hitBoxLocations[i] - stateMachine.GlobalPosition;
            if (dir.Length() < shortestLen)
            {
                shortestLen = dir.Length();
                shortestDir = i;
                direction = dir;
            }
        }
    }
}
