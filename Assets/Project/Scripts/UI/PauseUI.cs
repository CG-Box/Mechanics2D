using UnityEngine;

public class PauseUI : TogglePanel
{
	[Header("Events Raise")]
	[SerializeField]private BoolEventChannelSO pauseOpenedEvent = default; 

    public override void Show()
    {
        base.Show();
        pauseOpenedEvent.RaiseEvent(true);
    }
    public override void Hide()
    {
        base.Hide();
        pauseOpenedEvent.RaiseEvent(false);
    }
}
