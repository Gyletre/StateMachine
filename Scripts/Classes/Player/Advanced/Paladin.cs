using System.Collections.Generic;
using StateMachine;

namespace Classes{
    public class Paladin : PlayerAdvancedClass
    {
        public Paladin(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            prerequisites.AddRange(new[] {ClassList.Warrior,ClassList.Cleric});
            identity = ClassList.Paladin;
            this.stateMachine = stateMachine;
            CreateClass(new StatSpread(20,20,15,10,14,14)); //advanced stat spread must be 20, 20, 15, 14, 14, 10
        }
    }
}