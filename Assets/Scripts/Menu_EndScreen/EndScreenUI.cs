using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI_1;
    [SerializeField] private GameObject endScreenVisual;
    [SerializeField] private EndScreenStatistics endScreenStatistics;
    public static EndScreenUI instance;

    private void Awake()
    {
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
    public void EndScreenText()
    {
        string text;
        if (endScreenStatistics.rationalityScore <= 0)
        {
            text = ". A negative score means you acted overall irrationally.";
        }
        else {
            text = ". A positive score means you acted overall rationally.";
        }

        if (endScreenStatistics.WinOrLose())
        {
            interactTextMeshProUGUI_1.text = "You Survived! Your Rationality Score is " + endScreenStatistics.rationalityScore.ToString() + text;
        }
        else {
            interactTextMeshProUGUI_1.text = "You Died! Your Rationality Score is " + endScreenStatistics.rationalityScore.ToString() + text;
        }
    }

    public void ShowEndScreen()
    {
        endScreenVisual.SetActive(true);
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
