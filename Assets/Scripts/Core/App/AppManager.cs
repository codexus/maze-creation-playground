using Codexus.Maze;
using Core.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.States;

namespace Core
{
    /// <summary>
    /// Apps entry point. It sets up the app state machine and injects the dependencies into the the states.
    /// </summary>
    public class AppManager : MonoBehaviour
    {
        [SerializeField] private ViewManager viewManager;
        [SerializeField] private AppStates initialState = AppStates.Intro;
        [SerializeField] private MazeManager mazeManager;

        private AppStateMachine appStateMachine;
        private IUpdatable updatableState = null;

        private void Awake()
        {
            appStateMachine = new AppStateMachine();

            viewManager.Initialize();

            appStateMachine.Initialize(new State<AppStates>[]{
                new IntroState(appStateMachine, AppStates.Intro, viewManager),
                new GenerateState(appStateMachine, AppStates.Generate, viewManager, mazeManager),
            }, initialState);

            appStateMachine.OnStateChanged += OnStateChanged;
        }

        private void OnStateChanged(AppStates state)
        {
            updatableState = appStateMachine.CurrentState as IUpdatable;
        }

        void Update()
        {
            updatableState?.OnUpdate();
        }
    }

}
