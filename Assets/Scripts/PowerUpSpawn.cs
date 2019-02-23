using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    public List<GameObject> powerUps;// gameobject that contains all the power ups as a child
    public int powerUpSpawnCount = 4; // how many power ups will spawn in one spawn point
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameObject.FindGameObjectWithTag("PowerUp").transform.childCount; i++)
        {
            powerUps.Add(GameObject.FindGameObjectWithTag("PowerUp").transform.GetChild(i).gameObject);
        };
        //GameObject.FindGameObjectWithTag("PowerUp").SetActive(false);
        Debug.Log(powerUps[0]);

        for (int i = 0; i < powerUpSpawnCount; i++)
        {
            GameObject currentGameobject = GameObject.Instantiate(powerUps[Random.Range(0, powerUps.Count)], transform);
            currentGameobject.SetActive(true);
            currentGameobject.transform.localPosition = new Vector3(i * 3, 0, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
