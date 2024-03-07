using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class NoteSortEventArgs : EventArgs
{
    public NoteType type;
    public NoteSortEventArgs(NoteType type)
    {
        this.type = type;
    }
}

public class СhronicPanel : TogglePanel
{
    [SerializeField]private TextMeshProUGUI text;
    [Header("Items")]
    [SerializeField]private GameObject itemsGameObject;
    [SerializeField]private GameObject itemTemplateGO;
    [SerializeField]private GameObject itemEmptyGO;
    [SerializeField]private GameObject buttonsContainer;

    [Header("UI Settings")]
    [SerializeField]private float refreshRate = 0.5f;

    List<Note> notes;
    NoteType lastSortType = NoteType.All;
    Coroutine refreshCoroutine;

    public event EventHandler<NoteSortEventArgs> OnTypeSelect;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Toggle();
        }
    }

    void OnEnable()
    {
        BindButtons();
    }
    void OnDisable()
    {
        UnbindButtons();
    }

    void BindButtons()
    {
        int i = 0;
        foreach(Transform child in buttonsContainer.transform)
        {
            Button typeButton = child.gameObject.GetComponent<Button>();
            string typeName = ((NoteType)i).ToString();
            typeButton.GetComponentInChildren<TextMeshProUGUI>().text = typeName;
            typeButton.onClick.AddListener(()=>{
                var type = (NoteType)System.Enum.Parse(typeof(NoteType), typeName);
                TypeButtonClick_Handler(typeButton, type);
            });
            i++;
        }

        //Select correct type button if needed
        if(lastSortType != NoteType.All)
        {
            buttonsContainer.transform.GetChild((int)lastSortType).gameObject.GetComponent<Button>().Select();
        }
    }
    void UnbindButtons()
    {
        foreach(Transform child in buttonsContainer.transform)
        {
            child.gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }

    void TypeButtonClick_Handler(Button typeButton, NoteType type)
    {
        if(type == lastSortType)
        {
            lastSortType = NoteType.All;
            type = NoteType.All;

            Utility.Deselect();
        }
        else
        {
            lastSortType = type;
        }
        OnTypeSelect?.Invoke(typeButton, new NoteSortEventArgs(type));
    }

    public void Refresh()
    {
        //remove old except emptyText and template (0 and 1)
        while (itemsGameObject.transform.childCount > 2) {
            DestroyImmediate(itemsGameObject.transform.GetChild(2).gameObject);
        }


        TextMeshProUGUI noteText = null;

        //spawn new
        foreach(var note in notes)
        {
            noteText = Instantiate(itemTemplateGO, itemsGameObject.transform).GetComponent<TextMeshProUGUI>();
            noteText.gameObject.SetActive(true);
            noteText.text = $"<color=#990000>{note.Date.ToLongTimeString()}</color> {note.description} <br>";
        }
        itemTemplateGO.SetActive(false);

        //there aren't any notes show emptyText or hide they exist
        itemEmptyGO.SetActive(notes.Count == 0);
    }

    public void SetNotes(List<Note> notes)
    {
        this.notes = notes;
    }

    IEnumerator DelayedRefresh(float delay)
    {
        yield return new WaitForSeconds(delay);
        Refresh();
        //refreshCoroutine = null;
    }

    public void LazyRefresh()
    {
        if (refreshCoroutine != null)
        {
            StopCoroutine(refreshCoroutine);
        }
        refreshCoroutine = StartCoroutine(DelayedRefresh(refreshRate)); 
    }

}
