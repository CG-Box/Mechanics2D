using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 movement;

    private bool haveControl = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // old input system
    
    /*
    void Update()
    {
        if(!haveControl)
            return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }*/

    // new input system
    void OnMove(InputValue inputValue)
	{
        if(!haveControl)
            return;
        movement = inputValue.Get<Vector2>();
	}

    private void FixedUpdate()
    {
        // Перемещение игрока
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void GainControl()
    {
        haveControl = true;
    }

    public void ReleaseControl(bool resetValues = true)
    {
        haveControl = false;
        if (resetValues)
        {
            movement.x = 0f;
            movement.y = 0f;
        }
    }
}