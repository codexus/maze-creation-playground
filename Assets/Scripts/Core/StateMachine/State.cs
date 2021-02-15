using System;

namespace Core.States
{
    public abstract class State<U> where U : Enum
    {
        protected U stateType;
        protected IStateManager<U> stateManager;

        public State(IStateManager<U> stateManager, U stateType)
        {
            this.stateType = stateType;
            this.stateManager = stateManager;
        }

        public U GetStateType() => stateType;
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnResume() { }
        public bool IsTypeOf(U stateType)
        {
            return GetStateType().Equals(stateType);
        }
    }
}
