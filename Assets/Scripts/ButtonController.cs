﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public GameObject canvasMenuPanel;
    public GameObject canvasCarSelectionPanel;

    public void ChangeScene(string SceneName)
    {
        if (CarSelection.currentVehicle == null)
        {
            CarSelection.currentVehicle = "RaceCar";
        }
        SceneManager.LoadScene(SceneName);
    }
    public void VehicleSelection()
    {
        canvasMenuPanel.SetActive(false);
        canvasCarSelectionPanel.SetActive(true);
    }
    public void Options()
    {
        Debug.Log("Options button clicked");
    }
    public void Credits()
    {
        Debug.Log("Credits button clicked");
    }
    public void SelectVehicle(string vehicleName)
    {
        CarSelection.currentVehicle = vehicleName;
        canvasMenuPanel.SetActive(true);
        canvasCarSelectionPanel.SetActive(false);
    }
}
