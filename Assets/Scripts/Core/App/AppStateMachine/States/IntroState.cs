namespace Core
{
    using UI;
    using States;
    using System;

    public class IntroState : State<AppStates>
    {
        private ViewManager viewManager;
        private IntroView introView;
        public IntroState(StateManager<AppStates> stateManager, AppStates state, ViewManager viewManager) : base(stateManager, state) 
        {
            this.viewManager = viewManager;
            introView = viewManager.GetView<IntroView>();
        }

        public override void OnEnter()
        {
            viewManager.SwitchView<IntroView>();
            introView.GoToMazeButton.onClick.AddListener(OnGoToMazeClickHandler);
        }

        // Will be invoked after stateManager.SwitchState will be called.
        public override void OnExit()
        {
            introView.GoToMazeButton.onClick.RemoveListener(OnGoToMazeClickHandler);
        }

        private void OnGoToMazeClickHandler()
        {
            stateManager.SwitchState(AppStates.Generate);
        }
    }

}
