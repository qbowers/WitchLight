using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovement : MonoBehaviour {
    
    public bool alwaysDetect;
    public float detectionRange;
    public float speed;

    private Vector3 towardsPos;
    public bool detected = false;

    // Start is called before the first frame update
    void Update() {
        if (!alwaysDetect){
            var hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRange);
            detected = false;
            foreach (var collider in hitColliders) {
                float minDist = 99999f;
                if(collider.CompareTag("Player") | collider.CompareTag("Distraction")){
                    float dist = Vector3.Distance(transform.position, collider.transform.position);
                    if (dist < minDist){
                        minDist = dist;
                        towardsPos = collider.transform.position;
                    }
                    detected = true;
                }
            }
        }
        else {
            detected = true;
            towardsPos = GameObject.Find("Player").transform.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (detected){
            float currspeed = speed * (GameObject.Find("Player").transform.position.x > transform.position.x ? 1:-1);
            transform.position = new Vector3(transform.position.x + currspeed * Time.deltaTime, transform.position.y, transform.position.z);
        }
    }
}
