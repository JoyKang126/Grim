using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DialogueSystem
{
    public class DialogueLine : DialogueBaseClass
    {
        private TextMeshProUGUI textHolder;

        [Header ("Text Options")]
        [SerializeField] private string input;
        [SerializeField] private Color textColor;
        [SerializeField] private TMP_FontAsset textFont;

        [Header ("Time Parameters")]
        [SerializeField] private float delay;
        [SerializeField] private float delayBetweenLines;

        [Header ("Sound")]
        [SerializeField] private AudioClip sound;

        [Header ("Character Image")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;

        [Header ("Character Name")]
        [SerializeField] private string characterName;
        [SerializeField] private TextMeshProUGUI nameHolder;



        private IEnumerator lineAppear;
        private void Awake()
        {
            textHolder = GetComponent<TextMeshProUGUI>();
            textHolder.text = "";
            imageHolder.sprite = characterSprite;
            imageHolder.preserveAspect = true;
            nameHolder.text = characterName;
            
        }
        private void OnEnable()
        {
            ResetLine();
            nameHolder.text = characterName;
            lineAppear = WriteText(input, textHolder, textColor, textFont, delay, sound, delayBetweenLines);
            StartCoroutine(lineAppear);
            Debug.Log(input);
        }

        private void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                if(textHolder.text != input)
                {
                    StopCoroutine(lineAppear);
                    textHolder.text = input;
                }
                else
                    finished = true;
            }
        }

        private void ResetLine()
        {
            //textHolder = GetComponentInParent<Text>();
            textHolder.SetText("");
            finished = false;
        }

        public void setContent(string minput, Color mtextColor, TMP_FontAsset mtextFont, AudioClip msound)
        {
            input = minput;
            textColor = mtextColor;
            textFont = mtextFont;
            sound = msound;
            
        }

        public void SetLines(string minput, string mname)
        {
            input = minput;
            characterName = mname;
        }
    }
}

