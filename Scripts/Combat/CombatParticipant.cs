using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
namespace Game.Combat;

public interface CombatParticipant
{
    public void BeginCombat();
    public void EndCombat();
    public RoundData roundData { get; set; }
    public Action StartTurn { get; set; }
    public Action EndTurn { get; set; }
    public bool IsAlive { get; set; }
    public Vector2 GlobalPosition { get; set; }

}

public class RoundData
{
    public int combatActions;
    public Vector2 startPoint;
    public float movementRange;
    public RoundData(int comActions, Vector2 point, float speed)
    {
        combatActions = comActions;
        startPoint = point;
        movementRange = speed * 5;
    }
}
