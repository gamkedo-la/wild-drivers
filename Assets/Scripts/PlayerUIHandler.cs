using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
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
	public float maxSpeedInKPH;

	private AudioSource audSrc;

	// Start is called before the first frame update
	void Start()
    {
        minimapIcon.SetActive(true);
        if (gameObject.GetComponent<CarDrive>() == null)
        {
            playerNumber = gameObject.GetComponent<BikeDrive>().playerNumber;
        }
        else
        {
            playerNumber = gameObject.GetComponent<CarDrive>().playerNumber;
        }
        playerCarNodeHandling = gameObject.GetComponent<CarNodeHandling>();

        currentLap = playerCarNodeHandling.currentLap;
        lapIndicator.GetComponent<Text>().text = "Current Lap:" + currentLap;

		audSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PauseHandling();


        speedInKPH = Mathf.Round((gameObject.GetComponent<Rigidbody>().velocity.magnitude) * 3.6f);
        speedTextObject.GetComponent<Text>().text = speedInKPH + "/Kph";
		audSrc.volume = speedInKPH / maxSpeedInKPH;
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
