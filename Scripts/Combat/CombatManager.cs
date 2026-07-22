using Godot;
using Game.SceneManagement;
using System.Collections.Generic;
using System;
using Game.UI;

namespace Game.Combat;

public partial class CombatManager : Node
{
    static int next;
    static List<CombatParticipant> combat_order = new();
    public static void StartCombat()
    {
        if (SceneManager.player is CombatParticipant p)
        {
            p.BeginCombat();
            combat_order.Add(p);
        }
        foreach (Enemy i in SceneManager.enemies)
        {
            if (i is CombatParticipant e)
            {
                e.BeginCombat();
                combat_order.Add(e);
            }
        }
        foreach (CombatParticipant cp in combat_order)
        {
            cp.EndTurn = NextTurn;
        }
        ShuffleParticipants();
        next = 0;
        NextTurn();
    }

    private static void NextTurn()
    {
        string namelist = "";
        foreach (Node2D participant in combat_order)
        {
            namelist += " ";
            namelist += (participant != null) ? participant.Name : "NULL";
        }
        GD.Print($"Combat participants: {namelist}");

        while (combat_order[next] == null)
        {
            GD.Print("Removing null participant");
            combat_order.Remove(null);
            next %= combat_order.Count;
        }
        GD.Print($"{((Node2D)combat_order[next]).Name}'s turn.");
        combat_order[next].StartTurn?.Invoke();

        next++;
        next %= combat_order.Count;
        GD.Print($"Next turn is {((Node2D)combat_order[next]).Name}'s turn.");
    }
    public override void _Process(double delta)
    {
        if (SceneManager.enemies.Count == 0)
        {
            //Player wins
        }
        else if (SceneManager.player == null)
        {
            //Enemies win
        }
        foreach (CombatParticipant cp in combat_order)
        {
            cp.EndCombat();
        }
    }



    private static void ShuffleParticipants()
    {
        RandomNumberGenerator rng = new();
        int n = combat_order.Count;
        while (n > 1)
        {
            n--;
            int k = rng.RandiRange(0, n);
            CombatParticipant cp = combat_order[k];
            combat_order[k] = combat_order[n];
            combat_order[n] = cp;
        }
    }

}
