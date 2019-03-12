using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUIHandler : MonoBehaviour
{
    public GameObject minimapIcon;// Is used for enabling minimapIcon gameobject when game starts.
    public GameObject speedTextObject;
    public float speedInKPH;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        speedInKPH = Mathf.Round((gameObject.GetComponent<Rigidbody>().velocity.magnitude) * 3.6f);
        speedTextObject.GetComponent<Text>().text = speedInKPH + "/Kph";
    }
}
