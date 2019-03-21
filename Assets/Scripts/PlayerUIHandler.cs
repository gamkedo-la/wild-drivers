using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    private CarDrive playerCarDrive;
    private CarNodeHandling playerCarNodeHandling;

    private float restartTimer = 5;
    private float restartTimerLeft = 5; //counting down before restarting the game.
    private bool isPaused = true;// set to true to countdown at the start of the round.
    private int playerNumber;
    private bool pauseInput;
    public GameObject restartCountdown;

    public int currentLap;
    public GameObject lapIndicator;

    public GameObject minimapIcon;// Is used for enabling minimapIcon gameobject when game starts.
    public GameObject speedTextObject;
    public float speedInKPH;
    // Start is called before the first frame update
    void Start()
    {
        playerCarDrive = gameObject.GetComponent<CarDrive>();
        playerCarNodeHandling = gameObject.GetComponent<CarNodeHandling>();

        playerNumber = playerCarDrive.playerNumber;
        currentLap = playerCarNodeHandling.currentLap;
        lapIndicator.GetComponent<Text>().text = "Current Lap:" + currentLap;
    }

    // Update is called once per frame
    void Update()
    {
        PauseHandling();


        speedInKPH = Mathf.Round((gameObject.GetComponent<Rigidbody>().velocity.magnitude) * 3.6f);
        speedTextObject.GetComponent<Text>().text = speedInKPH + "/Kph";
    }

    public void PauseHandling()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))//pause handling
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
            restartCountdown.SetActive(true);
            Time.timeScale = 0f;
            restartTimerLeft -= Time.unscaledDeltaTime;
            restartCountdown.GetComponent<Text>().text = Mathf.Ceil(restartTimerLeft).ToString();
        }
        else if (!isPaused)
        {
            restartTimerLeft = 0;
            Time.timeScale = 1f;
            restartCountdown.SetActive(false);
        }
    }

    public void LapUIHandling()
    {
        lapIndicator.GetComponent<Text>().text = "Current Lap:" + currentLap;
    }
}
