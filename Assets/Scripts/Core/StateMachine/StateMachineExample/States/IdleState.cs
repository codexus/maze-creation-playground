namespace Core.States.Example
{
    public class IdleState : State<ExmapleState>
    {
        public IdleState(StateManager<ExmapleState> stateManager, ExmapleState state) : base(stateManager, state){}
    }
}
