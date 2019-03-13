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
    public GameObject Player1countdown;
    public GameObject Player2countdown;
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
            Player1countdown.SetActive(true);
            Player2countdown.SetActive(true);
            Time.timeScale = 0f;
            restartTimerLeft -= Time.unscaledDeltaTime;
            Player1countdown.GetComponent<Text>().text = Mathf.Ceil(restartTimerLeft).ToString();
            Player2countdown.GetComponent<Text>().text = Mathf.Ceil(restartTimerLeft).ToString();
        }
        else if (!isPaused)
        {
            restartTimerLeft = 0;
            Time.timeScale = 1f;
            Player1countdown.SetActive(false);
            Player2countdown.SetActive(false);
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
