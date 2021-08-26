using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteract : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DrinkStation")
        {
            if (Input.GetKey(KeyCode.X))
            {
                Debug.Log("x");
                GameObject.Find("Drink Station").transform.GetChild(0).gameObject.SetActive(true);
                GameManager.Instance.gameMode = GameManager.GameMode.DrinkMaking;
            }
            
        }

    }
}
