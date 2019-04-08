using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ModeHandlingScript : MonoBehaviour
{
    public GameObject player1Vehicles;
    public GameObject player2Vehicles;

    //Time trial mode stuff
    public GameObject currentTimeObject;
    public Text currentTimeText;
    public float currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        currentTimeText = currentTimeObject.GetComponent<Text>();
        currentTimeText.text = "Current Time :" + currentTime;
        if (CarSelection.currentMode == "Multiplayer")
        {
            currentTimeObject.SetActive(false);
            player1Vehicles.SetActive(true);
            player2Vehicles.SetActive(true);
        }
        else if (CarSelection.currentMode == "TimeTrial")
        {
            currentTimeObject.SetActive(true);
            player1Vehicles.SetActive(true);
            player2Vehicles.SetActive(false);

            for (int i = 0; i < player1Vehicles.transform.childCount; i++)
            {
				player1Vehicles.transform.GetChild(i).Find("CameraViews").GetComponent<CameraViewSwitcher>().SetSinglePlayerViewport();
				player1Vehicles.transform.GetChild(i).Find("Rearview Mirror").GetComponent<Camera>().rect = new Rect(0.35f, 0.9f, 0.3f, 0.1f);
            }
        }
    }
    void Update()
    {
        if (currentTimeObject.activeSelf)
        {
            currentTime += Time.deltaTime;
            Debug.Log(currentTime);
            currentTimeText.text = "Current Time :" + System.Math.Round(currentTime,2);
        }
    }
}
