using Godot;
using System.Collections.Generic;

namespace Game.UI
{
    public partial class TextMessageWriter : Node2D
    {
        [Export] float fadeTime = 2f;
        static List<Label> messages = new();
        static TextMessageWriter instance;
        static int offset = 20;
        public static void Print(string message)
        {
            Label label = new Label();
            label.Text = message;
            messages.Add(label);
            instance.AddChild(label);
            label.Position = new Vector2(-label.Size.X / 2, -offset * (messages.Count - 1));
        }
        public override void _Ready()
        {
            instance = this;
        }
        public override void _Process(double delta)
        {
            for (int i = 0; i < messages.Count; i++)
            {
                var color = messages[i].Modulate;
                color.A -= (float)delta / (fadeTime * messages[i].Text.Length / 10);
                if (color.A <= 0)
                {
                    messages[i].QueueFree();
                    messages.Remove(messages[i]);
                    i--;
                    continue;
                }

                else messages[i].Modulate = color;
                if (messages[i].Position.Y < -offset * i)
                {
                    messages[i].Position = new Vector2(0, messages[i].Position.Y + 1);
                }

            }
        }


    }
}