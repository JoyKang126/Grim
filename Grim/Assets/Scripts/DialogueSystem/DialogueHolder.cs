using System.Collections;
using UnityEngine;
namespace DialogueSystem
{
    public class DialogueHolder : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(dialogueSequence());
        }
        private IEnumerator dialogueSequence()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Deactivate();
                Debug.Log(transform.childCount);
                transform.GetChild(i).gameObject.SetActive(true);
                yield return new WaitUntil(()=>transform.GetChild(i).GetComponent<DialogueLine>().finished);
            }
            gameObject.SetActive(false);
        }

        private void Deactivate()
        {
            for (int i=0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}

