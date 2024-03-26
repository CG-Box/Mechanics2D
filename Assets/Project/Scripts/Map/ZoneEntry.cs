using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum EntryID
{
    A1,
    A2,
    B1,
    B2,
    B3,
    C1,
    C2,
    D1
}

public class ZoneEntry : MapSpace
{
    [SerializeField] private Button entryButton;
    [SerializeField] private EntryID entryID;
    public EntryID EntryID { get { return entryID; } }

    [SerializeField] private Zone connectedZone;

    public Zone ConnectedZone { get { return connectedZone; } }

    public event EventHandler<ZoneEntry> OnSelectEntry;


    public void Select()
    {
        OnSelectEntry?.Invoke(this, this);
    }

    public override void Disable()
    {
        base.Disable();

        entryButton.interactable = false;
    }
    public override void Enable()
    {
        base.Enable();

        entryButton.interactable = true;
    }
}
