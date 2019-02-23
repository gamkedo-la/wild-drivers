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
    public int playerNumber;
    private bool explodeInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        if (playerNumber == 1)
        {
            explodeInput = Input.GetButtonDown("Fire2");
        }
        else if (playerNumber == 2)
        {
            explodeInput = Input.GetKeyDown(KeyCode.T);
        }
        rb.AddForce(transform.forward * -thrust);
        //rb.AddForce(transform.forward * -thrust, ForceMode.Impulse);
    }

    void Update()
    {

        if (explodeInput)
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