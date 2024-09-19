using StateMachine;

namespace Classes{
    public class Mage : PlayerBaseClass{
        public Mage(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            identity = ClassList.Mage;
            this.stateMachine = stateMachine;
            CreateClass(new StatSpread(7,5,15,12,10,10)); //base class needs stats of 15, 12, 10, 10, 7, 5
        }
        

    }
}