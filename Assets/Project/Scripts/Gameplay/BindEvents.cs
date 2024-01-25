using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindEvents : MonoBehaviour
{
    [SerializeField]private bool workAsReferences = true;
    [SerializeField]private GameManagerSO gameManager;
    [SerializeField]private SlotManager slotManager;
    [SerializeField]private ItemsLibrary itemsLibrary;
    [SerializeField]private SpeakersLibrary speakersLibrary;

    void UpdateLibraries()
    {
        itemsLibrary.UpdateStaticLibrary();
        speakersLibrary.UpdateStaticLibrary();
    }

    void OnEnable()
    {
        //Del later or rewrite as singleton
        UpdateLibraries();

        if(workAsReferences) return;

        slotManager.PrepareSlots();
        gameManager.RemoveBindings();
        gameManager.AddBindings();
        UpdateLibraries();

        /*
        if(!Constants.GameManagerReady)
        {
            Constants.GameManagerReady = true;
            gameManager.AddBindings();
            UpdateLibraries();
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
