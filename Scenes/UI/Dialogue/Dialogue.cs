using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Game.Dialogue;

public partial class Dialogue : Node
{
    [Export] DialogueEntry[] dialogueEntries;

    public void StartDialogue(Action<Action> OnQuestStart = null, bool completedquest = false)
    {
        if (OnQuestStart != null)
        {
            foreach (var dialogue in dialogueEntries)
            {
                if (dialogue.type == DialogueEntryType.QuestGive)
                {
                    dialogue.action += OnQuestStart;
                }
            }
        }
        DialogueManager.instance.StartDialogue(dialogueEntries, completedquest);
    }
    public override string[] _GetConfigurationWarnings()
    {
        var warnings = new List<string>();
        foreach (DialogueEntry entry in dialogueEntries)
        {
            if (entry == null)
            {
                warnings.Append("dialogue entry cannot be null");
                continue;
            }
            if (entry.text == "")
            {
                warnings.Append("missing dialogue text");
            }
            if (entry.type == DialogueEntryType.NPCtalksAnswer && entry.answerOptions == null)
            {
                warnings.Append("missing answers to dialogue");
            }
            if (entry.answerOptions.Length > 4)
            {
                warnings.Append("too many answer options");
            }

        }
        return warnings.ToArray();
    }
}
