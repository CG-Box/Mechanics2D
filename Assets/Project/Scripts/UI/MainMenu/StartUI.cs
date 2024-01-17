using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Mechanics2D
{
    public class StartUI : MonoBehaviour {

        [Header("Menu Buttons")]
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button continueGameButton;
        [SerializeField] private Button loadGameButton;

        [Header("Slot manager")]
        [SerializeField] private SlotManager slotManager;

        void Start() 
        {
            DisableButtonsDependingOnData();
        }

        void DisableButtonsDependingOnData() 
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


        public void NewGame() 
        {
            slotManager.NewGame();
        }

        public void ContinueGame() 
        {}

        public void Quit()
        {
            #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            #else       
            Application.Quit();
            #endif
        }
    }
}
