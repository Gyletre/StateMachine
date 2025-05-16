using System;
using System.Dynamic;
using Godot;

namespace Core
{
    public partial class InputReader : Node
    {

        public Vector2 moveDirection { get; private set; } = new Vector2(0, 0);
        public event Action JumpEvent;
        public event Action DodgeEvent;
        bool isAttacking;

        public bool IsAttacking()
        {
            if (isAttacking)
            {
                isAttacking = false;
                return true;
            }
            return false;
        }

        public override void _Process(double delta)
        {
            if (Input.IsActionJustPressed("attack"))
                isAttacking = true;

            if (Input.IsActionJustPressed("dodge"))
            {
                DodgeEvent?.Invoke();
                GD.Print("dodge");
            }
            if (Input.IsActionJustPressed("jump"))
            {
                JumpEvent?.Invoke();
            }
            float x, y;
            x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
            y = Input.GetActionStrength("down") - Input.GetActionStrength("up");
            moveDirection = new Vector2(x, y).Normalized();
        }

    }
}