using System.Dynamic;
using Godot;
namespace Game
{
	[GlobalClass]
	public partial class DirectionalAnimation : Resource
	{
		[Export] public AnimationType type;
		[Export] public bool looping { get; private set; } = false;
		[Export] public Texture2D texture;
		[Export] public int YOffset = 0;
		[Export] public int length;
		[Export] public float[] times { get; private set; }
		[Export] public int hitFrame = -1;

		public int frameNumber = 0;
	}


	/// <summary>
	/// All animation types. All types below "Attack" is Player specific
	/// </summary>
	public enum AnimationType
	{
		Idle,
		Walking,
		Hurt,
		Death,
		Attack,
		MeleeAttack,
		CastSpell,
		RangedAttack,
		StartFishing,
		FishIdle,
		FishJerk,
		Grab,
		Hammering,
		Hoeing,
		ItemGot,
		Jump,
		Mining,
		Watering,
		Woodcutting
	}
}
