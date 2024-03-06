using System.Collections.Generic;
using UnityEngine;
using System;

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

    void OnEnable()
    {
        AddPanelListeners();
    }
    void OnDisable()
    {
        RemovePanelListeners();
    }

    void AddPanelListeners()
    {
        chronicPanel.OnTypeSelect += Chronicles_OnTypeSelect;
    }
    void RemovePanelListeners()
    {
        chronicPanel.OnTypeSelect -= Chronicles_OnTypeSelect;
    }

    void Chronicles_OnTypeSelect(object sender, NoteSortEventArgs eventArgs)
    {
        if(eventArgs.type == NoteType.All)
        {
            UpdateUI();//show all notes
        }
        else
        {
            ShowNotes(eventArgs.type);
        }
    }

    public void Add(Note record)
    {
        notes.Add(record);
        UpdateUI();
    }

    public List<Note> GetNotesByType(NoteType targetType)
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

    public void ShowNotes(NoteType targetType)
    {
        chronicPanel.SetNotes(GetNotesByType(targetType));
        chronicPanel.Refresh();
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
