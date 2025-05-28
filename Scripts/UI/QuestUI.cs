using Godot;
using System;
using Game.UI;

namespace Game.Quest;

public partial class QuestUI : Control
{
    [Export] Label title;
    [Export] Label description;
    [Export] TileButton button;
    public void SetupQuestUI(string questTitle, string questDescription, Spell spellReward, int moneyReward, Action OnAccept)
    {
        title.Text = questTitle;
        string descript = questDescription + "\n Reward(s):";
        if (spellReward != Spell.None)
        {
            descript += "\n" + Enum.GetName(spellReward) + " spell";
        }
        if (moneyReward > 0)
        {
            descript += "\n" + moneyReward + " gold";
        }
        description.Text = descript;
        button.OnPressed += OnAccept;
        button.OnPressed += RemoveUI;
    }

    private void RemoveUI()
    {
        QueueFree();
    }

}
