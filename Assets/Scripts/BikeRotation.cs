using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BikeRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float horizontalInput;
    void Start()
    {
        horizontalInput = gameObject.GetComponentInParent<CarDrive>().horizontalInput;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        horizontalInput = gameObject.GetComponentInParent<CarDrive>().horizontalInput;
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -horizontalInput * 15);
    }
}
