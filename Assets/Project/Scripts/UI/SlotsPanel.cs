using UnityEngine;

namespace Mechanics2D
{
    public class SlotsPanel : MonoBehaviour 
    {

        [SerializeField]
        private SlotManager slotManager;

        [SerializeField]
        private SlotButton[] slotButtonsList;

        void OnEnable()
        {
            InitializeSlotButtons();
        }
        void OnDisable()
        {
            ClearSlotButtons();
        }

        void InitializeSlotButtons()
        {
            foreach(SlotButton slotButton in slotButtonsList)
            {
                AddSlotButtonListeners(slotButton);
            }
        }
        void ClearSlotButtons()
        {
            foreach(SlotButton slotButton in slotButtonsList)
            {
                RemoveSlotButtonListeners(slotButton);
            }
        }

        private void AddSlotButtonListeners(SlotButton slotButton)
        {
            slotButton.OnSelectSlot += SlotsPanel_OnSelectSlot;
        }
        private void RemoveSlotButtonListeners(SlotButton slotButton)
        {

            slotButton.OnSelectSlot -= SlotsPanel_OnSelectSlot;
        }

        private void SlotsPanel_OnSelectSlot(object sender, SlotButtonEventArgs slotEventArgs)
        {
            slotManager.SetActiveSlot(slotEventArgs.slotId);
        }

    }
}
