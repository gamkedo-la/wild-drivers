﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeParticleScript : MonoBehaviour
{
    // Start is called before the first frame update,
    ParticleSystem ps;
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(270, 0, 0);
        if (!ps.IsAlive())
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
