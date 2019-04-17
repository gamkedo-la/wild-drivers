using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject meshes;
    public BikeDrive bikeDriveScript;
    public float horizontalInput;
    public float bikeAcceleration;
    public GameObject wheels;
    private float speedInKPH;
    void Start()
    {
        bikeDriveScript = gameObject.GetComponentInParent<BikeDrive>();
        bikeAcceleration = bikeDriveScript.acceleration;
        horizontalInput = bikeDriveScript.horizontalInput;
    }
    private void FixedUpdate()
    {
        speedInKPH = Mathf.Round((gameObject.GetComponent<Rigidbody>().velocity.magnitude) * 3.6f);
        for (int i = 0; i < wheels.transform.childCount; i++)
        {
            wheels.transform.GetChild(i).localEulerAngles = new Vector3(wheels.transform.GetChild(i).localEulerAngles.x + speedInKPH*10, wheels.transform.GetChild(i).localEulerAngles.y, wheels.transform.GetChild(i).localEulerAngles.z);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bikeAcceleration = bikeDriveScript.acceleration;
        horizontalInput = gameObject.GetComponentInParent<BikeDrive>().horizontalInput;
        meshes.transform.localEulerAngles = new Vector3(meshes.transform.localEulerAngles.x, meshes.transform.localEulerAngles.y, -horizontalInput * 7 * bikeAcceleration);
        
    }
}
