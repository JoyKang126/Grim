using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //controls
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator anim;

    // Keep track of direction of most recent movement

    private NPC_Controller npc;

    Vector2 movement;  // Vector2 stores an X and Y

    // Update is called once per frame
    void Update()
    {
        // User Input

        // Value between -1 (left arrow) to 1 (right arrow)
        if (!inDialogue())
        {
            if (Input.GetKey(left))
            {
                movement.x = -1;
                movement.y = 0;
            }
            else if (Input.GetKey(right))
            {
                movement.x = 1;
                movement.y = 0;
            }
            else
            {
                movement.x = 0;
            }


            if (Input.GetKey(up))
            {
                movement.y = 1;
                movement.x = 0;
            }

            else if (Input.GetKey(down))
            {
                movement.y = -1;
                movement.x = 0;
            }
            else
            {
                movement.y = 0;
            }

           if (movement != Vector2.zero)
            {
                anim.SetFloat("Horizontal", movement.x);
                anim.SetFloat("Vertical", movement.y);
            }
            anim.SetFloat("Speed", movement.sqrMagnitude);
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
