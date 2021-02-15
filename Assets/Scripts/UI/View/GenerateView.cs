using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Core.UI
{
    public class GenerateView : View
    {
        [SerializeField] private Button generateButton;
        [SerializeField] private TMP_Dropdown generationDropdown;

        public Button GenerateButton => generateButton;
        public TMP_Dropdown GenerationDropdown => generationDropdown;
    }

}
