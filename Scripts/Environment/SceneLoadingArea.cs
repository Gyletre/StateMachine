using Game.UI;
using Godot;
using Game.SceneManagement;
namespace Game.SceneLoader;

public partial class SceneLoadingArea : Area2D
{
    [Export] SceneConfig sceneToLoad;
    public override void _Ready()
    {
        BodyEntered += LoadScene;
    }

    private void LoadScene(Node2D body)
    {
        if (body is Player)
        {
            if (sceneToLoad != null)
            {
                SceneManager.LoadMap(sceneToLoad);
            }
            else
            {
                TextMessageWriter.Print("No scene to load..");
            }
        }
    }
}
