using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosiontest : MonoBehaviour
{
    
    public float radius = 500.0F;
    public float power = 1000.0F;

    void Update()
    {

        if (Input.GetButtonDown("Fire2"))
        {
            
            Vector3 explosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                    rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
               
            }
        }
    }
}
