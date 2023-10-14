using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinSeconds : MonoBehaviour
{
    public GameObject textGameObject;
    private GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        TMP_Text timeText = textGameObject.GetComponent<TMP_Text>();
        int roundTime = gameController.roundTime;
        timeText.text = "Time: "+roundTime+" seconds";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}