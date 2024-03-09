using UnityEngine;
using System.Collections.Generic;

public class HealthBehaviour : MonoBehaviour //, ITakeFromFile
{
    public int Health { get { return health.value; } }

    [SerializeField] private SlotManager slotManager;

    [SerializeField] private Wrap<int> health;

    [Header("Events Raise")]
	[SerializeField] private IntEventChannelSO healthUpdateEvent = default;

    [SerializeField] private NoteEventChannelSO addNoteRequest = default;

    void Awake()
    {
        LoadData(slotManager.GetActiveSlot().data);
    }

    void Start()
    {
        healthUpdateEvent.RaiseEvent(health.value);
    }

    public void ChangeHealth(int amount)
    {
        health.value += amount;
        healthUpdateEvent.RaiseEvent(health.value);

        addNoteRequest.RaiseEvent(new Note(NoteType.Health, $"You get {amount} health"));
    }


    //ITakeFromFile
    public void LoadData(GameData data)
    {
        this.health = data.globals.health;
    }
}
