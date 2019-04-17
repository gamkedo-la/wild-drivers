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

    public AudioClip audioClip;
    public AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

		if (ButtonController.sound)
		{
			audioSource = gameObject.GetComponent<AudioSource>();
			audioSource.clip = audioClip;
			audioSource.Play();
		}
    }

    void FixedUpdate()
    {
        handleInput();
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
    
    private void handleInput()
    {
        if (playerNumber == 1 && Time.timeScale != 0f)
        {
            explodeInput = Input.GetButtonDown("Fire2");
        }
        else if (playerNumber == 2 && Time.timeScale != 0f)
        {
            explodeInput = Input.GetButtonDown("Fire2Player2");
        }
        else
        {
            explodeInput = false;
        }
    }
}