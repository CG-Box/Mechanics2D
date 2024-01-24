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

        gameManager.RemoveBindings();
        gameManager.AddBindings();
        itemsLibrary.UpdateStaticLibrary();

        /*
        if(!Constants.GameManagerReady)
        {
            Constants.GameManagerReady = true;
            gameManager.AddBindings();
            itemsLibrary.UpdateStaticLibrary();
            Debug.Log("AddBindings, UpdateStaticLibrary");
        }
        else
        {
            Debug.Log("Menu second time");
        }*/

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
