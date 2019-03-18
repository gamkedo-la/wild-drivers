using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeDrive : MonoBehaviour
{
    public int playerNumber; //To determine which player is this script is on

    public float driveSpeed;
    public float maxTurnRate;
    private float turnRate;
    public float boost = 100.0f;
    public float acceleration = 0.0f;
    public float accelerationCap = 1.0f;
    public float brakeForce = 10f;
    public string currentPowerUp;

    public Transform restartAt;

    public WheelCollider frontCollider, backCollider;
    public Transform frontTransform, backTransform;

    [SerializeField] private Rigidbody rb;
    public float verticalInput;
    public float horizontalInput;

    //public GameObject minimapIcon;// Is used for enabling minimapIcon gameobject when game starts.

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Car object named " + gameObject.name +" started script!");

        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogWarning("Car isn't set up right, no rigidbody found?");
        }

        frontCollider.ConfigureVehicleSubsteps(5, 12, 15);
        RestartAtSpawn();
        Debug.Log(gameObject.GetComponent<Rigidbody>().centerOfMass);
        //gameObject.GetComponent<Rigidbody>().centerOfMass = new Vector3(0.0f, -0.5f, 0.0f);
    }

    public void RestartAtSpawn()
    {
        transform.position = restartAt.position;
        transform.rotation = restartAt.rotation;

        rb.angularVelocity = Vector3.zero;
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
        UpdateWheelPose(frontCollider, frontTransform);
        UpdateWheelPose(backCollider, backTransform);

        //Debug.Log(verticalInput);

        //transform.position += transform.forward * Time.deltaTime * driveSpeed * acceleration;

        //transform.Rotate(Vector3.up, turnRate * Time.deltaTime *
    } // end of Update()


    public void Accelerate()
    {
        frontCollider.motorTorque = driveSpeed * verticalInput;
        backCollider.motorTorque = driveSpeed * verticalInput;

        /*frontLeftCollider.ConfigureVehicleSubsteps(5, 12, 15);
        frontRightCollider.ConfigureVehicleSubsteps(5, 12, 15);
        backLeftCollider.ConfigureVehicleSubsteps(5, 12, 15);
        backRightCollider.ConfigureVehicleSubsteps(5, 12, 15);*/
    }

    private void Turn()
    {
        turnRate = maxTurnRate * horizontalInput;
        frontCollider.steerAngle = turnRate;
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position = wheelTransform.position;
        Quaternion rotation = wheelTransform.rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
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
