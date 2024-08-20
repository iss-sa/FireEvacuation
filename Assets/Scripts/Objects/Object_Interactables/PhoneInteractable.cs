using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneInteractable : MonoBehaviour
{
    [SerializeField] private GameObject phoneIntactVisual;
    [SerializeField] private GameObject phoneBrokenVisual;
    Timer timer;
    AudioManager audioManager;
    EndScreenStatistics statistics;
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
            Debug.Log("Interact Phone 1: Call Help --> Counter extends 1min and turns green");
            audioManager.PlaySFX(audioManager.phoneCall);
            timer.safe = true;
            ShowBrokenPhone();
            HideIntactPhone();
            // Rationality Score +1
            statistics.rationalityScore += 1;
        } else if (Input.GetKeyDown(KeyCode.E)){
            Debug.Log("Interact Phone 2: Smash");
            audioManager.PlaySFX(audioManager.phoneBreak);
            ShowBrokenPhone();
            HideIntactPhone();
            // Rationality Score -2
            statistics.rationalityScore -= 2;
        }
    }

    private void ShowBrokenPhone()
    {
        phoneBrokenVisual.SetActive(true);
    }

    private void HideIntactPhone()
    {
        phoneIntactVisual.SetActive(false);
    }
}
