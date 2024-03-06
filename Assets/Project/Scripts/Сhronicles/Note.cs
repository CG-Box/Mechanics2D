using System;
using UnityEngine;

public enum NoteType
{
    System,
    Stat,
    Money,
    Inventory,
    Health,
    Quest,

    All, //for sort for sorting purpose
}

[Serializable]
public struct Note
{
    public NoteType type;
    public string description;
    [SerializeField] private long date;
    public DateTime Date
    {
        get { return  DateTime.FromBinary(date);}
        set { date = value.ToBinary(); }
    }

    public Note(NoteType type, string description)
    {
        this.type = type;
        this.description = description;
        this.date = DateTime.Now.ToBinary();
    }
}
