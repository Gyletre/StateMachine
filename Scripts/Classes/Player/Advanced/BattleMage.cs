using System.Collections.Generic;
using StateMachine;

namespace Classes{
    public class BattleMage : PlayerAdvancedClass
    {
        public BattleMage(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            prerequisites.AddRange(new[] {ClassList.Warrior,ClassList.Mage});
            identity = ClassList.BattleMage;
            this.stateMachine = stateMachine;
            CreateClass(new StatSpread(10,20,20,15,14,14)); //advanced stat spread must be 20, 20, 15, 14, 14, 10
        }
    }
}