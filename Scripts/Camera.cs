using Godot;
using Scene;
using System;
using System.Data;

public partial class Camera : Camera2D
{
    public static Camera instance;
    Node2D player;
    /// <summary>
    /// Sets camera restrictions by sending position of top left corner and bottom right corner of current map
    /// </summary>
    /// <param name="TL">Top left corner</param>
    /// <param name="BR">Bottom right corner</param>
    public void SetCameraConstraints(Vector2I TL, Vector2I BR)
    {
        LimitLeft = TL.X;
        LimitTop = TL.Y;
        LimitRight = BR.X;
        LimitBottom = BR.Y;
    }
    public override void _Ready()
    {
        if (instance == null)
        {
            instance = this;
            player = SceneManager.player;
        }
        else
        {
            QueueFree();
        }

    }
    public override void _Process(double delta)
    {
        if (player != null)
            GlobalPosition = player.GlobalPosition;
    }



}
