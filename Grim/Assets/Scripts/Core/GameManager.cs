using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : Singleton<GameManager>
{
    public GameMode gameMode = GameMode.Gameplay;
    private PlayableDirector activeDirector;
    // Start is called before the first frame update
    public void DrinkMakingGame()
    {
        gameMode = GameMode.DrinkMaking;
    }
    public void PauseTimeline(PlayableDirector whichOne)
	{
		activeDirector = whichOne;
		activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(0d);
		gameMode = GameMode.DialogueMoment; //InputManager will be waiting for a spacebar to resume
		//UIManager.Instance.TogglePressSpacebarMessage(true);
	}

	//Called by the InputManager
	public void ResumeTimeline()
	{
		//UIManager.Instance.TogglePressSpacebarMessage(false);
		//UIManager.Instance.ToggleDialoguePanel(false);
		activeDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
		gameMode = GameMode.Gameplay;
	}    
    public enum GameMode
	{
		Gameplay,
		DrinkMaking,
		DialogueMoment, //waiting for input
	}
}
