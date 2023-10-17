using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseHandler : MonoBehaviour
{
    bool GameIsPaused = false;
    public GameObject menuUI;
    public Tween tw;
    // Start is called before the first frame update
    void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }

        }
        

    }

    public void Pause()
    {
        Time.timeScale = 0f;
        menuUI.SetActive(true);
        tw.ButtonPop();
        GameIsPaused = true;

    }


    public void Resume()
    {
        Time.timeScale = 1f;
        menuUI.SetActive(false);
        GameIsPaused = false;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
