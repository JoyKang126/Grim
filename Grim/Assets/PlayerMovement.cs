using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;

    Vector2 movement;  // Vector2 stores an X and Y

    // Update is called once per frame
    void Update()
    {
        // User Input

        // Value between -1 (left arrow) to 1 (right arrow)
        movement.x = Input.GetAxisRaw("Horizontal");

        // Value between -1 (up arrow) to 1 (down arrow)
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate()
    {
        // Execute Movement

        // Handles movement + collision
        // note: rb.position is a Vector2 coordinate
        // Multiply by Time.fixedDeltaTime to maintain consistency
        rb.MovePosition(rb.position + movement*moveSpeed*Time.fixedDeltaTime);
    }
}
