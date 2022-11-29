using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class MonsterAI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target;

    public float speed = 20f;
    public float airSpeed = 40f;
    public float jumpForce = 20f;
    public float jumpConstraint = 0.95f;
    public Vector2 direction;
    private Vector2 velocity;
    public float jumpDelay = 0.05f;
    public float jumpDelayCounter = 0;
    public float nextWaypointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachEndOfPath = false;
    private float scaleDir;
    public bool onGround = true;
    private Vector2 directionPlayer;

    private Seeker seeker;
    private Rigidbody2D rb;
    private MonsterGround ground;
    private SpiderBrain spiderBrain;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<MonsterGround>();
        spiderBrain = GetComponent<SpiderBrain>();

        InvokeRepeating("UpdatePath", 0f, 1f);

    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    private void OnPathComplete(Path p) 
    {
        if (!p.error) 
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void MonsterMovement(Vector2 direction)
    {
        onGround = ground.GetOnGround();

        // Get velocity of monster
        velocity = rb.velocity;

        // Only creates force in x direction
        Vector2 forceX = direction * 50f;
        forceX.y = 0;

        if (onGround)
        {
            spiderBrain.moveLegs = true;
            rb.gravityScale = 0;
            if(Mathf.Abs(rb.velocity.x) < speed){
                rb.AddForce(forceX);
            }

            jumpDelayCounter += Time.deltaTime;

            if (direction.y > jumpConstraint && jumpDelayCounter > jumpDelay) 
            {
                spiderBrain.moveLegs = false;
                velocity.y = jumpForce;
                jumpDelayCounter = 0;
            }
        }
        else {
            rb.gravityScale = 4;
            if(Mathf.Abs(rb.velocity.x) < airSpeed){
                rb.AddForce(forceX);
            }
        }

        rb.velocity = velocity;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (path == null) 
        {
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachEndOfPath = true;
            return;
        } else
        {
            reachEndOfPath = false;
        }

        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        directionPlayer = ((Vector2)(target.position) - rb.position).normalized;
        Vector3 localScale = transform.localScale;
        if (directionPlayer.x < 0){
            if (localScale.x > 0){
                localScale.x *= -1;
            }
        }
        else {
            if (localScale.x < 0){
                localScale.x *= -1;
            }
        }
        transform.localScale = localScale;
        MonsterMovement(direction);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
