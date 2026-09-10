using Godot;
using Game.SceneManagement;
namespace Game.Camera;

public partial class Camera : Camera2D
{
    public static Camera instance;
    public CameraMode mode;
    public Vector2 combatTarget;
    PlayerManageable player;
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
    public void SetCameraMode(CameraMode cameraMode)
    {
        mode = cameraMode;
    }
    public override void _Ready()
    {
        if (instance == null)
        {
            mode = CameraMode.FreeMove;
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
        if (mode == CameraMode.Cutscene) return;
        if (player != null && mode == CameraMode.FreeMove)
            GlobalPosition = player.GetGlobalPos();
        else if (mode == CameraMode.Combat)
        {

        }
    }

}
public enum CameraMode
{
    FreeMove,
    Combat,
    Cutscene
}
