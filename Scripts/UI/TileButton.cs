using Godot;
using System;

public partial class TileButton : TileMapLayer
{
    [Export] Vector2I buttonDimensions;
    [Export] Vector2I hoveredOffset;
    [Export] Vector2I pressedOffset;

    public event Action OnPressed;

    Area2D clickableArea;
    Action onClick;
    Vector2I[,] baseAtlas;
    Vector2I offset;



    public override void _Ready()
    {
        clickableArea = GetChild<Area2D>(1);
        clickableArea.MouseEntered += ActivateClick;
        clickableArea.MouseExited += DeactivateClick;
        baseAtlas = new Vector2I[buttonDimensions.X, buttonDimensions.Y];
        for (int i = 0; i < buttonDimensions.X; i++)
        {
            for (int j = 0; j < buttonDimensions.Y; j++)
            {
                baseAtlas[i, j] = GetCellAtlasCoords(new Vector2I(i, j));
            }
        }
    }

    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("click")) onClick?.Invoke();
    }



    private void ActivateClick()
    {
        ChangeButtonColor(hoveredOffset);
        onClick += PressButton;
    }
    private void DeactivateClick()
    {
        ChangeButtonColor(Vector2I.Zero);
        onClick -= PressButton;
    }

    private void ChangeButtonColor(Vector2I newOffset)
    {
        offset = newOffset;
        for (int i = 0; i < buttonDimensions.X; i++)
        {
            for (int j = 0; j < buttonDimensions.Y; j++)
            {
                Vector2I cell = new Vector2I(i, j);
                SetCell(cell, 0, baseAtlas[i, j] + offset);
            }
        }
    }

    private void PressButton()
    {
        ChangeButtonColor(pressedOffset);
        GD.Print("pressed!");
        OnPressed?.Invoke();
        GetTree().CreateTimer(0.1f).Timeout += () =>
        {
            ChangeButtonColor(Vector2I.Zero);
        };
    }
}
