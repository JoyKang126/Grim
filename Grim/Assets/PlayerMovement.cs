using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;

    // Keep track of direction of most recent movement
    private int isMovingX = 32;
    private int isMovingY = 32;
    private NPC_Controller npc;

    Vector2 movement;  // Vector2 stores an X and Y

    // Update is called once per frame
    void Update()
    {
        // User Input

        // Value between -1 (left arrow) to 1 (right arrow)
        if (!inDialogue())
        {
            movement.x = Input.GetAxisRaw("Horizontal");

            // Value between -1 (up arrow) to 1 (down arrow)
            movement.y = Input.GetAxisRaw("Vertical");

            if (movement != Vector2.zero)
            {
                // Only move in one direction (not diagonally)
                if (isMovingX > 0)
                {
                    if (isMovingX >= 32 && Mathf.Abs(movement.y) > 0.5f)
                    {
                        movement.x = 0;
                        isMovingX = 0;
                        isMovingY = 1;
                    }
                    else
                    {
                        movement.y = 0;
                        isMovingX++;
                    }
                }
                else if (isMovingY > 0)
                {
                    if (isMovingY >= 32 && Mathf.Abs(movement.x) > 0.5f)
                    {
                        movement.y = 0;
                        isMovingX = 1;
                        isMovingY = 0;
                    }
                    else
                    {
                        movement.x = 0;
                        isMovingY++;
                    }
                }

                anim.SetFloat("moveX", movement.x);
                anim.SetFloat("moveY", movement.y);
                anim.SetBool("moving", true);
            }
            else
            {
                anim.SetBool("moving", false);
            }
        }

    }

    void FixedUpdate()
    {
        // Execute Movement

        // Handles movement + collision
        // note: rb.position is a Vector2 coordinate
        // Multiply by Time.fixedDeltaTime to maintain consistency
        rb.MovePosition(rb.position + movement*moveSpeed*Time.fixedDeltaTime);
    }

    private bool inDialogue()
    {
        if(npc != null)
            return npc.DialogueActive();
        else
            return false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            npc = collision.gameObject.GetComponent<NPC_Controller>();
            if(Input.GetKey(KeyCode.E))
                npc.ActivateDialogue();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
    }
}
