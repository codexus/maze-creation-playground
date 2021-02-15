using System;

namespace Core.States
{
    public interface IStateManager<U>
    {
        event Action<U> OnStateChanged;
        void SwitchState(U stateType);
        void SwitchToPrevious();
    }
}
