using Godot;
using System;
using System.Collections.Generic;

namespace Game.Quest;

public partial class QuestManager : Node2D
{
    public static List<Quest> quests { get; private set; } = new();

    public static void AddQuest(Quest quest)
    {
        foreach (Quest activeQuest in quests)
        {
            if (activeQuest.questName == quest.questName)
            {
                return;
            }
        }
        GD.Print("Start " + quest.questName + " quest");
        quests.Add(quest);
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
        SceneManager.OnAllEnemiesDefeated += FinishDefeatEnemyQuest;
    }

    private void FinishDefeatEnemyQuest()
    {
        foreach (Quest q in quests)
        {
            if (q.questType == QuestType.KillAllEnemies)
            {
                GD.Print("completed " + q.questName + " quest");
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