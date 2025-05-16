using System.Runtime.CompilerServices;
using Godot;

namespace StateMachine
{
    public abstract partial class StateMachine : CharacterBody2D
    {
namespace StateMachine
    {
        public abstract partial class StateMachine : CharacterBody2D
        {
            State currentState;
            public float slowrate = 1f;
            public void SwitchState(State newState)
            {
                currentState?.Exit();
                currentState = newState;
                currentState?.Enter();
            }
            public override void _PhysicsProcess(double delta)
            {
                currentState?.Tick(delta * slowrate);
            }
        }
    }