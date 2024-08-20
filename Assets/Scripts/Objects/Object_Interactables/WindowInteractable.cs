using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowInteractable : MonoBehaviour
{
    [SerializeField] private ObjectInteractUI objectInteractUI = null;
    
    [SerializeField] private GameObject windowIntactVisual;
    [SerializeField] private GameObject windowBrokenVisual;
    private int lastScene;
    private float rationalityScore;
    AudioManager audioManager;
    LevelLoader levelLoader;
    Timer timer;
    EndScreenStatistics statistics;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        timer = GameObject.FindGameObjectWithTag("Time").GetComponent<Timer>();
        statistics = GameObject.FindGameObjectWithTag("Score").GetComponent<EndScreenStatistics>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }
    public void Interact(int state)
    {
        if (state == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interact window 1: Break - Change Visual");
                // Rationality Score
                statistics.rationalityScore += CalculateRationality("Break");
                ShowBrokenWindow();
                HideIntactWindow();
                audioManager.PlaySFX(audioManager.windowBreak);
                 
            } else if (Input.GetKeyDown(KeyCode.R)){
                Debug.Log("Interact window 2: Do Nothing");
                // Rationality Score +0
                statistics.rationalityScore += CalculateRationality("Nothing");
                objectInteractUI.Hide();
            }
        } 
        else if (state == 2)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Interact window 1: Shout for Help");
                // Rationality Score
                statistics.rationalityScore += CalculateRationality("Shout");
                audioManager.PlaySFX(audioManager.fireShout);
                 
            } 
            else if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Interact window 2: Jump out");
                // Rationality Score
                lastScene = levelLoader.CurrentSceneNumber();
                statistics.rationalityScore += CalculateRationality("Jump", lastScene);
                if (lastScene == 2)
                {
                    timer.remainingTime = 0;
                    timer.safe = false;
                }
                else if (lastScene == 3)
                {
                    timer.remainingTime = 0;
                    timer.safe = true;
                }  
                levelLoader.LoadEndScreen();               
            }
        }        
    }

    private void ShowBrokenWindow()
    {
        windowBrokenVisual.SetActive(true);
    }

    private void HideIntactWindow()
    {
        windowIntactVisual.SetActive(false);
    }

    private float CalculateRationality(string choice = "", int lastScene = 0)
    {
        if (choice == "Break")
        {
            if (timer.remainingTime >= 200)
            {
                rationalityScore = -1;
            } else {
                rationalityScore = 1;
            }
        }
        else if (choice == "Nothing")
        {
            if (timer.remainingTime >= 100)
            {
                rationalityScore = 0;
            } else {
                rationalityScore = -1;
            }            
        }
        else if (choice == "Jump")
        {
            if (lastScene == 0)
            {
                if (timer.remainingTime >= 100)
                {
                    rationalityScore = -5;
                } else {
                    rationalityScore = -1;
                }
            } else if (lastScene == 1){
                rationalityScore = 3;
            }
        }
        else if (choice == "Shout")
        {
            if (lastScene == 0)
            {
                rationalityScore = 3;
            } else {
                rationalityScore = 1;
            }
        }

        return rationalityScore;
    }
}
