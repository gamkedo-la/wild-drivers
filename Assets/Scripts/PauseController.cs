using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private float restartTimer = 5;
    private float restartTimerLeft = 5; //counting down before restarting the game.
    private bool isPaused = true;// set to true to countdown at the start of the round.
    private int playerNumber;
    private bool pauseInput;
    public GameObject Countdown;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = gameObject.GetComponent<CarDrive>().playerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                isPaused = false;
                restartTimerLeft = restartTimer;
            }
            else
            {
                isPaused = true;
                Time.timeScale = 0f;
            }
        }

        if (restartTimerLeft > 0)
        {
            isPaused = false;
            Countdown.SetActive(true);
            Time.timeScale = 0f;
            restartTimerLeft -= Time.unscaledDeltaTime;
            Countdown.GetComponent<Text>().text = Mathf.Ceil(restartTimerLeft).ToString();
        }
        else if (!isPaused)
        {
            restartTimerLeft = 0;
            Time.timeScale = 1f;
            Countdown.SetActive(false);
        }
    }
}
