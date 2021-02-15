using System;

namespace Core.States
{
    public abstract class State<T> where T : Enum
    {
        protected T stateType;
        protected IStateManager<T> stateManager;

        public State(IStateManager<T> stateManager, T stateType)
        {
            this.stateType = stateType;
            this.stateManager = stateManager;
        }

        public T GetStateType() => stateType;
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
        public virtual void OnResume() { }
        public bool IsTypeOf(T stateType)
        {
            return GetStateType().Equals(stateType);
        }
    }
}
