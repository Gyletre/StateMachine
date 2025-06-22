using Godot;
using System;

public partial class DialoguePortraitManager : Sprite2D
{
    [Export] Texture2D neutral;
    [Export] Texture2D happy;
    [Export] Texture2D angry;
    [Export] Texture2D sad;
    [Export] Texture2D asleep;
    [Export] Texture2D nervous;
    public void SetEmotion(PortraitEmotions emotion){
        switch (emotion)
        {
            case PortraitEmotions.neutral:
                Texture = neutral;
                break;
            case PortraitEmotions.happy:
                Texture = happy;
                break;
            case PortraitEmotions.angry:
                Texture = angry;
                break;
            case PortraitEmotions.sad:
                Texture = sad;
                break;
            case PortraitEmotions.asleep:
                Texture = asleep;
                break;
            case PortraitEmotions.nervous:
                Texture = nervous;
                break;
            default:
                return;
        }
    }
}
public enum PortraitEmotions{
    neutral,
    happy,
    angry,
    sad,
    asleep,
    nervous
}
