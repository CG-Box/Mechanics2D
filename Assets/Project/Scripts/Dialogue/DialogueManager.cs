using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;
using System;

//KeyCode.

public class DialogueManager : MonoBehaviour, ITakeFromFile
{
    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.03f;

    [Header("Load Globals JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;
    
    [Header("Speakers Library")]
    [SerializeField] private SpeakersLibrary speakersLibrary;

    [Header("External Functions")]
    [SerializeField] private InkExternalFunctions inkExternalFunctions;

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator layoutAnimator;

    [SerializeField] private Image portraitImage;
    [SerializeField] private Sprite defaultPortraitSprite;
    const string defaultDisplayNameText = "???";

    [Header("Choices UI")]

    [SerializeField] private GameObject choicesPanel;
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;

    [SerializeField] 
    private bool canQuit = false;

    [Header("Events Raise")]
    public VoidEventChannelSO dialogueStartEvent = default;

    [Header("Events Listen")]
    public TextAssetEventChannelSO enterDialogueEvent = default;

    private Story currentStory;

    public Story CurrentStory
    {
        get {return currentStory;}
    }

    public bool dialogueIsPlaying { get; private set; }

    private bool canContinueToNextLine = false;

    private Coroutine displayLineCoroutine;

    private static DialogueManager instance;

    private const string SPEAKER_TAG = "speaker";
    private const string EMOTION_TAG = "emotion";
    private const string DISPLAY_NAME_TAG = "name";
    private const string LAYOUT_TAG = "layout";
    
    private const string SOUND_TAG = "sound";

    public enum Layout
    {
        Left,
        Right
    }

    Speaker currentSpeaker = null;

    private DialogueVariables dialogueVariables;

    private GameData.DialogueData dialogueData;

    void Awake() 
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene");
        }
        instance = this;

        dialogueData = new GameData.DialogueData();
        InitVariables(dialogueData.jsonState);
    }

    void InitVariables(string jsonState)
    {
        dialogueVariables = new DialogueVariables(loadGlobalsJSON, jsonState);
        //inkExternalFunctions = new InkExternalFunctions(); //used only when using simple class, not scriptableObject
    }


    #region EventsBinding
    void OnEnable()
    {
        AddBindings();
    }
    void OnDisable()
    {
        RemoveBindings();
        RemoveChoiceListeners();
    }

    public void AddBindings()
    {
		if (enterDialogueEvent != null)
			enterDialogueEvent.OnEventRaised += EnterDialogueMode;
    }
    public void RemoveBindings()
    {
		if (enterDialogueEvent != null)
			enterDialogueEvent.OnEventRaised -= EnterDialogueMode;
    }
    #endregion

    public static DialogueManager GetInstance() 
    {
        return instance;
    }

    void Start() 
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        choicesPanel.SetActive(false);

