using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.States
{
    public abstract class StateManager<T> : IStateManager<T> where T : Enum
    {
        private T initialState;

        private Dictionary<T, State<T>> states;

        protected State<T>[] statesArray;

        public event Action<T> OnStateChanged = delegate { };

        public State<T> PreviousState { get; private set; }

        public State<T> CurrentState { get; private set; }

        public State<T> NextState { get; private set; }

        public virtual void Initialize()
        {
            var appStates = statesArray;
            states = new Dictionary<T, State<T>>();

            foreach (var state in appStates)
            {
                RegisterState(state);
            }
            SwitchState(initialState);
            Debug.LogFormat("[StatesManager] Initialized with state ({0}).", initialState.ToString());
        }

        private void RegisterState(State<T> newState)
        {
            if (states.ContainsKey(newState.GetStateType()))
            {
                Debug.LogErrorFormat("[StatesManager] There is already ({0}) state type in dictionary.", newState.GetStateType().ToString());
                return;
            }

            states.Add(newState.GetStateType(), newState);
        }

        public void SwitchState(T stateType)
        {
            NextState = states[stateType];

            if (CurrentState != null)
            {
                if (CurrentState.IsTypeOf(stateType))
                {
                    Debug.LogFormat("[StateManager] State {0} is already running", stateType);
                    CurrentState.OnResume();
                    return;
                }
                CurrentState.OnExit();
            }

            if (!states.ContainsKey(stateType))
            {
                Debug.LogErrorFormat("[StatesManager] There is no ({0}) state in dictionary.", stateType.ToString());
                return;
            }

            PreviousState = CurrentState;
            CurrentState = states[stateType];

            CurrentState.OnEnter();
            OnStateChanged(CurrentState.GetStateType());
        }

        public void SwitchToPrevious()
        {
            if (PreviousState == null) return;
            SwitchState(PreviousState.GetStateType());
        }

        protected void SetInitialState(T initialState)
        {
            this.initialState = initialState;
        }
    }
}
