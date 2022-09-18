using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject EscMenuPanel;
    private void Start()
    {
        EscMenuPanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                PauseMenu();
            }
        }
    }

    void Resume()
    {
        EscMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void PauseMenu()
    {
        EscMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void ResumeButton()
    {
        EscMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MaýnMenuButton()
    {
        SceneManager.LoadScene("MaýnMenu");
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
