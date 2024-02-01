using UnityEngine;
using UnityEngine.UI;

namespace Mechanics2D
{
    public class StartUI : MonoBehaviour 
    {

        [Header("Slot manager")]
        [SerializeField] private SlotManager slotManager;

        [Header("Menu Buttons")]
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueGameButton;
        [SerializeField] private Button loadGameButton;

        void Start() 
        {
            DisableButtonsDependingOnData();
            SelectDefaultButton();
        }

        public void DisableButtonsDependingOnData() 
        {
            if (!slotManager.HasFilesData()) 
            {
                continueGameButton.interactable = false;
                loadGameButton.interactable = false;
            }
            else
            {
                continueGameButton.interactable = true;
                loadGameButton.interactable = true;
            }
        }
        public void SelectDefaultButton()
        {
            if(continueGameButton.interactable == true)
            {
                continueGameButton.Select();
            }
            else
            {
                newGameButton.Select();
            }
        }
    }
}
