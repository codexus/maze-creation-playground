using Core.States;

namespace Core
{
    public enum AppStates
    {
        Intro,
        Generate,
    }

    public class AppStateMachine : StateManager<AppStates>
    {
        internal void Initialize(State<AppStates>[] statesArray, AppStates initialState = AppStates.Intro)
        {
            this.statesArray = statesArray;
            SetInitialState(initialState);
            Initialize();
        }
    }
}
