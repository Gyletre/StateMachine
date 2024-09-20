using System.Runtime.CompilerServices;
using Godot;

namespace StateMachine{
    public abstract partial class StateMachine : CharacterBody2D{
        State currentState;
        public void SwitchState(State newState){
            currentState?.Exit();
            currentState = newState;
            currentState?.Enter();
        }
        public override void _Process(double delta){
            currentState?.Tick(delta);
        }
    }
}