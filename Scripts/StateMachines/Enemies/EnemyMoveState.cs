using Godot;


namespace Game.StateMachine.EnemyState;

public class EnemyMoveState : EnemyBaseState
{
    Vector2[] hitBoxLocations;
    public EnemyMoveState(EnemyStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        hitBoxLocations = stateMachine.hitBoxManager.GetHitBoxLocations();
        stateMachine.animator.SwitchAnimation(AnimationType.Idle);
        if (stateMachine.hp <= 0) stateMachine.SwitchState(new EnemyDieState(stateMachine));
    }
    public override void Tick(double delta)
    {
        if (stateMachine.detectedPlayer)
        {
            int shortestDir;
            float shortestLen;
            Vector2 direction;

            GetClosestAttackDirection(out shortestDir, out shortestLen, out direction);

            stateMachine.animator.SwitchDirection(shortestDir);
            if (shortestLen < stateMachine.hitBoxManager.GetHitBoxRadius())
            {
                if (stateMachine.attackCooldownTime == 0)
                    stateMachine.SwitchState(new EnemyAttackState(stateMachine));
            }
            else
            {
                stateMachine.animator.SwitchAnimation(AnimationType.Walking);
                stateMachine.Velocity = direction.Normalized() * stateMachine.speed * (float)delta * 100;
                stateMachine.MoveAndSlide();
            }

        }
        else
        {
            stateMachine.animator.SwitchAnimation(AnimationType.Idle);
        }

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

    public override void Exit()
    {

    }
}