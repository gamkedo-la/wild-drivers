using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour {
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

	// Use this for initialization
	void Start () {
        Debug.Log("Car object named " + gameObject.name +" started script!");

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

        Accelerate();
        Turn();
        UpdateWheelPose(frontRightCollider, frontRightTransform);
        UpdateWheelPose(frontLeftCollider, frontLeftTransform);
        UpdateWheelPose(backRightCollider, backRightTransform);
        UpdateWheelPose(backLeftCollider, backLeftTransform);

        //Debug.Log(Input.GetAxisRaw("Vertical"));

        //transform.position += transform.forward * Time.deltaTime * driveSpeed * acceleration;

        //transform.Rotate(Vector3.up, turnRate * Time.deltaTime *
                         //Input.GetAxisRaw("Horizontal") * acceleration);

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
        if (Input.GetAxisRaw("Vertical") != 0)
        {
            if (acceleration < accelerationCap && acceleration > -accelerationCap)
            {
                acceleration += 0.01f * Input.GetAxisRaw("Vertical");
            }
        }
        else
        {
            if (acceleration > 0)
            {
                acceleration -= 0.005f;
            }
            if (acceleration < 0)
            {
                acceleration += 0.005f;
            }

        }
        //Debug.Log(frontLeftCollider.motorTorque);

        if ((frontLeftCollider.motorTorque > 0 && Input.GetAxisRaw("Vertical") < 0) ||
            (frontLeftCollider.motorTorque < 0 && Input.GetAxisRaw("Vertical") > 0))//braking
        {
            acceleration = 0;
            frontLeftCollider.brakeTorque += brakeForce;
            frontRightCollider.brakeTorque += brakeForce;

            if (frontLeftCollider.motorTorque > 0)// We are also decreasing the motor torque to the point where it is 0. When it is 0 we will get rid of the brake torque and player will drive backwards
            {
                frontLeftCollider.motorTorque -= brakeForce;
                frontRightCollider.motorTorque -= brakeForce;
            }
            else if (frontLeftCollider.motorTorque == 0)
            {
                frontRightCollider.motorTorque = 0;
            }
            else
            {
                frontLeftCollider.motorTorque += brakeForce;
                frontRightCollider.motorTorque += brakeForce;
            }

            //Debug.Log(frontLeftCollider.motorTorque);
            //Debug.Log(frontLeftCollider.brakeTorque);
        }
        else
        {
            frontLeftCollider.brakeTorque = 0;
            frontRightCollider.brakeTorque = 0;
            frontLeftCollider.motorTorque = driveSpeed * acceleration;
            frontRightCollider.motorTorque = driveSpeed * acceleration;
        }
    }

    private void Turn()
    {
        turnRate = maxTurnRate * Input.GetAxisRaw("Horizontal");
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

} // end of class
