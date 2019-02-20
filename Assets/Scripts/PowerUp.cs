using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject rocketPrefab;
    public Transform AttachPoint;

    void OnTriggerEnter(Collider pickup)
    {
        if (pickup.gameObject.CompareTag("Pickup"))
        {
            pickup.gameObject.SetActive(false);

            Instantiate(rocketPrefab,AttachPoint.position, AttachPoint.rotation, AttachPoint.parent);
            
        }
    }
}
