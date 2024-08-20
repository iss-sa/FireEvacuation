using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour
{
    [SerializeField] private ObjectInteractUI objectInteractUI;
    [SerializeField] private GameObject doorClosedVisual;
    [SerializeField] private GameObject doorOpenVisual;
    private float rationalityScore;
    AudioManager audioManager;
    EndScreenStatistics statistics;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        statistics = GameObject.FindGameObjectWithTag("Score").GetComponent<EndScreenStatistics>();
    }

    public void Interact(int state)
    {
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (state == 1)
            {
                Debug.Log("Interact Door 1: Open --> Get rid of visual, toggle open_door visual");
                audioManager.PlaySFX(audioManager.door);
                ShowOpenDoor();
                HideClosedDoor();
                // Rationality Score +0
                statistics.rationalityScore += CalculateRationality("Open", state);
            } 
            else if (state == 2)
            {
                Debug.Log("Interact Door 1: Close --> Get rid of visual");
                audioManager.PlaySFX(audioManager.door);
                ShowClosedDoor();
                HideOpenDoor();
                statistics.rationalityScore += CalculateRationality("Close", state);
            }
        } 
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Interact Door 2: Ignore");
            objectInteractUI.Hide();
            // Rationality Score +0
            statistics.rationalityScore += CalculateRationality("Nothing", state);
        }     
    }

    private float CalculateRationality(string choice = "", int doorState = 0)
    {
        if (choice == "Nothing" && doorState == 2) // 2 is door Open
        {
            rationalityScore = -2f;
        }
        else if (choice == "Open")
        {
            rationalityScore = -1f;
        }
        else if (choice == "Close")
        {
            rationalityScore = 2f;
        }

        return rationalityScore;
    }

    private void ShowOpenDoor()
    {
        doorOpenVisual.SetActive(true);
    }
    private void HideClosedDoor()
    {
        doorClosedVisual.SetActive(false);
    }
    private void ShowClosedDoor()
    {
        doorClosedVisual.SetActive(true);
    }
    private void HideOpenDoor()
    {
        doorOpenVisual.SetActive(false);
    }
}
