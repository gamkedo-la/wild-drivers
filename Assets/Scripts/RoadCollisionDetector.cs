using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCollisionDetector : MonoBehaviour
{
	public AudioSource audioSource;

	public AudioClip[] collisionSounds;
	public float minVolume = 0.1f;
	public float maxVelocity = 15f;
	
    void Start()
    {
        
    }
	
    void Update()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag.Contains("Player"))
		{
			float intensity = collision.relativeVelocity.magnitude / maxVelocity;
            Debug.Log(intensity);

            if (collision.gameObject.GetComponent<CarDrive>() != null && intensity <= 0.3f)
            {
                collision.gameObject.GetComponent<CarDrive>().frontLeftCollider.brakeTorque = Mathf.Infinity;
                collision.gameObject.GetComponent<CarDrive>().frontRightCollider.brakeTorque = Mathf.Infinity;
                collision.gameObject.GetComponent<CarDrive>().backLeftCollider.brakeTorque = Mathf.Infinity;
                collision.gameObject.GetComponent<CarDrive>().backRightCollider.brakeTorque = Mathf.Infinity;
            }


			if (intensity <= 0.3f)
            {
                audioSource.PlayOneShot(collisionSounds[0], (intensity * 2f) + minVolume);
            }
			else
				audioSource.PlayOneShot(collisionSounds[1], intensity + minVolume);
		}
	}
}
