using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Quic;
using Game.UI;
using Game.SceneManagement;
using Game.Combat;

namespace Game.Quest;

public partial class QuestManager : Node2D
{
    [Export(PropertyHint.File)] string questWindow;
    static QuestManager instance;
    static List<Quest> quests = new();
    static Quest maybeQuest;

    public static void AddQuest(Quest quest)
    {
        foreach (Quest activeQuest in quests)
        {
            if (activeQuest.questName == quest.questName)
            {
                return;
            }
        }
        QuestUI window = ResourceLoader.Load<PackedScene>(instance.questWindow).Instantiate<QuestUI>();
        window.SetupQuestUI(quest.questName, quest.questDescription, quest.spellQuestReward, quest.goldQuestReward, ActivateQuest);
        instance.GetNode("/root/SceneManager/UI").AddChild(window);
        maybeQuest = quest;
    }

    private static void ActivateQuest()
    {
        if (maybeQuest == null) return;
        quests.Add(maybeQuest);
        TextMessageWriter.Print("Start " + maybeQuest.questName + " quest");
    }

    /// <summary>
    /// Checks if quest is completed
    /// </summary>
    /// <param name="quest">The quest to check</param>
    /// <returns>True if quest is already completed, false otherwise</returns>
    public static bool CheckForCompletedQuest(Quest quest)
    {
        foreach (Quest activeQuest in quests)
        {
            if (activeQuest.questName == quest.questName)
            {
                return activeQuest.completed;
            }
        }
        return false;
    }
    public override void _Ready()
    {
        instance = this;
        CombatManager.OnAllEnemiesDefeated += FinishDefeatEnemyQuest;
    }

    private void FinishDefeatEnemyQuest()
    {
        foreach (Quest q in quests)
        {
            if (q.questType == QuestType.KillAllEnemies)
            {

                q.CompleteQuest(SceneManager.GetCurrentLevelIdentifier());
            }
        }
    }

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