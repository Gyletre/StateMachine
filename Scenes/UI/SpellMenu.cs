using Godot;
using System.Collections.Generic;

namespace Game.UI;

public partial class SpellMenu : Control
{
    [Export] PackedScene[] spellScenes;
    public Dictionary<Spell, PackedScene> spellLibrary;
    PlayerManageable player;
    Vector2 baseEntryLocation;
    Vector2 entryOffset;


    Node menu;
    bool menuVisible = false;
    public override void _Ready()
    {
        player = SceneManager.player;
        menu = GetChild(0);
        InitializeAllSpells();
        RemoveChild(menu);
    }

    private void InitializeAllSpells()
    {
        for (int i = 0; i < spellScenes.Length; i++)
        {
            Spell currentClassifier = spellScenes[i].Instantiate<SpellEntry>().spellIdentity;
            spellLibrary.Add(currentClassifier, spellScenes[i]);
        }
        foreach (KeyValuePair<Spell, PackedScene> pair in spellLibrary)
        {

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
