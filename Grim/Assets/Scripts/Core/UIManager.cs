  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public GameObject charNameText, dialogueLineText;
	public GameObject dialoguePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetDialogue(string charName, string lineOfDialogue, int sizeOfDialogue)
	{
		//charNameText.SetText(charName);
		dialogueLineText.GetComponent<DialogueSystem.DialogueLine>().SetLines(lineOfDialogue, charName);
		//dialogueLineText.fontSize = sizeOfDialogue;

		ToggleDialoguePanel(true);
	}

    public void ToggleDialoguePanel(bool active)
	{
		Debug.Log("activate dialogue!");
		dialoguePanel.SetActive(active);
	}
}
