using System;
using System.Dynamic;
using Godot;

namespace Core{
    public partial class InputReader: Node2D{
        public bool isAttacking;
        public Vector2 moveDirection { get; private set; } = new Vector2(0, 0);
        public event Action JumpEvent;
        public event Action DodgeEvent;
        public event Action TargetEvent;
        
        public override void _Process(double delta)
        {
            isAttacking = Input.IsActionPressed("attack");
            
            if (Input.IsActionJustPressed("dodge")){
                DodgeEvent?.Invoke();
                    GD.Print("dodge");
            }
            if (Input.IsActionJustPressed("jump")){
                JumpEvent?.Invoke();
            }
            float x,y = 0f;
            x = Input.GetActionStrength("right") - Input.GetActionStrength("left");
            y = Input.GetActionStrength("down") - Input.GetActionStrength("up");
            moveDirection = new Vector2(x,y);
        }
        
    }
}