using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    private float restartTimer = 5;
    private float restartTimerLeft = 5; //counting down before restarting the game.
    private bool isPaused = true;// set to true to countdown at the start of the round.
    private bool pauseInput;
    public GameObject pauseMenucanvas;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                pauseMenucanvas.SetActive(false);
                isPaused = false;
                restartTimerLeft = restartTimer;
            }
            else if(restartTimerLeft <= 0)
            {
                pauseMenucanvas.SetActive(true);
                isPaused = true;
                Time.timeScale = 0f;
            }
        }

        if (restartTimerLeft > 0)
        {
            isPaused = false;
            Time.timeScale = 0f;
            restartTimerLeft -= Time.unscaledDeltaTime;
        }
        else if (!isPaused)
        {
            restartTimerLeft = 0;
            Time.timeScale = 1f;
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void resumeGame()
    {
        pauseMenucanvas.SetActive(false);
        isPaused = false;
        restartTimerLeft = restartTimer;
    }
}
