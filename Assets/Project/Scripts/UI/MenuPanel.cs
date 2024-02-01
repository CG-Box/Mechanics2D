using UnityEngine;
using UnityEngine.UI;

//Base class with simple open/close animation and defaultButtonSelection for SlotsPanel, SettingsUI and ConfirmationPopup

public class MenuPanel : MonoBehaviour
{
    [Header("Elements to animate")]
    [SerializeField] protected Transform innerTransform;
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected float animationSpeed = 0.3f;

    [Header("Default Selected Button")]
    [SerializeField] protected Button defaultButton;

    protected virtual void ShowAnimation()
    {
        canvasGroup.alpha = 0;
        canvasGroup.LeanAlpha(to : 1, time: animationSpeed);
    }
    public virtual void HideAnimation()
    {
        canvasGroup.LeanAlpha(0, time: animationSpeed).setOnComplete(DeactivateMenu);
    }

    protected virtual void DeactivateMenu() 
    {
        this.gameObject.SetActive(false);
    }

    public void SelectDefaultButton()
    {
        if(defaultButton != null)
            defaultButton.Select();
    }
}
