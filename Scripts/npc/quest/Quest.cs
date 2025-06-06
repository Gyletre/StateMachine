using Godot;
using System;
using System.Dynamic;
using Game.UI;
using Game.SceneManagement;

namespace Game.Quest;

public partial class Quest : Node, ISaveable
{
    [Export] public string questName;
    [Export] public QuestType questType;
    [Export(PropertyHint.MultilineText)] public string questDescription;
    [Export] public Spell spellQuestReward;
    [Export] public int goldQuestReward;
    [Export] public LevelIdentifier questLocation;

    public bool completed { get; private set; } = false;

    private bool rewardReceived = false;

    public void StartQuest()
    {
        if (rewardReceived) return;
        if (!QuestManager.CheckForCompletedQuest(this))
        {
            QuestManager.AddQuest(this);
        }
        else
        {
            TextMessageWriter.Print("TYSM, here is your reward");
            if (spellQuestReward != Spell.None) SceneManager.player.LearnSpell(spellQuestReward);
            if (goldQuestReward > 0) TextMessageWriter.Print("+ " + goldQuestReward + " gold");
            rewardReceived = true;
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

    public ISaveData SaveState()
    {
        QuestData data = new();
        data.rewardReceived = rewardReceived;
        return data;
    }

    public void LoadState(ISaveData state)
    {
        if (state is QuestData data)
            rewardReceived = data.rewardReceived;
    }

}
[Serializable]
public class QuestData : ISaveData
{
    public bool rewardReceived { get; set; }
}