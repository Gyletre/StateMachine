using StateMachine;

namespace Classes{
    public class Warrior : PlayerBaseClass{
        public Warrior(PlayerStateMachine stateMachine) : base(stateMachine)
        {
            identity = ClassList.Warrior;
            this.stateMachine = stateMachine;
            CreateClass(new StatSpread(10,15,5,10,12,7)); //base class needs stats of 15, 12, 10, 10, 7, 5
        }
        

    }
}