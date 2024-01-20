using UnityEngine;
using System.Collections.Generic;

public class HealthBehaviour : MonoBehaviour, ITakeFromFile
{
    public int MyHealth = 0;

    [Header("Events Listen")]
    public IntEventChannelSO healthChangeRequest = default;

    [Header("Events Raise")]
	public IntEventChannelSO healthUpdateEvent = default;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        Circle collectable = collider.GetComponent<Circle>();
        if(collectable != null)
        {
            ChangeHealth(collectable.health);
            healthUpdateEvent.RaiseEvent(MyHealth);
            //collectable.SetActive(false);
        }
    }

    public void ChangeHealth(int delta)
    {
        MyHealth += delta;
    }

    public void LoadData(GameData data)
    {
        MyHealth = data.globals.playerHealth;
        Debug.Log($"Health loaded {MyHealth}");
    }
}
