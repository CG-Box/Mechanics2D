using UnityEngine;

public class PauseUI : MonoBehaviour
{
    [SerializeField]private GameObject panelGameObject;

    [Header("Events Raise")]
    [SerializeField]private ControlDataEventChannelSO inputControlChannel;

    public void Show()
    {
        inputControlChannel.RaiseEvent(new ControlData(false));

        panelGameObject.SetActive(true);
    }
    public void Hide()
    {
        panelGameObject.SetActive(false);
        inputControlChannel.RaiseEvent(new ControlData(true));
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
}
