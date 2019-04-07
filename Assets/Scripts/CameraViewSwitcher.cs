using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewSwitcher : MonoBehaviour
{
	public int viewIndex = 0;
	private int totalViews = 0;

    void Start()
    {
		totalViews = transform.childCount;

		for (int i = 0; i < totalViews; i++)
			transform.GetChild(i).gameObject.SetActive(false);
		
		transform.GetChild(viewIndex).gameObject.SetActive(true);
    }
	
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.V))
		{
			viewIndex++;
			viewIndex = viewIndex >= totalViews ? 0 : viewIndex;

			for (int i = 0; i < totalViews; i++)
				transform.GetChild(i).gameObject.SetActive(false);

			transform.GetChild(viewIndex).gameObject.SetActive(true);
		}
    }

	public void SetSinglePlayerViewport()
	{
		for (int i = 0; i < totalViews; i++)
			transform.GetChild(i).gameObject.GetComponent<Camera>().rect = new Rect(0, 0, 1, 1);
	}
}
