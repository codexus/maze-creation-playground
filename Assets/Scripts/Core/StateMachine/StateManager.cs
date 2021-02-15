using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.States
{
    public abstract class StateManager<U> : IStateManager<U> where U : Enum
    {
        private U initialState;

        private Dictionary<U, State<U>> states;

        protected State<U>[] statesArray;

        public event Action<U> OnStateChanged = delegate { };

        public State<U> PreviousState { get; private set; }

        public State<U> CurrentState { get; private set; }

        public State<U> NextState { get; private set; }

        public virtual void Initialize()
        {
            var appStates = statesArray;
            states = new Dictionary<U, State<U>>();

            foreach (var state in appStates)
            {
                RegisterState(state);
            }
            SwitchState(initialState);
            Debug.LogFormat("[StatesManager] Initialized with state ({0}).", initialState.ToString());
        }

        private void RegisterState(State<U> newState)
        {
            if (states.ContainsKey(newState.GetStateType()))
            {
                Debug.LogErrorFormat("[StatesManager] There is already ({0}) state type in dictionary.", newState.GetStateType().ToString());
                return;
            }

            states.Add(newState.GetStateType(), newState);
        }

        public void SwitchState(U stateType)
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

        protected void SetInitialState(U initialState)
        {
            this.initialState = initialState;
        }
    }
}
