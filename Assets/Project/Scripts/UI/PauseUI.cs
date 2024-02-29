using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField]private GameObject panelGameObject;

    [Header("Default Selected Button")]
    [SerializeField]private Button defaultButton;

	[Header("Events Raise")]
	[SerializeField]private BoolEventChannelSO pauseOpenedEvent = default; 

    public void Show()
    {
        panelGameObject.SetActive(true);
        SelectDefaultButton();
        pauseOpenedEvent.RaiseEvent(true);
    }
    public void Hide()
    {
        panelGameObject.SetActive(false);
        pauseOpenedEvent.RaiseEvent(false);
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
