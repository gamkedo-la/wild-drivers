using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashIntoMe : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Debug.Log("Crash script started");
	}

	private void OnCollisionEnter(Collision collision)
	{
        CarDrive cdScript = collision.collider.gameObject.GetComponentInParent<CarDrive>();

        if(cdScript != null) {
            Debug.Log("Player crashed!");
            cdScript.RestartAtSpawn();
        } else {
            Debug.Log("I (" + gameObject.name + ") got bumped by " + collision.collider.gameObject.name);
        } // end of else
	} // end of collision function

} // end of class
