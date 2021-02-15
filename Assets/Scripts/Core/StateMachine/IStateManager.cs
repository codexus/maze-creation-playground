using System;

namespace Core.States
{
    public interface IStateManager<T>
    {
        event Action<T> OnStateChanged;
        void SwitchState(T stateType);
        void SwitchToPrevious();
    }
}
