using System;
using System.Dynamic;
using Godot;

namespace Game
{
    public partial class InputReader : Node
    {

        public Vector2 moveDirection { get; private set; } = new Vector2(0, 0);
        public event Action JumpEvent;
        public event Action DodgeEvent;
        public Action<int>[] abilities = new Action<int>[3];
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
        public override void _Input(InputEvent @event)
        {
            if (Input.IsActionJustPressed("attack")) isAttacking = true;
            if (Input.IsActionJustPressed("dodge")) DodgeEvent?.Invoke();
            if (Input.IsActionJustPressed("jump")) JumpEvent?.Invoke();

            if (Input.IsActionJustPressed("ability1")) abilities[0]?.Invoke(0);
            if (Input.IsActionJustPressed("ability2")) abilities[1]?.Invoke(1);
            if (Input.IsActionJustPressed("ability3")) abilities[2]?.Invoke(2);
            moveDirection = new Vector2(Input.GetActionStrength("right") - Input.GetActionStrength("left"),
                                        Input.GetActionStrength("down") - Input.GetActionStrength("up")
                                        ).Normalized();
        }
    }
}