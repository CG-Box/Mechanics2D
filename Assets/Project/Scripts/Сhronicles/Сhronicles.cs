using System.Collections.Generic;
using UnityEngine;

public class Сhronicles : MonoBehaviour
{
    [SerializeField] private СhronicPanel chronicPanel;
    [SerializeField] private SlotManager slotManager;

    List<Note> notes;

    void Awake()
    {
        notes = new List<Note>();
        LoadData(slotManager.GetActiveSlot().data);
        UpdateUI();
    }

    public void Add(Note record)
    {
        notes.Add(record);
        chronicPanel.Refresh();
    }

    public List<Note> GetRecordsByType(NoteType targetType)
    {
        var targetNotes = new List<Note>();
        foreach(var note in notes)
        {
            if(note.type == targetType)
            {
                targetNotes.Add(note);
            }
        }
        return targetNotes;
    }

    public void UpdateUI()
    {
        chronicPanel.SetNotes(notes);
        chronicPanel.Refresh();
    }


    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.notes = data.globals.notes;
    }
}
