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

    void OnTriggerEnter(Collider pickup)
    {
        if (pickup.gameObject.CompareTag("Pickup"))
        {
            pickup.gameObject.SetActive(false);

            rockettoLaunch = Instantiate(rocketPrefab, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;
            // rocketPrefab.GetComponentInChildren<ParticleSystem>().enableEmission = false;
           // rockettoLaunch.gameObject.SetActive(true);// why is this needed? check has prefab got set to off in error?
            //rockettoLaunch.GetComponentInChildren<ParticleSystem>();
        } 
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            //rocketPrefab.GetComponentInChildren<ParticleSystem>().enableEmission = true;
            Debug.Log("fire missile");
            rockettoLaunch.gameObject.SetActive(false);
            //rockettoLaunch.transform.Translate(0, 0, -12 * Time.deltaTime);
            rockettoLaunch = Instantiate(rocketfiredPrefab, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;
        }
    }



}
