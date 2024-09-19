using System.Collections.Generic;
using StateMachine;

namespace Classes{
    public abstract class PlayerAdvancedClass : PlayerBaseClass{
        public List<ClassList> prerequisites;
        public PlayerAdvancedClass(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        

    }
}