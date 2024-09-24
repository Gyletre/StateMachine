namespace StateMachine
{
    public abstract class State
    {
        public abstract void Enter();
        public abstract void Tick(double delta);
        public abstract void Exit();
    }
}