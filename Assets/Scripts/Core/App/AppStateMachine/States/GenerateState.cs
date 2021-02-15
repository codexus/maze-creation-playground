namespace Core
{
    using Codexus.Maze;
    using States;
    using UI;

    public class GenerateState : State<AppStates>
    {
        private MazeManager mazeManager;
        private ViewManager viewManager;
        private GenerateView generateView;

        public GenerateState(StateManager<AppStates> stateManager, AppStates state, ViewManager viewManager, MazeManager mazeManager) : base(stateManager, state) 
        {
            this.viewManager = viewManager;
            this.mazeManager = mazeManager;
            generateView = viewManager.GetView<GenerateView>();
        }

        public override void OnEnter()
        {
            viewManager.SwitchView<GenerateView>();
            generateView.GenerateButton.onClick.AddListener(GenerateButtonHandler);
        }

        public override void OnExit()
        {
            generateView.GenerateButton.onClick.RemoveListener(GenerateButtonHandler);
        }

        private void GenerateButtonHandler()
        {
            mazeManager.RegenerateMaze();
        }
    }
}
