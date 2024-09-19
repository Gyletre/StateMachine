using System;
using System.Runtime.Serialization;
using System.Security.Cryptography.X509Certificates;

namespace Classes{
    public class Class{
        public ClassList identity {get; protected set;} = ClassList.Base;
        public StatSpread statScaling {get; private set;}
        public int exp {get; private set;}
        public bool maxed {get; private set;} = false;
        int level;

        public void GainExp(int amount){
            if(maxed) return;
            exp += amount;
            CheckForLevelUp();
        }
        public int ExpToLvlUp()
        {
            return (int)(90 + 10 * MathF.Pow(level, 2));
        }

        /// <summary>
        /// Necessary setup for the class
        /// </summary>
        /// <param name="statSpread">The stats of the class</param>
        protected void CreateClass(StatSpread statSpread){
            level = 1;
            exp = 0;
            statScaling = statSpread;
        }
        private void CheckForLevelUp()
        {
            if (exp >= ExpToLvlUp())
            {
                exp -= ExpToLvlUp();
                level++;
                ImproveStats();
                if(level >= 10){
                    level = 10;
                    maxed = true;
                }
            }
        }
        private void ImproveStats()
        {
            statScaling.LevelUp(level);
        }

        
        
        public int GetStat(StatType type){
            switch (type){
                case StatType.Health:
                    return statScaling.health.value;
                case StatType.Attack:
                    return statScaling.attack.value;
                case StatType.Magic:
                    return statScaling.magic.value;
                case StatType.Agility:
                    return statScaling.agility.value;
                case StatType.Defence:
                    return statScaling.defence.value;
                case StatType.Resist:
                    return statScaling.resist.value;
            }
            return -1;
        }

    }
    
    
    public struct StatSpread{
        public Stat health;
        public Stat attack;
        public Stat magic;
        public Stat agility;
        public Stat defence;
        public Stat resist;
        /// <summary>
        /// Constructor for the StatSpread
        /// </summary>
        /// <param name="baseHealth"></param>
        /// <param name="baseAttack"></param>
        /// <param name="baseMagic"></param>
        /// <param name="baseAgility"></param>
        /// <param name="baseDefence"></param>
        /// <param name="baseResist"></param>
        public StatSpread(int baseHealth,  
                          int baseAttack,  
                          int baseMagic,   
                          int baseAgility, 
                          int baseDefence, 
                          int baseResist){
            health = new Stat(baseHealth*2);
            attack = new Stat(baseAttack);
            magic = new Stat(baseMagic);
            agility = new Stat(baseAgility);
            defence = new Stat(baseDefence);
            resist = new Stat(baseResist);
        }
        public void LevelUp(int level){
            health.Improve(level);
            attack.Improve(level);
            magic.Improve(level);
            agility.Improve(level);
            defence.Improve(level);
            resist.Improve(level);
        }
        
        public struct Stat{
            public Stat(int bStat){
                scaling = bStat;
                value = scaling;
            }
            public void Improve(int level){
                value = scaling * level;
            }
            public int value;
            private int scaling;
            }
    }
    public enum StatType{
            Health,
            Attack,
            Magic,
            Agility,
            Defence,
            Resist
        }
}