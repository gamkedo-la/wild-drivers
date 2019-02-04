using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {

    public Transform currentPoint;
    public int nextPoint;
    public Transform nodeList;

    public float speed = 13.56F;
    public float turnRate = 30.0f;
    private float acceleration = 0.0f;
    private float accelerationCap = 1.0f;
    private float startTime;
    private float journeyLength;
    public List<Transform> nodes;

    void Start()
    {
        startTime = Time.time;
        //currentPoint = transform;

        nodeList = GameObject.FindGameObjectWithTag("NodeList").transform;
        for (int i = 0; i < nodeList.childCount; i++)
        {
            nodes.Add(nodeList.GetChild(i));
        }
        nextPoint = 1;
    }

    void Update()
    {
        //Debug.Log(nextPoint);
        if (nextPoint > 0)
        {
            Vector3 currentTrackSegment = nodes[nextPoint].transform.position - nodes[nextPoint - 1].transform.position;
            Vector3 nextTrackSegment = nodes[nextPoint + 1].transform.position - nodes[nextPoint].transform.position;
            Vector3 perpendiculerBetweenTwoRoads = Vector3.Cross(nextTrackSegment, currentTrackSegment);
            float angleBetweenCarAndPerpendiculer = Vector3.SignedAngle(transform.position, perpendiculerBetweenTwoRoads - transform.position, Vector3.up);
            Debug.Log(angleBetweenCarAndPerpendiculer);
            if (angleBetweenCarAndPerpendiculer < 0)
            {
                nextPoint++;
                Debug.Log("Road changed");
            }


            //Debug.Log(Vector3.SignedAngle(transform.forward, currentTrackSegment - transform.position, Vector3.up));
        }
        if (Vector3.Distance(transform.position, nodes[nextPoint].transform.position) < 4f)
        {
            nodes[nextPoint].GetComponent<Node>().playersThatPassedBy.Add(this.gameObject);
            nextPoint++;
            //Debug.Log("hi");
        }
        else
        {
            Accelerate();
            transform.position += transform.forward * Time.deltaTime * speed * acceleration;
        }
        
        if (Vector3.Angle(transform.forward, nodes[nextPoint].transform.position - transform.position) > 5)
        {
            if (Vector3.SignedAngle(transform.forward, nodes[nextPoint].transform.position - transform.position, Vector3.up) > 5)
            {
                transform.Rotate(Vector3.up, turnRate * Time.deltaTime  * acceleration);
                //Debug.Log(acceleration);
            }
            if (Vector3.SignedAngle(transform.forward, nodes[nextPoint].transform.position - transform.position, Vector3.up) < -5)
            {
                transform.Rotate(-Vector3.up, turnRate * Time.deltaTime * acceleration);
                //Debug.Log(acceleration);
            }
        }

    }

    public void Accelerate()
    {

        if (Vector3.Angle(transform.forward, nodes[nextPoint].transform.position - transform.position) > 30)
        {
            accelerationCap = 0.3f;
            turnRate = 100f;
        }
        else
        {
            turnRate = 63f;
            accelerationCap = 1f;
        }
        if (accelerationCap < acceleration)
        {
            acceleration -= 0.01f;
        }

        else if (acceleration < accelerationCap && acceleration > -accelerationCap)
        {
            acceleration += 0.01f;
        }
    }
}
