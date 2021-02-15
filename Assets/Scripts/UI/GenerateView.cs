using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class GenerateView : View
    {
        [SerializeField] private Button generateButton;

        public Button GenerateButton => generateButton;
    }

}
