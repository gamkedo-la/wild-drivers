using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRandomTransform : MonoBehaviour
{
    public int randomTransformY;
    public Vector3 tr;
    // Start is called before the first frame update
    void Start()
    {
        randomTransformY = Random.Range(0, 360);
        tr = new Vector3(0.0000f, randomTransformY, 0.0000f);
        transform.rotation = Quaternion.Euler(tr);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
