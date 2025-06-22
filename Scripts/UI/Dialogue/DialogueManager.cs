using Godot;
using System;

namespace Game.Dialogue;

public partial class DialogueManager : Control
{
    public static DialogueManager instance;
    [Export] DialoguePortraitManager dpm;
    [Export] RichTextLabel text;

    DialogueEntry[] currentDialogue;
    int dialogNo = 0;
    bool writingToScreen = false;
    bool ignoreClick = false;
    bool progressForce = false;

    public override void _Ready()
    {
        instance = this;
        GetChild<CanvasItem>(0).Visible = false;

    }
    public override void _Process(double delta)
    {
        if (writingToScreen || ignoreClick) return;
        if (!progressForce && (!Input.IsActionJustPressed("click") || currentDialogue == null)) { return; }
        progressForce = false;
        if (dialogNo >= currentDialogue.Length)
        {
            EndDialogue();
            return;
        }
        LoadDialogueEntry(dialogNo);
        dialogNo += currentDialogue[dialogNo].jumpDistance;
    }


    public void StartDialogue(DialogueEntry[] chat, bool completedQuest)
    {
        if (completedQuest)
        {
            for (int i = 0; i < chat.Length; i++)
            {
                if (chat[i].type == DialogueEntryType.CompletedQuest) dialogNo = i;
            }
        }
        if (chat == null) return;
        progressForce = true;
        GetChild<CanvasItem>(0).Visible = true;
        currentDialogue = chat;
    }

    private void LoadDialogueEntry(int dialogNo)
    {
        DialogueEntry item = currentDialogue[dialogNo];
        if (item.type == DialogueEntryType.PlayerTalks)
        {
            dpm.SetEmotion(item.pEmotion);
            GetChild<Node2D>(0).Position = Vector2.Zero;
        }
        else
        {
            GetChild<Node2D>(0).Position = new Vector2(-190, 0);
        }
        writingToScreen = true;
        text.Text = "";
        LoadTextOverTime(item.text, 0, 0.05f, dialogNo);
    }

    private void LoadTextOverTime(string text, int i, float time, int dNo)
    {
        if (i >= text.Length)
        {
            var dialogue = currentDialogue[dNo];
            writingToScreen = false;
            if (dialogue.type == DialogueEntryType.NPCtalksAnswer)
                GD.Print(dialogue.answerOptions);
            else if (dialogue.type == DialogueEntryType.QuestGive)
            {
                ignoreClick = true;
                if (dialogue.action != null)
                    dialogue.action?.Invoke(ReenableclickToProgressDialogue);
                else
                {
                    GD.Print("issue loading quest");
                }
            }
            return;
        }
        GetTree().CreateTimer(time).Timeout += () =>
        {
            this.text.AppendText(text[i].ToString());
            LoadTextOverTime(text, i + 1, time, dNo);
        };
    }

    private void ReenableclickToProgressDialogue()
    {
        ignoreClick = false;
        progressForce = true;
    }

    private void EndDialogue()
    {
        currentDialogue = null;
        dialogNo = 0;
        GetChild<CanvasItem>(0).Visible = false;
    }
}
