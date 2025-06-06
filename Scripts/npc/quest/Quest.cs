using Godot;
using System;
using System.Dynamic;
using Game.UI;
using Game.SceneManagement;

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

    private bool rewardReceived = false;

    public void QuestInteraction()
    {
        if (rewardReceived) return;
        if (QuestManager.CheckQuestProgression(this) == QuestProgression.NotStarted)
        {
            QuestManager.AddQuest(this);
        }
        else if (QuestManager.CheckQuestProgression(this) == QuestProgression.InProgress)
        {
            CheckIfQuestIsCompleted();
        }
        else if (QuestManager.CheckQuestProgression(this) == QuestProgression.Completed)
        {
            TextMessageWriter.Print("Thanks for doing my quest");
        }

    }

    private void CheckIfQuestIsCompleted()
    {
        switch (questType)
        {
            case QuestType.KillAllEnemies:
                if (!SceneManager.enemiesDefeatedInScene.ContainsKey(questLocation) || !SceneManager.enemiesDefeatedInScene[questLocation]) { return; }
                break;
            case QuestType.FetchItem:
                //return if item is not in inventory
                break;
            case QuestType.TalkToOtherNPC:
                //return if other npc is not talked to  
                break;
            default:
                return;
        }
        TextMessageWriter.Print("TYSM, here is your reward");
        if (spellQuestReward != Spell.None) SceneManager.player.LearnSpell(spellQuestReward);
        if (goldQuestReward > 0) TextMessageWriter.Print("+ " + goldQuestReward + " gold");
        QuestManager.CompleteQuest(this);

    }

}