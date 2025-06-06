using Godot;
using System;
using System.Collections.Generic;
using Game.SceneManagement;

namespace Game.UI;

public partial class SpellMenu : Control
{
    [Export] PackedScene[] spellScenes;
    [Export] Vector2 baseEntryLocation = new Vector2(88, 54);
    [Export] Vector2 entryOffset = new Vector2(0, 48);
    public Dictionary<Spell, PackedScene> spellLibrary = new();
    PlayerManageable player;

    Vector2 entryLocation;

    Node menu;
    bool menuVisible = false;
    public override void _Ready()
    {
        entryLocation = baseEntryLocation;
        player = SceneManager.player;
        menu = GetChild(0);
        InitializeAllSpells();
        RemoveChild(menu);
        player.OnSpellAdded += UpdateSpells;
    }

    private void InitializeAllSpells()
    {
        for (int i = 0; i < spellScenes.Length; i++)
        {
            Spell currentClassifier = spellScenes[i].Instantiate<SpellEntry>().spellIdentity;
            spellLibrary.Add(currentClassifier, spellScenes[i]);
        }
        UpdateSpells();
    }
    private void UpdateSpells()
    {
        entryLocation = baseEntryLocation;
        foreach (Node n in menu.GetChildren()) n.QueueFree();
        var spells = player.GetKnownSpells();
        foreach (KeyValuePair<Spell, PackedScene> pair in spellLibrary)
        {
            if (spells.Contains(pair.Key))
            {
                SpellEntry spellEntry = pair.Value.Instantiate<SpellEntry>();
                menu.AddChild(spellEntry);
                spellEntry.Position = entryLocation;
                entryLocation += entryOffset;
            }
        }
    }


    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("menu"))
        {
            if (!menuVisible)
            {
                AddChild(menu);
            }
            else
            {
                RemoveChild(menu);
            }
            menuVisible = !menuVisible;
        }

    }

}
