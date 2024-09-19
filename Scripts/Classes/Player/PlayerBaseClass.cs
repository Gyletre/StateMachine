using StateMachine;

namespace Classes{
    public class PlayerBaseClass : Class{
        protected PlayerStateMachine stateMachine;
        public PlayerBaseClass(PlayerStateMachine stateMachine){
            this.stateMachine = stateMachine;
            CreateClass(new StatSpread(10,10,10,10,10,10));
        }
    }
}