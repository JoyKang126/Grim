using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceScript : MonoBehaviour
{
    [SerializeField] Text promptText;
    [SerializeField] Text bubbleText;
    [SerializeField] List<Text> optionTexts;
    [SerializeField] Image sendButton;
    [SerializeField] Color highlight;

    const int NUMLINES = 9;
    bool finishedChoosing = true;
    bool buttonClicked = true;

    int totalScore;
    int currentLine;    // ranges 0-8 corresponding to which line player is choosing for
    int currentOption;  // ranges 0-2 which corresponds to option
    List<int> optionScores;    // holds the score for options 0, 1, 2

    List<string> Prompts = new List<string>() 
    {
        "[Your choice] Anna, ",
        "this is Liv[Your choice] ",
        "Sorry to message you so late. I wanted to say I'm sorry [Your choice] back then. ",
        "I was [Your choice]. ",
        "You [Your choice], ",
        "and I [Your choice]. ",
        "I [Your choice], ",
        "but I just wanted to let you know that [Your choice]. ",
        "[Your choice]. "
    };

    List<string> PositiveOpts = new List<string>() 
    { 
        "Hey",
        ".",
        "for being so horrible to you",
        "really immature",
        "didn't deserve to be treated that way",
        "wish our friendship could’ve gone differently",
        "don't expect you to forgive me",
        "I am sorry",
        "Have a good night"  
    };

    List<string> NeutralOpts = new List<string>() 
    {
        "Hello",
        ", your best friend from elementary school.",
        "if I hurt your feelings",
        "not as nice as I really am",
        "were kind of mean to me too but I'm willing to overlook it",
        "hope you're not STILL mad at me",
        "hope you'll forgive me",
        "I’m trying to be the bigger person and apologize",
        "See you at school"
    };

    List<string> NegativeOpts = new List<string>() 
    {
        "Wassup",
        ", your ex-bff.",
        "for being a dick",
        "an asshole",
        "were kind of a wimp and I took advantage of that",
        "would love to be buddies again",
        "hope we’re cool now",
        "there’s no pressure, it’s all chill",
        "Catch you later"
    };

    List<string> Responses = new List<string>()
    {
        "Maybe I was too apologetic??",
        "Maybe I was being too defensive??",
        "Maybe I was trying too hard to seem casual."
    };

    // Start is called before the first frame update
    void Start()
    {
        totalScore = 0;
        currentLine = 0;
        optionScores = new List<int> { 1, 0, -1 };

        SetText(currentLine);
        bubbleText.text = "";
        sendButton.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (currentLine < NUMLINES) 
        {
            HandleOptionSelection();
        }
        else if (finishedChoosing)
        {   // execute this once
            for (int k = 0; k < 3; k++) {
                optionTexts[k].enabled = false;
            }
            promptText.text = "Press Enter to send!";
            sendButton.enabled = true;
            finishedChoosing = false;
        }
        else if (!finishedChoosing && buttonClicked)
        {   // execute this once only after finishedChoosing code was already executed
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(HandleButton());
                buttonClicked = false;
            }
        }
    }

    void SetText(int selectedLine)
    {
        optionTexts[0].text = PositiveOpts[selectedLine];
        optionTexts[1].text = NeutralOpts[selectedLine];
        optionTexts[2].text = NegativeOpts[selectedLine];

        promptText.text = Prompts[selectedLine];
    }

    void HandleOptionSelection() 
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if (currentOption > 0)
                --currentOption;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            if (currentOption < 2)
                ++currentOption;
        }
        UpdateOptionSelection(currentOption);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            string choice = optionTexts[currentOption].text;
            string str = promptText.text;
            string line = str.Replace("[Your choice]", choice);

            bubbleText.text += line;
            totalScore += optionScores[currentOption];

            ++currentLine;
            if (currentLine < NUMLINES)
                SetText(currentLine);
        }
    }

    void UpdateOptionSelection(int selectedOption) 
    {
        for (int k = 0; k < optionTexts.Count; k++) 
        {
            if (k == selectedOption)
                optionTexts[k].color = highlight;
            else
                optionTexts[k].color = Color.black;
        }
    }

    IEnumerator HandleButton()
    {
        sendButton.enabled = false;

        string GrimText = "Grim: What happened after that?";
        string LivText = "Liv: … she never ended up responding. ";

        if (totalScore > 3)
            LivText += Responses[0];
        else if (totalScore < -3)
            LivText += Responses[1];
        else
            LivText += Responses[2];

        promptText.text = GrimText;
        yield return new WaitForSeconds(2f);
        promptText.text = LivText;
    }
}
