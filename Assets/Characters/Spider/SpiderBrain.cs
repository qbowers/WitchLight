using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBrain : MonoBehaviour
{
    public Transform rayPoint;
    public float groundCheckDistance;
    public LayerMask groundLayer;
    public float yOffest;
    public Transform[] legTargets;
    public float speed;
    public float currspeed;
    public float offsetTimer;
    bool decrease;
    [Range(0f, 0.5f)]
    public float breatheHeight;
    public float breatheSpeed;
    public LegMover[] legs;
    public bool moveLegs = true;
    // Start is called before the first frame update
    void Start()
    {
        // PUT THIS ON THE BODY BONE
    }

    // Update is called once per frame
    void Update()
    {
        CalculateGround();
        Idle();
    }

    public void CalculateGround()
    {
        float offset = yOffest;
        // if (currspeed == 0)
        // {
        //     offset = ;
        // }
        // else
        // {
             
        // }
        RaycastHit2D hit = Physics2D.Raycast(rayPoint.position, Vector3.down, groundCheckDistance, groundLayer);
        if (hit.collider != null && moveLegs)
        {
            Vector3 point = Vector3.zero;
            for (int i = 0; i < legTargets.Length; i++)
            {
                point += legTargets[i].position; // gets all the legtargets positions
            }
            
            point.y = point.y / legTargets.Length;
            point.y += offset;
            transform.position = new Vector3(transform.position.x, point.y, transform.position.z);
        }


    }

    public void Idle()
    {
        if (offsetTimer < breatheHeight && decrease == false)
        {
            offsetTimer += Time.deltaTime * (breatheSpeed * 0.1f);
        }


        else if (offsetTimer > breatheHeight)
        {
            decrease = true;
        }

        if (offsetTimer > -breatheHeight && decrease == true)
        {
            offsetTimer -= Time.deltaTime * (breatheSpeed * 0.1f);
        }
        else if (offsetTimer < -breatheHeight && decrease == true)
        {

            decrease = false;
        }
    }
}