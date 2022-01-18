using UnityEngine;

public class NPC_Controller : MonoBehaviour
{
    [SerializeField] private GameObject dialogue;
    //have multiple GameObjects to represent diff dialogues

    //comparedrink fuction that calls a certain activateDialogue
    //function
    public void ActivateDialogue()
    {
        dialogue.SetActive(true);
    }

    //multiple dialogue functions (ActivateBadDialogue, ActiveGoodDialogue, etc)
    public bool DialogueActive()
    {
        return dialogue.activeInHierarchy;
    }
}