using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLeft = 30.0f;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private float timeToTurnRed = 10f;
    public bool gameStarted = false;
    private float timeToDisplay;

    public Color warningColor;

    private bool turnedRed = false;

    // Update is called once per frame
    void Update()
    {
        if (gameStarted && timeLeft > 0){
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0, 100);
            timeToDisplay = Mathf.Ceil(timeLeft);
            if (timeToDisplay <= timeToTurnRed && !turnedRed){
                timeText.color = warningColor;
            }
            timeText.text = timeToDisplay.ToString();
        }

        if (timeLeft <= 0){
            TimeUp();
        }
    }

    public void SetTimer(float time){
        timeLeft = time;
    }

    public void StartTimer(){
        gameStarted = true;
    }

    //TODO notify gamemaster when time is up
    public void TimeUp(){
        // call gamemaster component and end game
        Debug.Log("Time is up");
        GameMaster.instance.PlayerLose();
    }

    public void IncreaseTime(float time){
        timeLeft += time;
    }
}
