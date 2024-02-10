using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePanel : MonoBehaviour
{
    [Header("Elements to animate")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Transform innerTransform;
    [SerializeField] float animationSpeed = 0.3f;

    void OnEnable()
    {
        //ShowAnimation();
    }

    public void ShowAnimation()
    {
        canvasGroup.alpha = 0;
        canvasGroup.LeanAlpha(to : 1, time: animationSpeed);

        //innerTransform.localPosition = new Vector2(0, -Screen.height);
        //innerTransform.LeanMoveLocalY(0, animationSpeed).setEaseInOutExpo().delay = 0.1f;
    }
    public void HideAnimation()
    {
        canvasGroup.LeanAlpha(0, time: animationSpeed).setOnComplete(DeactivateMenu);

        //innerTransform.LeanMoveLocalY(-Screen.height, animationSpeed).setEaseInOutExpo().setOnComplete(DeactivateMenu);
    }

    void DeactivateMenu() 
    {
        this.gameObject.SetActive(false);
    }
}
