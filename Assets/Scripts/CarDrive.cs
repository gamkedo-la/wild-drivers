using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour {

    public int playerNumber; //To determine which player is this script is on

    public float driveSpeed;
    public float maxTurnRate;
    private float turnRate;
    public float boost = 100.0f;
    public float acceleration = 0.0f;
    public float accelerationCap = 1.0f;
    public float brakeForce = 10f;
    public string currentPowerUp;
    public float powerUpTimer;
    public bool isWeaponActive = false;
    public string currentWeapon;
    public int missileCount = 0;
    public GameObject bulletPrefab;

    public Transform restartAt;

    public WheelCollider frontRightCollider, frontLeftCollider, backRightCollider, backLeftCollider;
    public Transform frontRightTransform, frontLeftTransform, backRightTransform, backLeftTransform;

    [SerializeField]private Rigidbody rb;
    private float verticalInput;
    private float horizontalInput;

	// Use this for initialization
	void Start () {
        //Debug.Log("Car object named " + gameObject.name +" started script!");

        rb = gameObject.GetComponent<Rigidbody>();
        if(rb == null) {
            Debug.LogWarning("Car isn't set up right, no rigidbody found?");
        }

        RestartAtSpawn();
	}

    public void RestartAtSpawn() {
        transform.position = restartAt.position;
        transform.rotation = restartAt.rotation;

        rb.angularVelocity = Vector3.zero;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartAtSpawn();
        }

        setInputForPlayer();
        Accelerate();
        Turn();
        UpdateWheelPose(frontRightCollider, frontRightTransform);
        UpdateWheelPose(frontLeftCollider, frontLeftTransform);
        UpdateWheelPose(backRightCollider, backRightTransform);
        UpdateWheelPose(backLeftCollider, backLeftTransform);

        //Debug.Log(verticalInput);

        //transform.position += transform.forward * Time.deltaTime * driveSpeed * acceleration;

        //transform.Rotate(Vector3.up, turnRate * Time.deltaTime *
                         //horizontalInput * acceleration);

        if (powerUpTimer > 0)
        {
            switch (currentPowerUp)//things to do until power up ends"
            {

                case "speedBoost":
                    break;
            }

            powerUpTimer -= Time.deltaTime;
        }
        else if(powerUpTimer < 0)
        {
            currentPowerUp = null;
            powerUpTimer = 0;
        }

        if (isWeaponActive && Input.GetKeyDown(KeyCode.Space) && missileCount > 0)
        {
            Debug.Log("hi");
            Shoot(currentWeapon);
        }

    } // end of Update()

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("asdf");
        currentPowerUp = other.gameObject.tag;
        if (other.transform.parent.gameObject.tag == "PowerUp")
        {
            switch (currentPowerUp)//One time thing to do on the first time power up is taken
            {
                case "HomingMissile":
                    Debug.Log("ses");
                    currentWeapon = "HomingMissile";
                    missileCount += 1;
                    isWeaponActive = true;
                    break;
                case "SpeedPowerUp":
                    driveSpeed += 0.3f;
                    break;
            }
            powerUpTimer = 5;
            Destroy(other.gameObject);
        }
    }

    public void Accelerate()
    {
            frontLeftCollider.motorTorque = driveSpeed * verticalInput;
            frontRightCollider.motorTorque = driveSpeed * verticalInput;    }

    private void Turn()
    {
        turnRate = maxTurnRate * horizontalInput;
        frontRightCollider.steerAngle = turnRate;
        frontLeftCollider.steerAngle = turnRate;
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position = wheelTransform.position;
        Quaternion rotation = wheelTransform.rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    public void Shoot(string weapon)
    {
        GameObject bullet = Instantiate(bulletPrefab,transform.position,transform.rotation);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistanceToEnemy = Mathf.Infinity;

        foreach (var item in enemies)
        {
            float distance = Vector3.Distance(item.transform.position, transform.position);
            if (distance < closestDistanceToEnemy)
            {
                closestDistanceToEnemy = distance;
                closestEnemy = item;
            }
        }
        bullet.GetComponent<MissileScript>().target = closestEnemy;
        bullet.GetComponent<MissileScript>().source = this.gameObject;
        missileCount -= 1;
    }

    public void setInputForPlayer()
    {

        if (playerNumber == 1)
        {
            verticalInput = Input.GetAxis("Vertical");
            horizontalInput = Input.GetAxis("Horizontal");
            Debug.Log(verticalInput);
        }
        else if (playerNumber == 2)
        {
            verticalInput = Input.GetAxis("Vertical2");
            horizontalInput = Input.GetAxis("Horizontal2");
            Debug.Log(verticalInput);
        }
        else
        {
            Debug.Log("Please set the playerNumber for object named" + gameObject.name);
        }
    }

} // end of class
