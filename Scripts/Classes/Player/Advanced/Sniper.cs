using System.Collections.Generic;
using StateMachine;

namespace Classes{
    public class Sniper : PlayerAdvancedClass
    {
        public Sniper(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            prerequisites.AddRange(new[] {ClassList.Warrior,ClassList.Archer});
            identity = ClassList.Sniper;
            this.stateMachine = stateMachine;
            CreateClass(new StatSpread(15,20,10,20,14,14)); //advanced stat spread must be 20, 20, 15, 14, 14, 10
        }
    }
}