using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Events Raise")]
    public TextAssetEventChannelSO enterDialogueEvent = default;

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    bool playerInRange;

    void Awake() 
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    void OnUse(InputValue inputValue)
	{
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        {
            visualCue.SetActive(true);

            //DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            enterDialogueEvent.RaiseEvent(inkJSON);           
        }
        else 
        {
            visualCue.SetActive(false);
        }
	}

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == Constants.PlayerTag)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == Constants.PlayerTag)
        {
            playerInRange = false;
        }
    }
}