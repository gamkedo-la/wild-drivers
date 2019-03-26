 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeDrive : MonoBehaviour
{
    public int playerNumber; //To determine which player is this script is on

    public float driveSpeed;
    public float turnRate = 25;
    public float acceleration = 0.0f;
    public float accelerationCap = 1.0f;

    public Transform restartAt;

    public Transform frontTransform, backTransform;

    [SerializeField] private Rigidbody rb;
    public float verticalInput;
    public float horizontalInput;

    //public GameObject minimapIcon;// Is used for enabling minimapIcon gameobject when game starts.
    private void Awake()
    {
        //Code for the build. It is used for car selection menu.
        /*
        if (CarSelection.currentVehicle != "Motorcycle")
        {
            gameObject.SetActive(false);
        }
        */
    }

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Car object named " + gameObject.name +" started script!");

        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Car isn't set up right, no rigidbody found?");
        }
        RestartAtSpawn();
        Debug.Log(gameObject.GetComponent<Rigidbody>().centerOfMass);
        //gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);
    }

    public void RestartAtSpawn()
    {
        transform.position = restartAt.position;
        transform.rotation = restartAt.rotation;

        rb.velocity = Vector3.zero;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartAtSpawn();
        }

        setInputForPlayer();
        Accelerate();
        Turn();
        UpdateWheelPose(frontTransform);
        UpdateWheelPose(backTransform);

        //Debug.Log(verticalInput);

        //transform.position += transform.forward * Time.deltaTime * driveSpeed * acceleration;

        //transform.Rotate(Vector3.up, turnRate * Time.deltaTime *
    } // end of Update()


    public void Accelerate()
    {
        if (verticalInput != 0)
        {
            acceleration += verticalInput / 50;
        }
        else
        {
            acceleration *= 0.98f;
        }
        if (acceleration > accelerationCap)
        {
            acceleration = accelerationCap;
        }
        else if (acceleration < -accelerationCap)
        {
            acceleration = -accelerationCap;
        }
        rb.velocity = transform.forward * Time.deltaTime * driveSpeed * acceleration;
        
    }

    private void Turn()
    {
        //transform.Rotate(Vector3.up, turnRate * Time.deltaTime * horizontalInput *acceleration);
        rb.angularVelocity = new Vector3(0, turnRate * horizontalInput * acceleration, 0);
    }

    private void UpdateWheelPose(Transform wheelTransform)
    {

    }

    public void setInputForPlayer()
    {
        if (Time.timeScale != 0)
        {
            if (playerNumber == 1)
            {
                verticalInput = Input.GetAxis("Vertical");
                horizontalInput = Input.GetAxis("Horizontal");
                //Debug.Log(verticalInput);
            }
            else if (playerNumber == 2)
            {
                verticalInput = Input.GetAxis("Vertical2");
                horizontalInput = Input.GetAxis("Horizontal2");
                //Debug.Log(verticalInput);
            }
            else
            {
                Debug.Log("Please set the playerNumber for object named" + gameObject.name);
            }
        }
        else
        {
            verticalInput = 0;
            horizontalInput = 0;
        }

    }
}
