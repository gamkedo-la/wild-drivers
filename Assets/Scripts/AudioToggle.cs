using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioToggle : MonoBehaviour
{
	public bool forMusic = true;
	public AudioSource[] audioSources;

	static public bool change = false;

	private void Start()
	{
		change = true;
	}

	void Update()
	{
		if (change)
		{
			if ((forMusic && !ButtonController.music)
			|| (!forMusic && !ButtonController.sound))
			{
				for (int i = 0; i < audioSources.Length; i++)
				{
					audioSources[i].mute = true;
				}
			}
			else if ((forMusic && ButtonController.music)
			|| (!forMusic && ButtonController.sound))
			{
				for (int i = 0; i < audioSources.Length; i++)
				{
					audioSources[i].mute = false;
				}
			}
		}
    }

	private void LateUpdate()
	{
		change = false;
	}
}
