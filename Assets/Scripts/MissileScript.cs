using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour {
    public GameObject target;
    public GameObject source;
    public float timeLeftToDie = 3;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, .3f);
        timeLeftToDie -= Time.deltaTime;
        if (timeLeftToDie <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target)
        {

            Destroy(this.gameObject);
        }
    }
}
