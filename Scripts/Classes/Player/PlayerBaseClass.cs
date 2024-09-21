using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Godot;
namespace Classes{
    public class PlayerBaseClass : Class{
        protected Dictionary<int, Action<Node2D>> abilities = new Dictionary<int,Action<Node2D>>();
        public PlayerBaseClass(){
            CreateClass(new StatSpread(10,10,10,10,10,10));
        }
        public List<Action<Node2D>> GetAbilities() //gets abilities the player has appropriate level for
        {
            if (abilities == null) return null;
            List<Action<Node2D>> list = new List<Action<Node2D>>();
            foreach((int level, var ability) in abilities){
                if (this.level >= level)
                {
                    list.Add(ability);
                }
            }
            return list;
        }
        


    }
}