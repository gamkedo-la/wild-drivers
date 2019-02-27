using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarNodeHandling : MonoBehaviour    
{
    private int playerNumber;
    private Quaternion rightRotationToFace;
    private int degreesToFail = 90; // at which point it is considered to be facing wrong direction.
    [SerializeField]private List<GameObject> nodes;
    [SerializeField] private int nextNode;
    [SerializeField]private int currentNode;
    public int currentLap = 1;
    public int maxLap = 3;
    // Start is called before the first frame update
    void Start()
    {
        playerNumber = gameObject.GetComponent<CarDrive>().playerNumber;
        for (int i = 0; i < GameObject.FindGameObjectWithTag("NodeList").transform.childCount; i++)
        {
            nodes.Add(GameObject.FindGameObjectWithTag("NodeList").transform.GetChild(i).gameObject);
        }
        nextNode = 0;
        currentNode = nodes.Count -1;
        Vector3 relativePosBetweenNodes = nodes[nextNode].transform.position - nodes[currentNode].transform.position;
        rightRotationToFace = Quaternion.LookRotation(relativePosBetweenNodes);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenPlayerandNode = Vector3.Distance(nodes[nextNode].transform.position, transform.position);

        if (distanceBetweenPlayerandNode < 15 && nextNode != nodes.Count -1)
        {
             changeNode();
        }

        //Debug.Log(rightRotationToFace.eulerAngles.y + " righttoface - transform    " + transform.rotation.eulerAngles.y + " playerNumber : " + playerNumber);
        float degreesToFailPlus = rightRotationToFace.eulerAngles.y + degreesToFail;
        float degreesToFailMinus = rightRotationToFace.eulerAngles.y - degreesToFail;
        degreesToFailMinus = degreesToFailMinus - 360 * Mathf.Floor(degreesToFailMinus / 360);
        degreesToFailPlus = degreesToFailPlus - 360 * Mathf.Floor(degreesToFailPlus / 360);
        //Debug.Log((degreesToFailMinus) + " fail - plus   " + (degreesToFailPlus) + " playernumber :  " + playerNumber);
        if (degreesToFailMinus > degreesToFailPlus)
        {
            if (transform.rotation.eulerAngles.y > degreesToFailPlus && transform.rotation.eulerAngles.y < degreesToFailMinus)
            {
                Debug.Log("Player " + playerNumber + " is facing the wrong direction");
            }
        }
        else
        {
            if (!(transform.rotation.eulerAngles.y > degreesToFailMinus && transform.rotation.eulerAngles.y < degreesToFailPlus))
            {
                Debug.Log("Player " + playerNumber + " is facing the wrong direction");
            }
        }
    }
    private void changeNode()
    {
        currentNode = nextNode;
        nextNode += 1;
        Vector3 relativePosBetweenNodes = nodes[nextNode].transform.position - nodes[currentNode].transform.position;
        rightRotationToFace = Quaternion.LookRotation(relativePosBetweenNodes);
        //Debug.Log(rightRotationToFace.y + 180);
    }
    private void OnTriggerEnter(Collider finishLine)
    {
        if (finishLine.CompareTag("FinishLine") && nextNode == nodes.Count -1)
        {
            currentNode = nodes.Count - 1;
            nextNode = 0;
            Vector3 relativePosBetweenNodes = nodes[nextNode].transform.position - nodes[currentNode].transform.position;
            rightRotationToFace = Quaternion.LookRotation(relativePosBetweenNodes);
            if (currentLap < maxLap)
            {
                currentLap += 1;
            }
            else if (currentLap == maxLap)
            {
                Debug.Log(gameObject.name + " has won the game");
            }
        }
    }
}
