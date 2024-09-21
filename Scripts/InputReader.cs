using System;
using System.Dynamic;
using Godot;

namespace Core{
    public partial class InputReader: Node2D{
        Node2D player;
        public override void _Ready()
        {
            player = GetParent<Node2D>();
        }
        public bool isAttacking;
        public Vector2 moveDirection { get; private set; } = new Vector2(0, 0);
        public event Action JumpEvent;
        public event Action DodgeEvent;
        public event Action TargetEvent;
        public event Action<Node2D> ability1;
        public event Action<Node2D> ability2;
        public event Action<Node2D> ability3;

        public override void _UnhandledInput(InputEvent evt)
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
            if (evt.IsActionPressed("ability1")){
                ability1?.Invoke(player);
            }
            if (evt.IsActionPressed("ability2")){
                ability2?.Invoke(player);
            }
            if (evt.IsActionPressed("ability3")){
                ability3?.Invoke(player);
            }
        }
        
    }
}