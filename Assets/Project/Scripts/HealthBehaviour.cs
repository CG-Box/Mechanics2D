using UnityEngine;
using System.Collections.Generic;

public class HealthBehaviour : MonoBehaviour
{
    public int MyHealth = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        
        Circle collectable = collider.GetComponent<Circle>();
        if(collectable != null)
        {
            ChangeHealth(collectable.health);
            //collectable.SetActive(false);
        }
    }

    public void ChangeHealth(int delta)
    {
        MyHealth += delta;
    }
}
