using System.Dynamic;
using Godot;
namespace Animation;
[GlobalClass]
public partial class DirectionalAnimation : Resource
{
	[Export] public AnimationType type;
	[Export] public bool looping { get; private set; } = false;
	[Export] public Texture2D texture;
	[Export] public int YOffset = 0;
	[Export] public int length;
	[Export] public float[] times { get; private set; }


	public int animationNumber = 0;
}

public enum AnimationType
{
	Idle,
	Walking,
	Hurt,
	Death,
	Attack0,
	Attack1,
	Attack2,
}