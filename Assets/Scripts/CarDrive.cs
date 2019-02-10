using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDrive : MonoBehaviour {
    public float driveSpeed = 13.0f;
    public float turnRate = 30.0f;
    public float boost = 100.0f;
    private float acceleration = 0.0f;
    private float accelerationCap = 1.0f;
    public string currentPowerUp;
    public float powerUpTimer;
    public bool isWeaponActive = false;
    public string currentWeapon;
    public int missileCount = 0;
    public GameObject bulletPrefab;

    public Transform restartAt;

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

        //Debug.Log(Input.GetAxisRaw("Vertical"));
        
        transform.position += transform.forward * Time.deltaTime * driveSpeed * acceleration;

        transform.Rotate(Vector3.up, turnRate * Time.deltaTime *
                         Input.GetAxisRaw("Horizontal") * acceleration);

        if (powerUpTimer > 0)
        {
            switch (currentPowerUp)//things to do until power up ends"
            {

                case "speedBoost":
                    accelerationCap = 1.3f;
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
                    acceleration += 0.3f;
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
