using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public GameObject rocketPrefab;
    public GameObject rocketfiredPrefab;
    public GameObject homingMissilePrefab;
    public Transform AttachPoint;
    private GameObject powerUpToLaunch;// Power up that is current on the car
    private GameObject rocketFired;
    public GameObject smokeBombParticle;
    public GameObject smokeBombOnCar;
	
	private ParticleSystem smoke;
    private Rigidbody rb;
    private float missilelag = 0.75f;// scales initial velocity so that missile appears to have start up lag
    [SerializeField] private string currentPowerUp; //Current usable powerup
    private bool isPowerUpReadyToLaunch = false;// is there some powerup active on the attach point. If there is that means that player can use that power up.

    private int playerNumber;

    private float powerUpEffectTimer; // If powerUp does something when it is pushed for number of seconds (for example speed powerUp increases players speed for 5 seconds).
    private string currentPowerUpEffect; // Current powerUp Effect (Might be changed to a list later on)

    private bool fireInput;
    private bool fire2Input;
	


    void Start()
    {
        if (gameObject.GetComponent<CarDrive>() == null)
        {
            playerNumber = gameObject.GetComponent<BikeDrive>().playerNumber;
        }
        else
        {
            playerNumber = gameObject.GetComponent<CarDrive>().playerNumber;
        }

        rb = GetComponent<Rigidbody>();
		
    }


	void OnTriggerEnter(Collider pickup)
	{
		if (pickup && pickup.transform && pickup.transform.parent && pickup.transform.parent.gameObject &&
			pickup.transform.parent.gameObject.tag == "PowerUpZone" && !isPowerUpReadyToLaunch) // can be undefined
		{
			currentPowerUp = pickup.gameObject.tag;
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
					powerUpToLaunch = Instantiate(rocketPrefab, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;
					break;
				case "SmokeBomb":
					isPowerUpReadyToLaunch = true;
					pickup.gameObject.SetActive(false);
					powerUpToLaunch = Instantiate(smokeBombOnCar, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;
					break;
			}
			Destroy(pickup.gameObject);
		}
	}

	void FixedUpdate()
    {
        handleInput();
        if (powerUpEffectTimer > 0)
        {
            switch (currentPowerUpEffect)//things to do until power up ends
            {

                case "SpeedPowerUp":
                    //Debug.Log(Vector3.forward * 200);
                    //Debug.Log(gameObject.GetComponent<Rigidbody>().velocity.
                    break;
            }

            powerUpEffectTimer -= Time.deltaTime;
        }
        else if (powerUpEffectTimer < 0)
        {
            switch (currentPowerUpEffect)//things to do when power up ends
            {

                case "SpeedPowerUp":
                    //gameObject.GetComponent<CarDrive>().isBoostActive = false;
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
                    Destroy(powerUpToLaunch);
                    GameObject bullet = Instantiate(homingMissilePrefab, transform.position, transform.rotation);
                    GameObject enemy = (gameObject.GetComponent<CarDrive>().playerNumber == 1) ? GameObject.FindGameObjectWithTag("Player2") : GameObject.FindGameObjectWithTag("Player1");
                    bullet.GetComponent<MissileScript>().target = enemy;
                    bullet.GetComponent<MissileScript>().source = this.gameObject;
                    break;
                case "ToonMissile":
                    Destroy(powerUpToLaunch);
                    rocketFired = Instantiate(rocketfiredPrefab, AttachPoint.position, AttachPoint.rotation, AttachPoint.parent) as GameObject;
                    rocketFired.GetComponent<Rigidbody>().velocity = rb.velocity * missilelag;
                    rocketFired.GetComponent<Missile>().playerNumber = playerNumber;
                    break;
                case "SpeedPowerUp":
                    Destroy(powerUpToLaunch);
                    //powerUpEffectTimer = 5;
                    gameObject.GetComponent<Rigidbody>().velocity *= 1.3f;
                    //gameObject.GetComponent<CarDrive>().isBoostActive = true;
                    currentPowerUpEffect = currentPowerUp;
                    break;
                case "SmokeBomb":
                    Destroy(powerUpToLaunch);
                    GameObject smokeBomb = Instantiate(smokeBombParticle, AttachPoint.position - (Vector3.forward * 5), AttachPoint.rotation, AttachPoint.parent) as GameObject;
                    break;
            }
            isPowerUpReadyToLaunch = false;
            currentPowerUp = null;
        }
    }

    private void handleInput()
    {
        if (Time.timeScale != 0f)
        {
            if (playerNumber == 1)
            {
                fireInput = Input.GetButtonDown("Fire1");
            }
            else if (playerNumber == 2)
            {
                fireInput = Input.GetKeyDown("Fire1Player2");
            }
        }

    }



}
