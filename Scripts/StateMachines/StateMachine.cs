using System.Runtime.CompilerServices;
using Godot;

namespace Game.StateMachine
{
    public abstract partial class StateMachine : CharacterBody2D
    {
        State currentState;
        public void SwitchState(State newState)
        {
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
        public override void _PhysicsProcess(double delta)
        {
            currentState?.Tick(delta);
        }
    }
}