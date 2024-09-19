using StateMachine;

namespace Classes{
    public class Cleric : PlayerBaseClass{
        public Cleric(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            identity = ClassList.Cleric;
            this.stateMachine = stateMachine;
            CreateClass(new StatSpread(10,5,12,7,10,15)); //base class needs stats of 15, 12, 10, 10, 7, 5
        }
        

    }
}