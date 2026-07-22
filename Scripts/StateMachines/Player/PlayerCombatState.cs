using Godot;
using System;
using Game.Combat;
using Game.UI;

namespace Game.StateMachine.PlayerState;

public partial class PlayerCombatState : PlayerBaseState
{
    Vector2[] hitBoxLocations;
    public PlayerCombatState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }


    public override void Enter()
    {
        GD.Print("Player entered combat state");
        stateMachine.StartTurn = Activate;
        stateMachine.inputReader.JumpEvent += EndTurn;
        hitBoxLocations = stateMachine.hitBoxManager.GetHitBoxLocations();
        for (int i = 0; i < stateMachine.inputReader.abilities.Length; i++)
        {
            stateMachine.inputReader.abilities[i] += ActivateAbility;
        }
        stateMachine.inputReader.IsAttacking();
    }


    public override void Tick(double delta)
    {
        if (stateMachine.roundData == null) { return; }
        Move(delta);
        RestrictMovement();
        if (stateMachine.inputReader.IsAttacking())
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine));
        }
        if (stateMachine.roundData.combatActions <= 0) EndTurn();
    }
    public override void Exit()
    {
        stateMachine.inputReader.JumpEvent -= EndTurn;
        for (int i = 0; i < stateMachine.inputReader.abilities.Length; i++)
        {
            stateMachine.inputReader.abilities[i] -= ActivateAbility;
        }

    }

    private void Activate()
    {
        GD.Print(stateMachine.Name + " started round.");
        stateMachine.roundData = new RoundData(stateMachine.combatActionLimit,
                                               stateMachine.GlobalPosition,
                                               stateMachine.speed);
    }


    private void RestrictMovement()
    {
        Vector2 offset = stateMachine.GlobalPosition - stateMachine.roundData.startPoint;
        if (offset.Length() > stateMachine.roundData.movementRange)
        {
            offset = offset.Normalized() * stateMachine.roundData.movementRange;
            stateMachine.GlobalPosition = stateMachine.roundData.startPoint + offset;
        }
    }
    private void ActivateAbility(int spellNo)
    {
        if (stateMachine.roundData == null) return;
        Spell spell = stateMachine.spellsEquipped[spellNo];
        if (spell == Spell.None) return;
        stateMachine.roundData.combatActions--;
        stateMachine.SwitchState(new PlayerSpellCastingState(stateMachine, spell));
    }

    private void EndTurn()
    {
        if (stateMachine.roundData == null) return;
        stateMachine.roundData = null;
        stateMachine.EndTurn.Invoke();
    }
}
