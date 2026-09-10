using Godot;
using Game.SceneManagement;
using System.Collections.Generic;
using System;
using Game.UI;
using Game.Camera;

namespace Game.Combat;

public partial class CombatManager : Node
{
    static int next;
    static List<CombatParticipant> combat_order = new();
    public static Action OnAllEnemiesDefeated;
    public static void StartCombat()
    {
        if (combat_order.Count > 0) return;
        Camera.Camera.instance.SetCameraMode(CameraMode.Combat);
        if (SceneManager.player is CombatParticipant p)
        {
            p.BeginCombat();
            combat_order.Add(p);
        }
        GD.Print(SceneManager.enemies.Count + " enemies found");
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
        GD.Print($"Combat participants: {combat_order.Count}");

        while (!combat_order[next].IsAlive || combat_order[next] == null)
        {
            GD.Print("Removing dead participant");
            combat_order.RemoveAt(next);
            next %= combat_order.Count;
        }
        GD.Print($"{((Node2D)combat_order[next]).Name}'s turn.");
        combat_order[next].StartTurn?.Invoke();

        Camera.Camera.instance.combatTarget = combat_order[next].GlobalPosition;


        next++;
        next %= combat_order.Count;
        GD.Print($"Next turn is {((Node2D)combat_order[next]).Name}'s turn.");
    }
    public static void EndCombat()
    {
        if (combat_order.Count == 0) return;
        foreach (CombatParticipant cp in combat_order)
        {
            cp.EndTurn = null;
            cp.EndCombat();
        }
        combat_order = new();
    }
    public override void _Ready()
    {
        SceneManager.OnSceneChanged += EndCombat;
    }

    public override void _Process(double delta)
    {
        if (combat_order.Count == 0) { return; }
        if (!combat_order[next].IsAlive) NextTurn();
        if (SceneManager.enemies.Count == 0)
        {
            GD.Print("Player wins");
            OnAllEnemiesDefeated?.Invoke();
            EndCombat();
        }
        else if (SceneManager.player == null)
        {
            GD.Print("Enemy wins");
            EndCombat();
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
