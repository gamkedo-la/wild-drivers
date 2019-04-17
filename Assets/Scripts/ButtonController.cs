using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public GameObject canvasMenuPanel;
    public GameObject canvasCarSelectionPanel;
    public GameObject canvasModeSelectionPanel;
	public GameObject optionsPanel;

	static public bool music = true;
	static public bool sound = true;

	private void Start()
	{
		Button b = optionsPanel.transform.GetChild(0).gameObject.GetComponent<Button>();
		ColorBlock cb = b.colors;
		cb.normalColor = cb.highlightedColor = music ? Color.white : Color.grey;
		b.colors = cb;

		b = optionsPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
		cb = b.colors;
		cb.normalColor = cb.highlightedColor = sound ? Color.white : Color.grey;
		b.colors = cb;
	}

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
    public void ModeSelection()
    {
        canvasMenuPanel.SetActive(false);
        canvasModeSelectionPanel.SetActive(true);
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
    public void ChangeMode(string modeName)
    {
        CarSelection.currentMode = modeName;
        canvasMenuPanel.SetActive(true);
        canvasModeSelectionPanel.SetActive(false);
    }
	public void ToggleMusic()
	{
		music = !music;

		Button b = optionsPanel.transform.GetChild(0).gameObject.GetComponent<Button>();
		ColorBlock cb = b.colors;
		cb.normalColor = cb.highlightedColor = music ? Color.white : Color.grey;
		b.colors = cb;

		AudioToggle.change = true;
	}
	public void ToggleSound()
	{
		sound = !sound;

		Button b = optionsPanel.transform.GetChild(1).gameObject.GetComponent<Button>();
		ColorBlock cb = b.colors;
		cb.normalColor = cb.highlightedColor = sound ? Color.white : Color.grey;
		b.colors = cb;

		AudioToggle.change = true;
	}
}
