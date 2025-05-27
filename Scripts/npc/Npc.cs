using Godot;
using System;

namespace Game.Quest
{
    public partial class Npc : CharacterBody2D
    {
        [Export] Quest quest; // Quest object that is child outside object scene
        [Export] Interactable talkArea;
        public override void _Ready()
        {
            talkArea.OnInteract += StartDialogue;
            talkArea.SetInteractText("start quest");
        }

        private void StartDialogue()
        {
            if (quest == null) return;
            quest.StartQuest();
        }
    }
}

