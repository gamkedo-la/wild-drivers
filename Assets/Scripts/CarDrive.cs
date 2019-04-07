using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour {

    public int playerNumber; //To determine which player is this script is on

    public float startingDriveSpeed;
    public float driveSpeed;
    public float maxTurnRate;
    private float turnRate;
    public float boost = 100.0f;
    public float acceleration = 0.0f;
    public float accelerationCap = 1.0f;
    public float brakeForce = 10f;
    public string currentPowerUp;
    public bool isBoostActive;

	
    public Transform restartAt;

    public WheelCollider frontRightCollider, frontLeftCollider, backRightCollider, backLeftCollider, bikeBackCollider, bikeFrontCollider;
    public Transform frontRightTransform, frontLeftTransform, backRightTransform, backLeftTransform, bikeBackTransform, bikeFrontTransform;

    private Vector3 centerOfWheelColiiders;

    [SerializeField]private Rigidbody rb;
    public float verticalInput;
    public float horizontalInput;

	//public GameObject minimapIcon;// Is used for enabling minimapIcon gameobject when game starts.
	
	private void Awake()
    {
        //Code for the build. It is used for car selection menu.

        
        if (CarSelection.currentVehicle != "RaceCar")
        {
            //gameObject.SetActive(false);
        }
        else if(playerNumber == 1)
        {
            CarSelection.audioListener = gameObject.transform.Find("CameraViews").GetComponent<AudioListener>();
        }
    }

    // Use this for initialization
    void Start () {
        //Debug.Log("Car object named " + gameObject.name +" started script!");
        driveSpeed = startingDriveSpeed;

        rb = gameObject.GetComponent<Rigidbody>();
        if(rb == null) {
            Debug.LogWarning("Car isn't set up right, no rigidbody found?");
        }
        if (frontLeftCollider != null)
        {
            frontLeftCollider.ConfigureVehicleSubsteps(5, 12, 15);
        }
        if (backLeftCollider != null)
        {
            backLeftCollider.ConfigureVehicleSubsteps(5, 12, 15);
        }
        RestartAtSpawn();
        centerOfWheelColiiders = (frontRightCollider.gameObject.transform.localPosition + frontLeftCollider.gameObject.transform.localPosition + backRightCollider.gameObject.transform.localPosition + backLeftCollider.gameObject.transform.localPosition) / 4;
        gameObject.GetComponent<Rigidbody>().centerOfMass = centerOfWheelColiiders;
        Debug.Log(gameObject.GetComponent<Rigidbody>().centerOfMass);
	}

    public void RestartAtSpawn() {
        transform.position = restartAt.position;
        transform.rotation = restartAt.rotation;

        //rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartAtSpawn();
        }
        gameObject.GetComponent<Rigidbody>().centerOfMass = centerOfWheelColiiders;
        //Debug.Log(rb.angularVelocity);
        //Debug.Log(gameObject.GetComponent<Rigidbody>().centerOfMass + gameObject.name);

        /*if (isBoostActive)
        {
            //driveSpeed = startingDriveSpeed * 2;
            rb.
        }
        else
        {
            //driveSpeed = startingDriveSpeed;
        }*/

        setInputForPlayer();
        Accelerate();
        Turn();

        if (frontLeftCollider != null)
        {
            UpdateWheelPose(frontRightCollider, frontRightTransform);
            UpdateWheelPose(frontLeftCollider, frontLeftTransform);
            UpdateWheelPose(backRightCollider, backRightTransform);
            UpdateWheelPose(backLeftCollider, backLeftTransform);
        }

        /*if (bikeBackCollider != null)
        {
            UpdateWheelPose(bikeBackCollider, bikeBackTransform);
            UpdateWheelPose(bikeFrontCollider, bikeFrontTransform);
        }*/

        //Debug.Log(verticalInput);

        //transform.position += transform.forward * Time.deltaTime * driveSpeed * acceleration;

        //transform.Rotate(Vector3.up, turnRate * Time.deltaTime *
    } // end of Update()


    public void Accelerate()
    {
        if (frontLeftCollider != null)
        {
            frontLeftCollider.motorTorque = driveSpeed * verticalInput;
            frontRightCollider.motorTorque = driveSpeed * verticalInput;
            backLeftCollider.motorTorque = driveSpeed * verticalInput;
            backRightCollider.motorTorque = driveSpeed * verticalInput;
            /*if (playerNumber == 1)
            {
                Debug.Log(frontLeftCollider.);
                Debug.Log(frontRightCollider.forwardFriction);
                Debug.Log(backLeftCollider.forwardFriction);
                Debug.Log(backRightCollider.forwardFriction);
            }*/
        }

        /*if (bikeBackCollider != null)
        {
            bikeBackCollider.motorTorque = driveSpeed * verticalInput;
            bikeFrontCollider.motorTorque = driveSpeed * verticalInput;
        }*/

        /*frontLeftCollider.ConfigureVehicleSubsteps(5, 12, 15);
        frontRightCollider.ConfigureVehicleSubsteps(5, 12, 15);
        backLeftCollider.ConfigureVehicleSubsteps(5, 12, 15);
        backRightCollider.ConfigureVehicleSubsteps(5, 12, 15);*/
    }

    private void Turn()
    {
        turnRate = maxTurnRate * horizontalInput;

        if (frontLeftCollider != null)
        {
            frontRightCollider.steerAngle = turnRate;
            frontLeftCollider.steerAngle = turnRate;
        }

        if (bikeBackCollider != null)
        {
            bikeFrontCollider.steerAngle = turnRate;
        }

    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        if (bikeBackCollider != null)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, -horizontalInput * 5);
        }
        else
        {
            Vector3 position = wheelTransform.position;
            Quaternion rotation = wheelTransform.rotation;
            wheelCollider.GetWorldPose(out position, out rotation);
            wheelTransform.position = position;
            wheelTransform.rotation = rotation;
        }
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
} // end of class
