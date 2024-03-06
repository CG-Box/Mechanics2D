using UnityEngine;
using UnityEngine.UI;

public class TogglePanel : MonoBehaviour
{
    [SerializeField]private GameObject toggleGameObject;
        
    [Header("Default Selected Button")]
    [SerializeField]private Button defaultButton;

    public virtual void Show()
    {
        toggleGameObject.SetActive(true);
        SelectDefaultButton();
    }
    public virtual void Hide()
    {
        toggleGameObject.SetActive(false);
    }
    public void Toggle()
    {
        // if open
        if(toggleGameObject.activeSelf)
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
