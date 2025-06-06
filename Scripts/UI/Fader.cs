using Godot;
using System;

namespace Game.SceneManagement;

public partial class Fader : Control
{
    public Action OnFadedOut;
    CanvasItem fade;
    Fade currentFading;
    float currentAlpha;
    public override void _Ready()
    {
        fade = GetChild<CanvasItem>(0);
        currentAlpha = 1;
        SetAlpha(currentAlpha);
        currentFading = Fade.In;
        SceneManager.fader = this;
    }

    public override void _Process(double delta)
    {
        if (currentFading == Fade.None) return;
        if (currentFading == Fade.Out)
        {
            currentAlpha += (float)delta;
            if (currentAlpha >= 1)
            {
                currentAlpha = 1;
                currentFading = Fade.None;
                OnFadedOut?.Invoke();

            }
        }
        else if (currentFading == Fade.In)
        {
            currentAlpha -= (float)delta;
            if (currentAlpha <= 0)
            {
                currentAlpha = 0;
                currentFading = Fade.None;
            }

        }
        SetAlpha(currentAlpha);
    }

    public void FadeOut(Action OnFadedOut)
    {
        this.OnFadedOut = OnFadedOut;
        currentFading = Fade.Out;
    }
    public void FadeIn()
    {
        currentFading = Fade.In;
    }
    private void SetAlpha(float a)
    {
        var color = fade.Modulate;
        color.A = a;
        fade.Modulate = color;
    }
    enum Fade
    {
        None,
        Out,
        In
    }
}
