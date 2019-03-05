using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLock : MonoBehaviour
{
	private float yVal = 0f;
	Vector3 dir = Vector3.zero;
    private float currentVelocityZ;
    private float currentVelocityX;
    public GameObject player;
    private float sliderX = 0;
    private float sliderZ = 0;
    private float startingZ;
    private float startingX;

    private void Start()
    {
        startingX = transform.localPosition.x;
        startingZ = transform.localPosition.z;
    }

    void LateUpdate()
    {
        Debug.Log(player.transform.InverseTransformDirection(player.GetComponent<Rigidbody>().velocity).z);
        currentVelocityZ = player.transform.InverseTransformDirection(player.GetComponent<Rigidbody>().velocity).z;
        currentVelocityX = player.transform.InverseTransformDirection(player.GetComponent<Rigidbody>().velocity).x;

        yVal = transform.rotation.eulerAngles.y;
		dir = new Vector3(0f, yVal, 0f);
		transform.rotation = Quaternion.Euler(dir);

        sliderX = currentVelocityX / 10;
        if (sliderX > 1)
        {
            sliderX = 1;
        }
        if (sliderX < -1)
        {
            sliderX = -1;
        }

        sliderZ = currentVelocityZ / 25;
        if (sliderZ > 1)
        {
            sliderZ = 1;
        }
        if (sliderZ < 0)
        {
            sliderZ = 0;
        }
        Debug.Log(sliderZ);
        transform.localPosition = new Vector3(startingX - (sliderX * 6) , transform.localPosition.y, startingZ - (sliderZ * 4));
    }
}
