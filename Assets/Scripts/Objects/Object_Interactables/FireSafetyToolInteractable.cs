using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSafetyToolInteractable : MonoBehaviour
{
    [SerializeField] private ObjectInteractUI objectInteractUI;
    [SerializeField] private GameObject fireSafetyToolVisual;
    private float rationalityScore;
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Interact Fire Safety Tool 1: Use --> +30sec in Game (and remove fire in radius = X)");
            audioManager.PlaySFX(audioManager.fireSafetyTools);
            HideVisual();
            if (!timer.safe)
            {
                timer.remainingTime += 30f; 
            } 
            // get rid of fire
            // Rationality Score +3
            statistics.rationalityScore += CalculateRationality("Use");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Interact Fire Safety Tool: Ignore");
            objectInteractUI.Hide();
            // Rationality Score -3
            statistics.rationalityScore += CalculateRationality("Ignore");
        }
    }

    private void HideVisual()
    {
        fireSafetyToolVisual.SetActive(false);
    }

    private float CalculateRationality(string choice = "")
    {
        if (choice == "Use") 
        {
            rationalityScore = 3f;
        }
        if (choice == "Ignore") 
        {
            rationalityScore = -3f;
        }
        return rationalityScore;
    }
}
