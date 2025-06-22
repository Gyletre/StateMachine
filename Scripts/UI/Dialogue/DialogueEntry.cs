using Godot;
using System;
namespace Game.Dialogue;

[GlobalClass]
public partial class DialogueEntry : Resource
{
    [Export] public string text;
    [Export] public DialogueEntryType type;
    [Export] public PortraitEmotions pEmotion;
    [Export] public string[] answerOptions;
    [Export] public int[] answerJumps;
    [Export] public int jumpDistance = 1;
    public Action<Action> action;
}

public enum DialogueEntryType
{
    NPCtalks,
    PlayerTalks,
    QuestGive,
    ShopOpen,
    NPCtalksAnswer,
    CompletedQuest,
    PendingQuest
}