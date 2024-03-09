using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindEvents : MonoBehaviour
{
    [SerializeField]private bool workAsReferences = true;
    [SerializeField]private GameManagerSO gameManager;
    [SerializeField]private SlotManager slotManager;
    [SerializeField]private CanvasManager canvasManager;
    [SerializeField]private ItemsLibrary itemsLibrary;
    [SerializeField]private SpeakersLibrary speakersLibrary;
    [SerializeField]private EffectManager effectManager;

    void UpdateLibraries()
    {
        itemsLibrary.UpdateStaticLibrary();
        speakersLibrary.UpdateStaticLibrary();
    }

    void OnEnable()
    {
        //Del later or rewrite as singleton
        UpdateLibraries();
        EffectManager.Init(effectManager);

        //don't do it in main menu (it's optional, may be it's can be done only once in main menu, when game has build)
        if(workAsReferences)
        {
            canvasManager.RemoveBindings();
            canvasManager.AddBindings();
        }
        else //do it only in main menu
        {
            slotManager.PrepareSlots();
            gameManager.RemoveBindings();
            gameManager.AddBindings();
            UpdateLibraries();
        }
    }
    void OnDisable()
    {
        if(workAsReferences) return;

        //gameManager.RemoveBindings();
    }

    void OnApplicationQuit()
    {
        canvasManager.RemoveBindings();
        gameManager.RemoveBindings();
    }
}
