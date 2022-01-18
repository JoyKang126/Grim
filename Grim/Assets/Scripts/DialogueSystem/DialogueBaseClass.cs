using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace DialogueSystem
{
    public class DialogueBaseClass : MonoBehaviour
    {
        public bool finished {get; protected set;}
        // Start is called before the first frame update
        public IEnumerator WriteText(string input, TextMeshProUGUI textHolder, Color textColor, TMP_FontAsset textFont, float delay, AudioClip sound, float delayBetweenLines)
        {
            textHolder.color = textColor;
            textHolder.font = textFont;
            for (int i=0; i<input.Length; i++)
            {
                textHolder.SetText(textHolder.text+ input[i]);
                SoundManager.instance.PlaySound(sound);
                yield return new WaitForSeconds(delay);
            }
            //yield return new WaitForSeconds(delayBetweenLines);
            yield return new WaitUntil(()=>Input.GetKeyDown("space"));
            finished = true;
        }
    }
}