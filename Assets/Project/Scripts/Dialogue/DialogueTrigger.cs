using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;

    [Header("Events Raise")]
    public TextAssetEventChannelSO enterDialogueEvent = default;

    [Header("Dialogue")]
    [SerializeField] private DialogueText dialogueText;

    bool playerInRange;

    void Awake() 
    {
        playerInRange = false;
        visualCue.SetActive(false);
    }

    public void TryTalk()
	{
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying) 
        {
            //DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
            enterDialogueEvent.RaiseEvent(dialogueText.InkJSON);           
        }
	}

    void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == Constants.PlayerTag)
        {
            playerInRange = true;
            visualCue.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == Constants.PlayerTag)
        {
            playerInRange = false;
            visualCue.SetActive(false);
        }
    }
}