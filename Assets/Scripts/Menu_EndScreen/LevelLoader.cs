using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float transitionTime = 1f;
    private int lastScene = 4; //1st floor
    [SerializeField] private EndScreenUI endScreenUI;
    public static LevelLoader instance;

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

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadEndScreen()
    {
        StartCoroutine(LoadLevel(lastScene));
        endScreenUI.EndScreenText();
        endScreenUI.ShowEndScreen();
    }

    public int CurrentSceneNumber()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
