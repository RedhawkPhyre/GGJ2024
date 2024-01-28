using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleCast : MonoBehaviour
{

    public Transform orientation;

    public GameObject currentFrontObject;
    public GameObject leftObject;
    public GameObject rightObject;
    public Rigidbody rb;

    public Vector3 pt1;
    public Vector3 pt2;
    public float radius;
    public float maxDist = 5;
    public LayerMask layerMask;

    private Vector3 direction;
    private float currentHitDist;
    private Vector3 dir = new Vector3(Mathf.Sin(1), 0, Mathf.Cos(1));

    private float rotSpeed = 10f;
    private bool checkRight;
    private bool playerFound;
    private bool frontBlocked;
    private bool leftBlocked;
    private bool rightBlocked;
    private bool stillBlocked = true;



    // Start is called before the first frame update
    void Start()
    {
        direction = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {

        
        pt1 = new Vector3(transform.position.x, transform.position.y+radius, transform.position.z);
        pt2 = new Vector3(transform.position.x, transform.position.y - radius, transform.position.z);

        RaycastHit hit;

        Vector3 dir = new Vector3(Mathf.Sin(1), 0, Mathf.Cos(1));
        Vector3 right_dir = direction + dir;
        Vector3 left_dir = direction - dir;

        scanObstacles();
        if (frontBlocked)
        {

        }

        if (Physics.CapsuleCast(pt1, pt2, radius, left_dir, out hit, maxDist, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            
            leftObject = hit.transform.gameObject;

            //Debug.Log("Found: " + leftObject + " at: " + left_dir);
        }
        else
        {
            Debug.Log("left not blocked anymore, ended at: " + left_dir);
            leftObject = null;
            currentHitDist = maxDist;
            
        }

        if (Physics.CapsuleCast(pt1, pt2, radius, right_dir, out hit, maxDist, layerMask, QueryTriggerInteraction.UseGlobal))
        { 

            rightObject = hit.transform.gameObject;
            // Debug.Log("Found: " + rightObject + " at: " + right_dir);

        }
        else
        {
            Debug.Log("right not blocked anymore, ended at: " + right_dir);
            rightObject = null;
            currentHitDist = maxDist;

        }


    }

private void rotateRight() 

{

transform.Rotate(transform.up* Time.deltaTime* rotSpeed);

}

private void rotateLeft()

{

    transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);

}

private void scanObstacles()
    {
        RaycastHit hit;
        if (Physics.CapsuleCast(pt1, pt2, radius, direction, out hit, maxDist, layerMask, QueryTriggerInteraction.UseGlobal))
        {
            currentFrontObject = hit.transform.gameObject;
            currentHitDist = hit.distance;
            if (currentFrontObject == GameObject.Find("Player"))
            {
                Debug.Log("Player found at: " + direction);
                playerFound = true;
            }

            frontBlocked = true;
            
        }
    }
}
