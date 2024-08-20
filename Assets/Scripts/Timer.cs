using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    public float remainingTime = 240; //4min 
    public bool safe = false;
    private int currentTime;

    public static Timer instance;
    LevelLoader levelLoader;
    private void Awake()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        // if there are two Audio Managers, destroy one, keep other (only one needed)
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // keep current audio manager throughout all scenes
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        TimerColor();

        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            TriggerEndScreen(currentTime);
        } 
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        currentTime = (int)remainingTime;
        
    }
    private void TriggerEndScreen(int number)
    {
        if (levelLoader.CurrentSceneNumber() != 4)
        {
            levelLoader.LoadEndScreen();
        }
    }

    public void TimerColor()
    {
        if (safe)
        {
            timerText.color = Color.green;
        } else {
            timerText.color = Color.red;
            }
    }

    public int GetCurrentTime()
    {
        return currentTime;
    }
}
