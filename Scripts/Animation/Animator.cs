using Godot;
using System;
using System.Diagnostics;

namespace Game.Animation;

public partial class Animator : Sprite2D
{
	// Only one animation per tag
	[Export] DirectionalAnimation[] animations;
	[Export] float baseYOffset;

	public float animationSpeed = 1;

	DirectionalAnimation currentAnimation;
	float animationTime;
	int direction = 0;
	Action<int, float> OnHit;
	Action OnEnd;
	bool animationEnded = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentAnimation = animations[0];
		Hframes = currentAnimation.length;
		Vframes = 4;
		FrameCoords = new Vector2I(currentAnimation.frameNumber, direction);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (animationSpeed <= 0) return;
		if (animationTime >= currentAnimation.times[currentAnimation.frameNumber])
		{
			animationTime = 0f;
			currentAnimation.frameNumber++;
			if (currentAnimation.frameNumber == currentAnimation.length && currentAnimation.looping)
			{
				currentAnimation.frameNumber = 0;
			}
			else if (currentAnimation.frameNumber == currentAnimation.length && !currentAnimation.looping)
			{
				currentAnimation.frameNumber--;
				animationEnded = true;
				OnEnd?.Invoke();
			}
			else if (currentAnimation.hitFrame > 0 && currentAnimation.frameNumber == currentAnimation.hitFrame)
			{
				OnHit?.Invoke(direction, currentAnimation.times[currentAnimation.frameNumber]);
			}
		}
		animationTime += animationEnded ? 0 : (float)delta * animationSpeed;
		FrameCoords = new Vector2I(currentAnimation.frameNumber, direction);


	}
	public void SwitchAnimation(AnimationType type, Action<int, float> OnHit = null, Action OnEnd = null)
	{
		if (currentAnimation.type == type) return;

		foreach (DirectionalAnimation da in animations)
		{
			if (da.type == type)
			{
				da.frameNumber = 0;
				Offset = new Vector2(0, baseYOffset + da.YOffset);
				currentAnimation = da;
				this.OnHit = OnHit;
				this.OnEnd = OnEnd;
				animationEnded = false;
				Hframes = da.length;
				animationTime = 0f;
				Texture = da.texture;
				if (currentAnimation.hitFrame == 0)
					OnHit?.Invoke(direction, currentAnimation.times[currentAnimation.frameNumber]);

				return;
			}
		}
		GD.Print("Animation with tag \"" + Enum.GetName(typeof(AnimationType), type) + "\" not found");
	}

	/// <summary>
	/// Switches the direction the animation is facing
	/// </summary>
	/// <param name="direction">Changes when input direction is equal to Vector2.Down, Vector2.Left, Vector2.Right or Vector2.Up</param>
	public void SwitchDirection(Vector2 direction)
	{
		if (direction == Vector2.Down)
			this.direction = 0;
		else if (direction == Vector2.Left)
			this.direction = 1;
		else if (direction == Vector2.Right)
			this.direction = 2;
		else if (direction == Vector2.Up)
			this.direction = 3;
	}
	/// <summary>
	/// Switches the direction the animation is facing
	/// </summary>
	/// <param name="direction">Down = 0, Left = 1, Right = 2, Up = 3;</param>
	public void SwitchDirection(int direction)
	{
		this.direction = direction;
	}
}

