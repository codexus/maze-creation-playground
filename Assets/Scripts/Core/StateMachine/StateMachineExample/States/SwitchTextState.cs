using TMPro;
using UnityEngine;

namespace Core.States.Example
{
    /// <summary>
    /// This state simply fills text with current time
    /// </summary>
    public class SwitchTextState : State<ExmapleState>, IUpdatable
    {
        TextMeshPro textComponent;
        public SwitchTextState(StateManager<ExmapleState> stateManager, ExmapleState state, TextMeshPro textComponent) : base(stateManager, state)
        {
            this.textComponent = textComponent;
        }

        public override void OnEnter()
        {
            textComponent.text = "Enter";
        }

        public void OnUpdate()
        {
            textComponent.text = Time.time.ToString();

            if (Time.time > 3f) stateManager.SwitchState(ExmapleState.Idle);
        }

        public override void OnExit()
        {
            textComponent.text = "Exit";
        }
    }
}

