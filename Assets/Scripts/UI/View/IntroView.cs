using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class IntroView : View
    {
        [SerializeField] private Button goToMazeButton;

        public Button GoToMazeButton => goToMazeButton;
    }

}
