using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ConfirmationPopup : MenuPanel
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cancelButton;

    void OnEnable()
    {
        ShowAnimation();
        SelectDefaultButton();
    }

    #region  Animation
    protected override void ShowAnimation()
    {
        base.ShowAnimation();

        innerTransform.localPosition = new Vector2(0, -Screen.height);
        innerTransform.LeanMoveLocalY(0, animationSpeed).setEaseInOutExpo().delay = 0.1f;
    }

    public override void HideAnimation()
    {
        base.HideAnimation();

        innerTransform.LeanMoveLocalY(-Screen.height, animationSpeed).setEaseInOutExpo().setOnComplete(DeactivateMenu);
    }
    #endregion

    public void ActivateMenu(string displayText, UnityAction confirmAction, UnityAction cancelAction)
    {
        this.gameObject.SetActive(true);

        // set the display text
        this.displayText.text = displayText;

        // remove any existing listeners just to make sure there aren't any previous ones hanging around
        // note - this only removes listeners added through code
        confirmButton.onClick.RemoveAllListeners();
        cancelButton.onClick.RemoveAllListeners();

        // assign the onClick listeners
        confirmButton.onClick.AddListener(() => {
            confirmAction();
            HideAnimation();
        });
        cancelButton.onClick.AddListener(() => {
            cancelAction();
            HideAnimation();
        });
    }
}