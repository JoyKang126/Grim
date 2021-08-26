using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

[Serializable]
public class DialogueBehaviour : PlayableBehaviour
{
    //public string characterName;
   	public string input;
    public Color textColor;
    public Font textFont;
    public float delay;
    public float delayBetweenLines;
	public AudioClip sound;

    //public Sprite characterSprite;
    //public Image imageHolder;

	public bool hasToPause = false;

	private bool clipPlayed = false;
	private bool pauseScheduled = false;
	private PlayableDirector director;

	private GameObject line;
	private GameObject holder;
	private DialogueSystem.DialogueBaseClass dialogueclass;
	

	public override void OnPlayableCreate(Playable playable)
	{
		director = (playable.GetGraph().GetResolver() as PlayableDirector);
		line = GameObject.FindGameObjectWithTag("line1");
		holder = GameObject.FindGameObjectWithTag("dialogueholder");
	}

	public override void ProcessFrame(Playable playable, FrameData info, object playerData)
	{
		if(!clipPlayed
			&& info.weight > 0f)
		{
			//UIManager.Instance.SetDialogue(characterName, dialogueLine, dialogueSize);
			//Text textholder = line.GetComponent<Text>();
			//dialogueclass.WriteText(input, textholder, textColor, textFont, delay, sound, delayBetweenLines);
			line.GetComponent<DialogueSystem.DialogueLine>().setContent(input, textColor, textFont,sound);
			holder.gameObject.SetActive(true);

			if(Application.isPlaying)
			{
				if(hasToPause)
				{
					pauseScheduled = true;
				}
			}

			clipPlayed = true;
		}
	}

	public override void OnBehaviourPause(Playable playable, FrameData info)
	{
		if(pauseScheduled)
		{
			pauseScheduled = false;
			//GameManager.Instance.PauseTimeline(director);
			director.playableGraph.GetRootPlayable(0).SetSpeed(0d);
		}
		clipPlayed = false;
		if (Input.GetKeyDown(KeyCode.Space)) {
			director.playableGraph.GetRootPlayable(0).SetSpeed(1d);
		}
	}
}