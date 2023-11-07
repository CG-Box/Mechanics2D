using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Vector3 cer = Vector3.down;
    }

    private void Update()
    {
        // Входные данные игрока
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        // Перемещение игрока
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}