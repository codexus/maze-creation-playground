using UnityEngine;

namespace Core.States.Example
{
    public enum ExmapleState
    {
        Idle,
        SwitchText,
    }

    public class ExampleStateMachine : StateManager<ExmapleState>
    {
        internal void Initialize(State<ExmapleState>[] statesArray)
        {
            this.statesArray = statesArray;
            SetInitialState(ExmapleState.Idle);
            Initialize();
        }
    }
}