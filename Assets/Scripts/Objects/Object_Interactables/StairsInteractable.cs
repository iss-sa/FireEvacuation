using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsInteractable : MonoBehaviour
{
    [SerializeField] private ObjectInteractUI objectInteractUI;
    private float rationalityScore;
    LevelLoader levelLoader;
    AudioManager audioManager;
    Timer timer;
    EndScreenStatistics statistics;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        timer = GameObject.FindGameObjectWithTag("Time").GetComponent<Timer>();
        statistics = GameObject.FindGameObjectWithTag("Score").GetComponent<EndScreenStatistics>();
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Interact Stairs 1: Go Down Stairs - Scene Change");
            audioManager.PlaySFX(audioManager.stairsSteps);
            // Rationality Score +2
            statistics.rationalityScore += CalculateRationality("Stairs");
            if (levelLoader.CurrentSceneNumber() == 3)
            {
                timer.remainingTime = 0;
                timer.safe = true;
                levelLoader.LoadEndScreen();
            } else {
                levelLoader.LoadNextLevel();
            }
            
        } else if (Input.GetKeyDown(KeyCode.E)){
            Debug.Log("Interact Stairs 2: Take Elevator");
            audioManager.PlaySFX(audioManager.elevator);
            // Rationality Score -2
            statistics.rationalityScore += CalculateRationality("Elevator");
            if (levelLoader.CurrentSceneNumber() == 3)
            {
                timer.remainingTime = 0;
                timer.safe = true;
                levelLoader.LoadEndScreen();
            } else {
                levelLoader.LoadNextLevel();
            }
            
        }
    }

    private float CalculateRationality(string choice = "")
    {
        if (choice == "Stairs") // 2 is door Open
        {
            rationalityScore = 2f;
        }
        else if (choice == "Elevator")
        {
            rationalityScore = -2f;
        }

        return rationalityScore;
    }
}
