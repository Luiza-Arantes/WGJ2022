using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenuManager : MonoBehaviour
{
    public GameObject PauseCanvas;
    private bool isPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Time.timeScale = 1.0f;
                PauseCanvas.SetActive(false);
                isPaused = false;
                return;
            }
            Time.timeScale = 0f;
            PauseCanvas.SetActive(true);
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        PauseCanvas.SetActive(false);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
}
