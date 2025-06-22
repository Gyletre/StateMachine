using Godot;
using System;

namespace Game.Quest
{
    public partial class Npc : CharacterBody2D
    {
        [Export] Quest quest; // Quest object that is child outside object scene
        [Export] Interactable talkArea;
        [Export] Dialogue.Dialogue dialogue;
        public override void _Ready()
        {
            talkArea.OnInteract += StartDialogue;
            talkArea.SetInteractText("begin conversation");
        }

        private void StartDialogue()
        {
            dialogue.StartDialogue(quest.QuestInteraction, IsQuestCompleted());
        }

        private bool IsQuestCompleted()
        {
            if (quest == null) return false;
            else if (quest.IsCompleted())
            {
                quest.QuestInteraction(null);
                return true;
            }
            return false;
        }

    }
}

