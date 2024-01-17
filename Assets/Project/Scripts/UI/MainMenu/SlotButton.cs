using UnityEngine;
using UnityEngine.UI;
using System;

namespace Mechanics2D
{
    [RequireComponent(typeof(Button))]
    public class SlotButton : MonoBehaviour 
    {
        [SerializeField]
        private GameData_SO connectedSlot;

        public event EventHandler<SlotButtonEventArgs> OnSelectSlot;

        Button thisButton;

        #region EventsBinding
        void OnEnable()
        {
            thisButton = GetComponent<Button>();
            thisButton.onClick.AddListener(SlotButtonClick);
        }
        void OnDisable()
        {
            thisButton.onClick.RemoveListener(SlotButtonClick);
        }
        #endregion

        public void SelectSlot(bool invokeEvent = true)
        {
            if(invokeEvent)
                OnSelectSlot?.Invoke(this, new SlotButtonEventArgs(connectedSlot.SlotId));
        }
        public void SlotButtonClick()
        {
            SelectSlot();
        }
    }

    public class SlotButtonEventArgs : EventArgs
    {
        public string slotId;
        public SlotButtonEventArgs(string slotId)
        {
            this.slotId = slotId;
        }
    }
}
