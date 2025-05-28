using Godot;
using System;
using System.Dynamic;
using Game.UI;

namespace Game.Quest;

public partial class Quest : Node
{
    [Export] public string questName;
    [Export] public QuestType questType;
    [Export(PropertyHint.MultilineText)] public string questDescription;
    [Export] public Spell spellQuestReward;
    [Export] public int goldQuestReward;
    [Export] public LevelIdentifier questLocation;

    public bool completed { get; private set; } = false;
    public void StartQuest()
    {
        if (!QuestManager.CheckForCompletedQuest(this))
        {
            QuestManager.AddQuest(this);
        }
        else
        {
            TextMessageWriter.Print("TYSM, here is your reward");
            if (spellQuestReward != Spell.None) SceneManager.player.LearnSpell(spellQuestReward);
            if (goldQuestReward > 0) TextMessageWriter.Print("+ " + goldQuestReward + " gold");
            QueueFree();
        }

    }
    public void CompleteQuest(LevelIdentifier level)
    {
        if (questLocation == level)
        {
            TextMessageWriter.Print("completed " + questName + " quest");
            completed = true;
        }
    }
}