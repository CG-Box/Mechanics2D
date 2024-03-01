using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace Mechanics2D
{
    [RequireComponent(typeof(Collider2D))]
    public class SaveTest : MonoBehaviour, IDataPersister
    {
        public enum TriggerType
        {
            Once, Everytime,
        }

        [Tooltip("This is the gameobject which will trigger the director to play.  For example, the player.")]
        public GameObject triggeringGameObject;
        public TriggerType triggerType;

        //[HideInInspector]
        public DataSettings dataSettings;

        protected bool m_AlreadyTriggered;

        void ChangeColor()
        {
            SpriteRenderer m_SpriteRenderer = GetComponent<SpriteRenderer>();
            m_SpriteRenderer.color = Color.blue;
        }

        void OnEnable()
        {
            PersistentDataManager.RegisterPersister(this);
        }

        void OnDisable()
        {
            PersistentDataManager.UnregisterPersister(this);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            
            if (other.tag != "Player")
                return;

            /*
            if (other.gameObject != triggeringGameObject)
                return;*/

            if (triggerType == TriggerType.Once && m_AlreadyTriggered)
                return;

            ChangeColor();
            m_AlreadyTriggered = true;
        }

        public void OverrideAlreadyTriggered(bool alreadyTriggered)
        {
            m_AlreadyTriggered = alreadyTriggered;
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;

            //Debug.Log(dataSettings.dataTag);
        }

        public Data SaveData()
        {
            //Debug.Log($"SaveData {m_AlreadyTriggered}");
            return new Data<bool>(m_AlreadyTriggered);
        }

        public void LoadData(Data data)
        {
          
            Data<bool> directorTriggerData = (Data<bool>)data;
            m_AlreadyTriggered = directorTriggerData.value;

            //Debug.Log($"LoadData {directorTriggerData.value}");
            //Debug.Log(dataSettings.dataTag);

            if(m_AlreadyTriggered)
                ChangeColor();
        }
    }
}