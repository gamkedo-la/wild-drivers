using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject rocketfiredPrefab;
    public GameObject homingMissilePrefab;
    public Transform AttachPoint;
    private GameObject rockettoLaunch;
    private GameObject rocketFired;
    private ParticleSystem smoke;
    private Rigidbody rb;
    private float missilelag = 0.75f;// scales initial velocity so that missile appears to have start up lag
    [SerializeField] private string currentPowerUp; //Current usable powerup
    private bool isPowerUpReadyToLaunch = false;// is there some powerup active on the attach point. If there is that means that player can use that power up.

    private float powerUpEffectTimer; // If powerUp does something when it is pushed for number of seconds (for example speed powerUp increases players speed for 5 seconds).
    private string currentPowerUpEffect; // Current powerUp Effect (Might be changed to a list later on)

    private bool fireInput;
    private bool fire2Input;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }


    void OnTriggerEnter(Collider pickup)
    {
        if (pickup && pickup.transform && pickup.transform.parent && pickup.transform.parent.gameObject &&
            pickup.transform.parent.gameObject.tag == "PowerUpZone" && !isPowerUpReadyToLaunch) // can be undefined
        {
            currentPowerUp = pickup.gameObject.tag;
            Debug.Log("picked up something");
            switch (currentPowerUp)//One time thing to do on the first time power up is taken
            {
                case "HomingMissile":
                    isPowerUpReadyToLaunch = true;
                    pickup.gameObject.SetActive(false);
                    break;
                case "SpeedPowerUp":
                    isPowerUpReadyToLaunch = true;
                    pickup.gameObject.SetActive(false);
                    break;
                case "ToonMissile":
                    isPowerUpReadyToLaunch = true;
                    pickup.gameObject.SetActive(false);
                    rockettoLaunch = Instantiate(rocketPrefab, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;
                    break;
            }
            Destroy(pickup.gameObject);
        }
    }

    void Update()
    {
        handleInput();
        if (powerUpEffectTimer > 0)
        {
            switch (currentPowerUpEffect)//things to do until power up ends"
            {

                case "SpeedPowerUp":
                    break;
            }

            powerUpEffectTimer -= Time.deltaTime;
        }
        else if (powerUpEffectTimer < 0)
        {
            switch (currentPowerUpEffect)//things to do until power up ends"
            {

                case "SpeedPowerUp":
                    gameObject.GetComponent<CarDrive>().driveSpeed /= 3;
                    break;
            }
            currentPowerUpEffect = null;
            powerUpEffectTimer = 0;
        }



        if (isPowerUpReadyToLaunch && fireInput) // firing the PowerUp
        {
            switch (currentPowerUp)
            {
                case "HomingMissile":
                    GameObject bullet = Instantiate(homingMissilePrefab, transform.position, transform.rotation);
                    GameObject enemy = (gameObject.GetComponent<CarDrive>().playerNumber == 1) ? GameObject.FindGameObjectWithTag("Player2") : GameObject.FindGameObjectWithTag("Player1");
                    bullet.GetComponent<MissileScript>().target = enemy;
                    bullet.GetComponent<MissileScript>().source = this.gameObject;
                    break;
                case "ToonMissile":
                    Destroy(rockettoLaunch);
                    rocketFired = Instantiate(rocketfiredPrefab, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;
                    rocketFired.GetComponent<Rigidbody>().velocity = rb.velocity * missilelag;
                    rocketFired.GetComponent<Missile>().playerNumber = gameObject.GetComponent<CarDrive>().playerNumber;
                    break;
                case "SpeedPowerUp":
                    powerUpEffectTimer = 5;
                    gameObject.GetComponent<CarDrive>().driveSpeed *= 5;
                    currentPowerUpEffect = currentPowerUp;
                    break;
            }
            isPowerUpReadyToLaunch = false;
            currentPowerUp = null;
        }
    }

    private void handleInput()
    {
        if (gameObject.GetComponent<CarDrive>().playerNumber == 1)
        {
            fireInput = Input.GetButtonDown("Fire1");
        }
        else if (gameObject.GetComponent<CarDrive>().playerNumber == 2)
        {
            fireInput = Input.GetKeyDown(KeyCode.Space);
        }
    }



}
