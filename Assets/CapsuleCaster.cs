using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleCast : MonoBehaviour
{
    public GameObject currentHitObject;

    public Vector3 pt1;
    public Vector3 pt2;
    public float radius;
    public float maxDist = 20;
    public LayerMask layerMask; 

    private Vector3 direction;

    private float currentHitDist;

    private bool checkRight; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pt1 = new Vector3(transform.position.x, transform.position.y+radius, transform.position.z);
        pt2 = new Vector3(transform.position.x, transform.position.y - radius, transform.position.z);
        direction = transform.forward;
        RaycastHit hit;

        float angle = 1;
        var dir = new Vector3(Mathf.Sin(1), 0, Mathf.Cos(1));
        var right_dir = direction;
        var left_dir = direction;


        int count = 0;

        if (Physics.CapsuleCast(pt1, pt2, radius, direction, out hit, maxDist, layerMask, QueryTriggerInteraction.UseGlobal)) 
        {
            currentHitObject = hit.transform.gameObject;
            currentHitDist = hit.distance;
            Debug.Log("was hit at: " + direction);
        }
        else
        {
            Debug.Log("No object directly in front");

            while(!Physics.CapsuleCast(pt1, pt2, radius, direction, out hit, maxDist, layerMask, QueryTriggerInteraction.UseGlobal))
            {
                count += 1;
                if (checkRight)
                {
                    right_dir += dir;
                    Debug.Log("rirght dir: " + right_dir);
                    direction = right_dir;
                    Debug.Log("full left: " + direction);
                    checkRight = false;
                }
                else
                {
                    left_dir -= dir;
                    Debug.Log("left dir: " + left_dir);
                    direction = left_dir;
                    Debug.Log("full left: " + direction);
                    checkRight = true;
                }



                //Debug.Log("Running new angles: " + checkRight + " " + direction);
                //Mathf.Clamp(direction.x, -90f, 90f);
                //Mathf.Clamp(direction.y, -90f, 90f);

                if (Physics.CapsuleCast(pt1, pt2, radius, direction, out hit, maxDist, layerMask, QueryTriggerInteraction.UseGlobal))
                {
                    currentHitObject = hit.transform.gameObject;
                    currentHitDist = hit.distance;
                    break;
                    //Debug.Log(direction + " was hit ");
                }

                if(count == 6)
                {
                    break;
                }
                
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(pt1, pt1 + direction * currentHitDist);
        Debug.DrawLine(pt2, pt2 + direction * currentHitDist);

        Gizmos.DrawWireSphere(pt1 + direction * currentHitDist, radius);
        Gizmos.DrawWireSphere(pt2 + direction * currentHitDist, radius);
    }
}
