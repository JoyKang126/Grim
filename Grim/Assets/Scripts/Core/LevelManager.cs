using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string levelName;
    //public Customer liv; based on Grant's class. Need's a bool function to check if
    //customer received drink. For Liv specifically, she'll give a dialogue that will
    //allow customer to choose to continue with her story. Choosing "Yes" will move into
    //the next scene.

    // Start is called before the first frame update
    void OnEnable()
    {
        // Only specifying the sceneName or sceneBuildIndex will load the Scene with the Single mode
        SceneManager.LoadScene(levelName);
    }
}
