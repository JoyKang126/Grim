using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    //controls
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;
    public KeyCode interact;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    public GameObject player;
    public Animator anim;

    // Keep track of direction of most recent movement

    private NPC_Controller npc;

    Vector2 movement;  // Vector2 stores an X and Y
    // Update is called once per frame
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        switch(GameManager.Instance.gameMode)
        {
            case GameManager.GameMode.Gameplay:
                if (Input.GetKey(interact))
                {
                    //Interact();
                }
                
                if (Input.GetKey(left))
                {
                    movement.x = -1;
                    movement.y = 0;
                    player.transform.GetChild(0).gameObject.SetActive(true);
                    player.transform.GetChild(1).gameObject.SetActive(false);
                    player.transform.GetChild(2).gameObject.SetActive(false);
                    player.transform.GetChild(3).gameObject.SetActive(false);
                }
                else if (Input.GetKey(right))
                {
                    movement.x = 1;
                    movement.y = 0;
                    player.transform.GetChild(0).gameObject.SetActive(false);
                    player.transform.GetChild(1).gameObject.SetActive(true);
                    player.transform.GetChild(2).gameObject.SetActive(false);
                    player.transform.GetChild(3).gameObject.SetActive(false);
                }
                else
                {
                    movement.x = 0;
                }

                if (Input.GetKey(up))
                {
                    movement.y = 1;
                    movement.x = 0;
                    player.transform.GetChild(0).gameObject.SetActive(false);
                    player.transform.GetChild(1).gameObject.SetActive(false);
                    player.transform.GetChild(2).gameObject.SetActive(false);
                    player.transform.GetChild(3).gameObject.SetActive(true);
                }
                else if (Input.GetKey(down))
                {
                    movement.y = -1;
                    movement.x = 0;
                    player.transform.GetChild(0).gameObject.SetActive(false);
                    player.transform.GetChild(1).gameObject.SetActive(false);
                    player.transform.GetChild(2).gameObject.SetActive(true);
                    player.transform.GetChild(3).gameObject.SetActive(false);
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

                rb.MovePosition(rb.position + movement*moveSpeed*Time.fixedDeltaTime);
                break;

            case GameManager.GameMode.DrinkMaking:
                if(Input.GetKeyDown(KeyCode.Escape))
                {
                    GameObject.Find("Drink Station").transform.GetChild(0).gameObject.SetActive(false);
                    GameManager.Instance.gameMode = GameManager.GameMode.Gameplay;
                }
                break;
            case GameManager.GameMode.DialogueMoment:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    GameManager.Instance.ResumeTimeline();
                }
                break;
        }
    }

}
