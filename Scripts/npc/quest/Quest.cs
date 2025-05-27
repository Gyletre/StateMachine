using Godot;
using System;
using System.Dynamic;

namespace Game.Quest;

public partial class Quest : Node
{
    [Export] public string questName;
    [Export] public QuestType questType;
    [Export] Spell spellQuestReward;
    [Export] int goldQuestReward;
    //[Export] QuestLockedObject questLockedObj;
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
            GD.Print("TYSM, here is your reward");
            if (spellQuestReward != Spell.None) SceneManager.player.LearnSpell(spellQuestReward);
            if (goldQuestReward > 0) GD.Print("Added " + goldQuestReward + " gold");
            QueueFree();
        }

    }
    public void CompleteQuest(LevelIdentifier level)
    {
        if (questLocation == level)
        {
            completed = true;
        }
    }
}


