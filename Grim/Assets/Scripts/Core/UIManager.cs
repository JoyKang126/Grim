  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : Singleton<UIManager>
{
    public TextMeshProUGUI charNameText, dialogueLineText;
	public GameObject dialoguePanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetDialogue(string charName, string lineOfDialogue, int sizeOfDialogue)
	{
		charNameText.SetText(charName);
		dialogueLineText.SetText(lineOfDialogue);
		dialogueLineText.fontSize = sizeOfDialogue;

		ToggleDialoguePanel(true);
	}

    public void ToggleDialoguePanel(bool active)
	{
		dialoguePanel.SetActive(active);
	}
}
