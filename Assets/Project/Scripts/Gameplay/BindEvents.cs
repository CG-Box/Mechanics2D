using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindEvents : MonoBehaviour
{
    [SerializeField]private bool workAsReferences = true;
    [SerializeField]private GameManagerSO gameManager;
    [SerializeField]private SlotManager slotManager;
    [SerializeField]private ItemsLibrary itemsLibrary;

    void OnEnable()
    {
        if(workAsReferences) return;

        slotManager.PrepareSlots();
        gameManager.AddBindings();
        itemsLibrary.UpdateStaticLibrary();
    }
    void OnDisable()
    {
        if(workAsReferences) return;

        //gameManager.RemoveBindings();
    }

    void OnApplicationQuit()
    {
        gameManager.RemoveBindings();
    }
}
