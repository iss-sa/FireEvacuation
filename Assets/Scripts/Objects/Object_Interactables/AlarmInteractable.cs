using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmInteractable : MonoBehaviour
{
    [SerializeField] private ObjectInteractUI objectInteractUI;
    [SerializeField] private GameObject alarmOldVisual;
    [SerializeField] private GameObject alarmActivatedVisual;
    private float rationalityScore;
    Timer timer;
    EndScreenStatistics statistics;
    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        timer = GameObject.FindGameObjectWithTag("Time").GetComponent<Timer>();
        statistics = GameObject.FindGameObjectWithTag("Score").GetComponent<EndScreenStatistics>();
    }
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Interact Fire Alarm 1: Activate --> Counter turns green");
            audioManager.PlaySFX(audioManager.fireAlarm);
            ShowActivatedAlarm();
            HideOldAlarm();
            timer.remainingTime -= 60f;
            timer.safe = true;
            // Rationality Score +5
            statistics.rationalityScore += CalculateRationality("Use");
        } 
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact Fire Alarm 2: Ignore");
            objectInteractUI.Hide();
            // Rationality Score -5
            statistics.rationalityScore += CalculateRationality("Ignore");
        }
    }
    private float CalculateRationality(string choice = "")
    {
        if (choice == "Use") 
        {
            rationalityScore = 5f;
        }
        if (choice == "Ignore") 
        {
            rationalityScore = -5f;
        }
        return rationalityScore;
    }

    private void ShowActivatedAlarm()
    {
        alarmActivatedVisual.SetActive(true);
    }

    private void HideOldAlarm()
    {
        alarmOldVisual.SetActive(false);
    }
}
