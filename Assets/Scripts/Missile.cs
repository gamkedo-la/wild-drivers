using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public float thrust;
    public GameObject explosionPrefab;
    private GameObject explosionParticles;
    private Rigidbody rb;
    public float radius = 5.0F;
    public float power = 10.0F;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        rb.AddForce(transform.forward * -thrust);
        //rb.AddForce(transform.forward * -thrust, ForceMode.Impulse);
    }

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
                    rb.AddExplosionForce(power, explosionPos, radius, 10.0F);
                explosionParticles = Instantiate(explosionPrefab, transform.position, transform.rotation) as GameObject;
                Destroy(gameObject);
                
            }
        }
    }
}