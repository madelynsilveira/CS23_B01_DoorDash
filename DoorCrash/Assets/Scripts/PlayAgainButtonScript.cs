using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAgainButtonScript : MonoBehaviour
{
    // Specify the name of the scene you want to load
    public string sceneToLoad = "TaskGame";

    public void LoadTaskScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
