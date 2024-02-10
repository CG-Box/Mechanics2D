using UnityEngine;

public class ChoicePanel : MenuPanel
{
    void OnEnable()
    {
        ShowAnimation();
    }

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
}
