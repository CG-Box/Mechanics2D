using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class СhronicPanel : TogglePanel
{
    [SerializeField] private TextMeshProUGUI text;

	[Header("Events Raise")]
	public NoteEventChannelSO addNoteRequest = default;

    List<Note> notes;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Toggle();
        }
    }

    public void Refresh()
    {
        string notesText = "";

        foreach(var note in notes)
        {
            notesText += $"<color=#990000>{note.Date.ToLongTimeString()}</color> {note.description} <br>";
        }
        text.text = notesText;
    }

    public void SetNotes(List<Note> notes)
    {
        this.notes = notes;
    }

    int i = 0;
    [ContextMenu("Add note")]
    void AddNote()
    {
        i++;
        var newNote = new Note(NoteType.Stat, "yea boy "+i);
        addNoteRequest.RaiseEvent(newNote);
    }

}
