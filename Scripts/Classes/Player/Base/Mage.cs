using System;
using System.Collections.Generic;
using Godot;

namespace Classes
{
    public class Mage : PlayerBaseClass
    {
        PackedScene fireBoltScene;

        public Mage()
        {
            fireBoltScene = GD.Load<PackedScene>("res://Scenes/Entities/Projectiles/Firebolt.tscn");
            identity = ClassList.Mage;
            CreateClass(new StatSpread(7, 5, 15, 12, 10, 10)); //base class needs stats of 15, 12, 10, 10, 7, 5
            abilities.Add(1, Firebolt);

        }
        public void Firebolt(Node2D instanitator)
        {
            GD.Print("Firebolt!");
            var fireBolt = fireBoltScene.Instantiate<Node2D>();
            fireBolt.GlobalPosition = instanitator.GlobalPosition;

            if (fireBolt is Firebolt fb)
            {
                fb.direction = (instanitator.GetGlobalMousePosition() - instanitator.GlobalPosition).Normalized();
                fb.insantiator = instanitator;
            }
            instanitator.AddSibling(fireBolt);

            // make particles
            // small explotion on hit
        }

    }
}