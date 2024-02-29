using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField]private GameObject panelGameObject;

    [Header("Default Selected Button")]
    [SerializeField]private Button defaultButton;


    public void Show()
    {
        panelGameObject.SetActive(true);
        SelectDefaultButton();
    }
    public void Hide()
    {
        panelGameObject.SetActive(false);
    }
    public void TogglePanel()
    {
        // if open
        if(panelGameObject.activeSelf)
        {
            Hide();
        }
        else // if close
        {
            Show();
        }
    }

    public void SelectDefaultButton()
    {
        if(defaultButton != null)
            defaultButton.Select();
    }
}
