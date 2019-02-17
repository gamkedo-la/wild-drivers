using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurboBoost : MonoBehaviour
{
    
    public float boostPower = 20000;
    public AudioSource triggerSound;
    public Transform triggerPrefab;

    // see https://docs.unity3d.com/ScriptReference/Collider.OnTriggerEnter.html
    
    void OnTriggerEnter(Collider other) 
    {
        Debug.Log("Turbo Boost Triggered!");

        if (other) {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb) {
                rb.AddForce(0, 0, boostPower, ForceMode.Impulse);
            } else {
                //Debug.Log("No RB to push!");
            }
        }

        if (triggerSound) {
            //if (collision.relativeVelocity.magnitude > 2)
                triggerSound.Play();
        }

        if (triggerPrefab) {
            Instantiate(triggerPrefab, transform.position, transform.rotation);            
        }
    }
    
}
