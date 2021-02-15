namespace Core
{
    using Codexus.Maze;
    using States;
    using System;
    using System.Collections.Generic;
    using UI;

    public class GenerateState : State<AppStates>
    {
        private MazeManager mazeManager;
        private ViewManager viewManager;
        private GenerateView generateView;

        private List<string> optionsCache = new List<string>();

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

            SetDropDown(generateView, mazeManager);
            
            // Set listener.
            generateView.GenerationDropdown.onValueChanged.AddListener(OnGenerationValueChangeHandler);
        }

        private void SetDropDown(GenerateView generateView, MazeManager mazeManager)
        {
            // Create dropdown options.
            generateView.GenerationDropdown.ClearOptions();
            optionsCache.Clear();

            MazeGenerator[] mazeGenerators = mazeManager.GetMazeGenerators();
            
            for (int i = 0; i < mazeGenerators.Length; i++)
            {
                optionsCache.Add(mazeGenerators[i].GenerationStrategyType.ToString());
            }

            generateView.GenerationDropdown.AddOptions(optionsCache);
            generateView.GenerationDropdown.value = mazeManager.GetCurrentGeneratorIndex();
        }

        public override void OnExit()
        {
            generateView.GenerateButton.onClick.RemoveListener(GenerateButtonHandler);
            generateView.GenerationDropdown.onValueChanged.RemoveListener(OnGenerationValueChangeHandler);
        }

        private void GenerateButtonHandler()
        {
            mazeManager.RegenerateMaze();
        }

        private void OnGenerationValueChangeHandler(int option)
        {
            mazeManager.SetGenerationMethod(option);
        }

    }
}
