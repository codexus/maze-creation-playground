using TMPro;
using UnityEngine;

namespace Core.States.Example
{
    public class ExampleManager : MonoBehaviour
    {
        [SerializeField] TextMeshPro textComponent;

        private ExampleStateMachine stateMachine;
        private IUpdatable updatableState = null;

        void Awake()
        {
            stateMachine = new ExampleStateMachine();

            stateMachine.Initialize(new State<ExmapleState>[]{
                new IdleState(stateMachine, ExmapleState.Idle),
                new SwitchTextState(stateMachine, ExmapleState.SwitchText, textComponent),
            });

            stateMachine.OnStateChanged += OnStateChanged;
        }

        private void Start()
        {
            stateMachine.SwitchState(ExmapleState.SwitchText);
        }

        private void OnStateChanged(ExmapleState state)
        {
            updatableState = stateMachine.CurrentState as IUpdatable;
        }

        void Update()
        {
            updatableState?.OnUpdate();
        }
    }

}
