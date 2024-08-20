using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreenStatistics : MonoBehaviour
{
    Timer timer;
    public float rationalityScore = 0;
    private bool win = false;
    [SerializeField] private EndScreenUI endScreenUI;

    public static EndScreenStatistics instance;

    private void Awake()
    {
        timer = GameObject.FindGameObjectWithTag("Time").GetComponent<Timer>();

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
    
    // true if Win, false if Lose
    public bool WinOrLose()
    {
        // if end of timer and timer is green (safe) then you won
        if (timer.remainingTime == 0)
        {
            if (timer.safe)
            {
                win = true;
            } else {win = false;}
        }
        return win;
    }
}
