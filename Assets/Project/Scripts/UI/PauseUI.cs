using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PauseUI : MonoBehaviour
{
    [SerializeField]private GameObject panelGameObject;

    public void Show()
    {
        FreezePlayer();
        panelGameObject.SetActive(true);
    }
    public void Hide()
    {
        panelGameObject.SetActive(false);
        UnfreezePlayer();
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

    void FreezePlayer()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.ReleaseControl(true);
    }
    void UnfreezePlayer()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.GainControl();
    }

    // new input system
    void OnEsc(InputValue inputValue)
	{
        TogglePanel();
	}
}
