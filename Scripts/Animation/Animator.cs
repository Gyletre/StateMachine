using Godot;
using System;
using System.Diagnostics;

namespace Animation;
public partial class Animator : Sprite2D
{
	// Only one animation per tag
	[Export] DirectionalAnimation[] animations;

	DirectionalAnimation currentAnimation;
	float animationTime;
	int direction = 0;
	Action OnEnd;
	bool animationEnded = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		currentAnimation = animations[0];
		Hframes = currentAnimation.length;
		Vframes = 4;
		FrameCoords = new Vector2I(currentAnimation.animationNumber, direction);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (animationTime >= currentAnimation.times[currentAnimation.animationNumber])
		{
			animationTime = 0f;
			currentAnimation.animationNumber++;
			if (currentAnimation.animationNumber == currentAnimation.length && currentAnimation.looping)
			{
				currentAnimation.animationNumber = 0;
			}
			else if (currentAnimation.animationNumber == currentAnimation.length && !currentAnimation.looping)
			{
				currentAnimation.animationNumber--;
				animationEnded = true;
				OnEnd?.Invoke();
			}
		}
		animationTime += animationEnded ? 0 : (float)delta;
		FrameCoords = new Vector2I(currentAnimation.animationNumber, direction);


	}
	public void SwitchAnimation(AnimationType type, Action OnEnd = null)
	{
		if (currentAnimation.type == type) return;
		GD.Print("Switching to " + Enum.GetName(typeof(AnimationType), type));

		foreach (DirectionalAnimation da in animations)
		{
			if (da.type == type)
			{
				da.animationNumber = 0;
				currentAnimation = da;
				this.OnEnd = OnEnd;
				animationEnded = false;
				Hframes = da.length;
				animationTime = 0f;
				Texture = da.texture;
				Offset = new Vector2(0, da.YOffset);
				return;
			}
		}
		GD.Print("Animation with tag \"" + Enum.GetName(typeof(AnimationType), type) + "\" not found");
	}

	// Down = 0, Left = 1, Right = 2, Up = 3;
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
}

