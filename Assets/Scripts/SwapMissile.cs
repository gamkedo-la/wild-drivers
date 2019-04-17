using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMissile : MonoBehaviour
{

    public float thrust;
    public GameObject explosionPrefab;
    private GameObject explosionParticles;
    private Rigidbody rb;
    public float radius = 5.0F;
    public float power = 10.0F;
    public int playerNumber;
    private bool explodeInput;
    private GameObject player1;
    private GameObject player2;
    private GameObject tmpObject;

    public AudioClip audioClip;
    public AudioSource audioSource;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
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
                {
                    if ((playerNumber == 1 && rb.gameObject.tag == "Player2") || (playerNumber == 2 && rb.gameObject.tag == "Player1"))
                    {
                        GameObject tmpObject = new GameObject();
                        tmpObject.transform.position = player1.transform.position;
                        tmpObject.transform.rotation = player1.transform.rotation;
                        player1.transform.position = player2.transform.position;
                        player1.transform.rotation = player2.transform.rotation;
                        player2.transform.position = tmpObject.transform.position;
                        player2.transform.rotation = tmpObject.transform.rotation;
                    }
                }
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