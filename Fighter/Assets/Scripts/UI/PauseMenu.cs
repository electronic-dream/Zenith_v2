using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject deathMenuUI;

    public GameObject[] questions;

    public Health hp;

    private void Update()
    {
        int i = 0;

        if (!questions[i].activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                    Pause();
            }
            
            if (i <= questions.Length)
                i++;

            if (Health.askedQuestion)
                StartCoroutine(DeathMenu());
            else
                StopCoroutine(DeathMenu());
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        hp.immortal = false;
        Health.askedQuestion = false;
        Health.health = 1;
        SceneManager.LoadScene("Level1");
    }

    public void GoToMenu(string levelName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }

    public IEnumerator DeathMenu()
    {
        if (questions[questions.Length - 1].activeSelf)
            yield break;

        yield return new WaitForSecondsRealtime(2f);

        if (Health.health <= 0 && !questions[questions.Length - 1].activeSelf)
        {
            deathMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
