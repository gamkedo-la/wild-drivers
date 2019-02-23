using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject rocketfiredPrefab;
    public Transform AttachPoint;
    private GameObject rockettoLaunch;
    private GameObject rocketFired;
    private ParticleSystem smoke;
    private Rigidbody rb;
    private float missilelag = 0.75f;// scales initial velocity so that missile appears to have start up lag
    private bool missileloaded = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }


    void OnTriggerEnter(Collider pickup)
    {
        if (pickup.gameObject.CompareTag("Pickup"))
        {
            pickup.gameObject.SetActive(false);
            missileloaded = true;
            rockettoLaunch = Instantiate(rocketPrefab, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;

        } 
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && missileloaded)
        {
            
            Destroy(rockettoLaunch);
            rocketFired = Instantiate(rocketfiredPrefab, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;
            rocketFired.GetComponent<Rigidbody>().velocity = rb.velocity * missilelag;
            missileloaded = false;

        }
    }



}
