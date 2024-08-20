using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireInteract : MonoBehaviour
{
    Timer timer;
    EndScreenStatistics statistics;

    private void Awake()
    {
        timer = GameObject.FindGameObjectWithTag("Time").GetComponent<Timer>();
        statistics = GameObject.FindGameObjectWithTag("Score").GetComponent<EndScreenStatistics>();
    }
    
    public void Interact()
    {
        timer.remainingTime -= 60f;
        timer.safe = false;
        statistics.rationalityScore -= 2;
    }
}
