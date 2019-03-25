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
    void Start()
    {
        bikeDriveScript = gameObject.GetComponentInParent<BikeDrive>();
        bikeAcceleration = bikeDriveScript.acceleration;
        horizontalInput = bikeDriveScript.horizontalInput;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        bikeAcceleration = bikeDriveScript.acceleration;
        horizontalInput = gameObject.GetComponentInParent<BikeDrive>().horizontalInput;
        meshes.transform.localEulerAngles = new Vector3(meshes.transform.localEulerAngles.x, meshes.transform.localEulerAngles.y, -horizontalInput * 7 * bikeAcceleration);
    }
}