        // get all of the choices text 
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices) 
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
        AddChoiceListeners();
    }

    void Update() 
    {
        // return right away if dialogue isn't playing
        if (!dialogueIsPlaying) 
        {
            return;
        }

        //catch quit button and exit
        if(canQuit && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ExitDialogueMode());
        }
        

        // handle continuing to the next line in the dialogue when submit is pressed
        // NOTE: The 'currentStory.currentChoiecs.Count == 0' part was to fix a bug after the Youtube video was made
        if (canContinueToNextLine 
            && currentStory.currentChoices.Count == 0 
            && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }

        //Show full line
        //Need to add and check for some flag to work properly
        /*if(Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("End writing");
            dialogueText.maxVisibleCharacters = dialogueText.text.Length;
        }*/
    }

    public void EnterDialogueMode(TextAsset inkJSON) 
    {
        FreezePlayer();

        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        dialogueVariables.StartListening(currentStory);
        inkExternalFunctions.Bind(currentStory);

        // reset portrait, layout, and speaker
        ResetSpeaker();

        layoutAnimator.Play(nameof(Layout.Left));

        ContinueStory();

        dialogueStartEvent.RaiseEvent();
    }


    void FreezePlayer()
    {
        PlayerMovement playerInput = FindObjectOfType<PlayerMovement>();
        playerInput.ReleaseControl(true);
    }
    void UnfreezePlayer()
    {
        PlayerMovement playerInput = FindObjectOfType<PlayerMovement>();
        playerInput.GainControl();
    }

    IEnumerator ExitDialogueMode() 
    {
        yield return new WaitForSeconds(0.2f);

        dialogueData.jsonState = dialogueVariables.GetVariables();
        dialogueVariables.StopListening(currentStory);
        inkExternalFunctions.Unbind(currentStory);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";

        UnfreezePlayer();
    }

    void ContinueStory() 
    {
        if (currentStory.canContinue) 
        {
            // set text for the current dialogue line
            if (displayLineCoroutine != null) 
            {
                StopCoroutine(displayLineCoroutine);
            }

            string newLine = currentStory.Continue();

            // handle tags
            HandleTags(currentStory.currentTags);

            if(newLine.Length == 0 || newLine == "\n"|| newLine == " " || newLine == "") // check for empty string/symbols
            {
                if(currentStory.canContinue)
                {
                    newLine = currentStory.Continue(); //take next line if current is empty
                }
                else
                {
                    StartCoroutine(ExitDialogueMode()); //stop dialogue if there's nothing left
                }
            }

            displayLineCoroutine = StartCoroutine(DisplayLine(newLine));
        }
        else 
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    IEnumerator DisplayLine(string line) 
    {
        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;

        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;

        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // if the submit button is pressed, finish up displaying the line right away
            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                dialogueText.maxVisibleCharacters = line.Length;
                break;
            }

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag) 
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            else 
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices();

        canContinueToNextLine = true;
    }

    void HideChoices() 
    {
        foreach (GameObject choiceButton in choices) 
        {
            choiceButton.SetActive(false);
        }
        choicesPanel.SetActive(false);
    }

    void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags) 
        {
            // parse the tag
            string[] splitTag = tag.Split(':');
            if (splitTag.Length != 2) 
            {
                Debug.LogError("Tag could not be appropriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();
            
            // handle the tag
            switch (tagKey) 
            {
                case SPEAKER_TAG:
                    UpdateSpeaker(tagValue);
                    break;
                case EMOTION_TAG:
                    UpdateEmotion(tagValue);
                    //portraitAnimator.Play(tagValue);
                    break;
                case DISPLAY_NAME_TAG:
                    displayNameText.text = tagValue;
                    break;
                case LAYOUT_TAG:
                    layoutAnimator.Play(tagValue);
                    break;
                case SOUND_TAG:
                    //Debug.Log("play sound name: "+tagValue);
                    //AudioManager.Play(tagValue);
                    break;
                default:
                    Debug.LogWarning("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    void ResetSpeaker()
    {
        currentSpeaker = null;
        displayNameText.text = defaultDisplayNameText;
        portraitImage.sprite = defaultPortraitSprite;
    }
    void UpdateSpeaker(string tagValue)
    {
        Speaker speaker = GetSpeaker(tagValue);

        if(!speaker.person.CompareWithString(tagValue))
        {
            Debug.LogWarning($"wrong speaker variable in dialogue tag: {tagValue}, was converted to {speaker.person}");
        }

        portraitImage.sprite = speaker.GetEmotion(Speaker.Emotion.Normal);
        displayNameText.text = speaker.displayName;

        currentSpeaker = speaker;
    }
    void UpdateEmotion(string tagValue)
    {
        Speaker.Emotion emotionType;
        System.Enum.TryParse(tagValue, out emotionType);

        if(!emotionType.CompareWithString(tagValue))
        {
            Debug.LogWarning($"wrong speaker variable in dialogue tag: {tagValue}, was converted to {emotionType}");
        }

        portraitImage.sprite =  currentSpeaker.GetEmotion(emotionType);
    }
    Speaker GetSpeaker(string tagValue)
    {
        Speaker.Person personType;
        System.Enum.TryParse(tagValue, out personType);
        return speakersLibrary.GetItem(personType);
    }


    void DisplayChoices() 
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        int index = 0;
        // enable and initialize the choices up to the amount of choices for this line of dialogue
        foreach(Choice choice in currentChoices) 
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        //At least one choice available
        if(index != 0)
        {
            choicesPanel.SetActive(true);
        }

        // go through the remaining choices the UI supports and make sure they're hidden
        for (int i = index; i < choices.Length; i++) 
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    void AddChoiceListeners()
    {
        for(int i=0; i<choices.Length; i++)
        {
            int choiceNumber = i; //fix unexpected behav with closure
            choices[i].gameObject.GetComponent<Button>().onClick.AddListener(()=>{
                MakeChoice(choiceNumber);
            });
        }
    }
    void RemoveChoiceListeners()
    {
        for(int i=0; i<choices.Length; i++)
        {
            Button choiceButton = choices[i].gameObject.GetComponent<Button>();
            choiceButton.onClick.RemoveAllListeners();
        }
    }

    IEnumerator SelectFirstChoice() 
    {
        // Event System requires we clear it first, then wait
        // for at least one frame before we set the current selected object.
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine) 
        {
            currentStory.ChooseChoiceIndex(choiceIndex);

            // NOTE: The below two lines were added to fix a bug after the Youtube video was made
            //InputManager.GetInstance().RegisterSubmitPressed(); // this is specific to my InputManager script

            ContinueStory();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName) 
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null) 
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    //TO DO: work only when dialogue is playing fix this before using
    public void SetVariableState<T>(string variableName, T variableValue)
    {
        if(dialogueVariables.variables.ContainsKey(variableName))
        {
            Debug.Log($"value {variableName} exist = success");
            currentStory.variablesState[variableName] = variableValue;
            //dialogueVariables.GetGlobalStory().variablesState[variableName] = variableValue; //fail
        }
        else
        {
            Debug.Log($"value {variableName} wasnt found in the dialogueVariables");
        }
    }

    //ITakeFromFile
    public void LoadData(GameData data)
    {
        dialogueData = data.dialogueData;
        dialogueVariables.UpdateVariables(dialogueData.jsonState);
    }
}