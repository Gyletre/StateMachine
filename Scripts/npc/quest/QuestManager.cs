using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Quic;
using Game.UI;
using Game.SceneManagement;

namespace Game.Quest;

public partial class QuestManager : Node2D, ISaveable
{
    [Export(PropertyHint.File)] string questWindow;
    static QuestManager instance;
    static Dictionary<string, QuestProgression> quests = new();
    static Quest maybeQuest;

    public static void AddQuest(Quest quest)
    {
        if (quests.ContainsKey(quest.questName))
        {
            return;
        }
        QuestUI window = ResourceLoader.Load<PackedScene>(instance.questWindow).Instantiate<QuestUI>();
        window.SetupQuestUI(quest.questName, quest.questDescription, quest.spellQuestReward, quest.goldQuestReward, ActivateQuest);
        instance.GetNode("/root/SceneManager/UI").AddChild(window);
        maybeQuest = quest;
    }

    private static void ActivateQuest()
    {
        if (maybeQuest == null) return;
        quests[maybeQuest.questName] = QuestProgression.InProgress;
        TextMessageWriter.Print("Start " + maybeQuest.questName + " quest");
    }

    /// <summary>
    /// Checks if quest is completed
    /// </summary>
    /// <param name="quest">The quest to check</param>
    /// <returns>True if quest is already completed, false otherwise</returns>
    public static QuestProgression CheckQuestProgression(Quest quest)
    {
        if (quests.ContainsKey(quest.questName))
        {
            return quests[quest.questName];
        }
        return QuestProgression.NotStarted;
    }
    public override void _Ready()
    {
        instance = this;
    }



    public ISaveData SaveState()
    {
        var data = new QuestData();
        data.quests = quests;
        return data;
    }

    public void LoadState(ISaveData state)
    {
        if (state is QuestData data)
        {
            quests = data.quests;
        }
    }

    internal static void CompleteQuest(Quest quest)
    {
        quests[quest.questName] = QuestProgression.Completed;
    }


    public class QuestData : ISaveData
    {
        public Dictionary<string, QuestProgression> quests { get; set; }
    }
}

public enum QuestProgression
{
    NotStarted,
    InProgress,
    Completed
}

public enum QuestType
{
    KillAllEnemies,
    FetchItem,
    TalkToOtherNPC,
}
public enum RewardType
{
    SpellReward,
    MoneyReward,
    ProgressionReward,
}