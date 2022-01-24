using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.Instance.gameMode == GameManager.GameMode.Gameplay)
        {
            if (collision.gameObject.tag == "DrinkStation")
            {
                if (Input.GetKey(KeyCode.X))
                {
                    GameObject.Find("Drink Station").transform.GetChild(0).gameObject.SetActive(true);
                    GameManager.Instance.gameMode = GameManager.GameMode.DrinkMaking;
                }
            
            }
            if (collision.gameObject.tag == "NPC")
            {
                if (Input.GetKey(KeyCode.X))
                {
                    //call NPC's comparedrink function rather than the following function
                    collision.gameObject.GetComponent<NPC_Controller>().ActivateDialogue();
                }
            }
        }
        

    }
}
