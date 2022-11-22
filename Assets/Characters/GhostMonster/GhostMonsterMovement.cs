using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMonsterMovement : MonoBehaviour {
    public float speed = 1.0f;
    public Vector3 initialPosition = new Vector3(0.0f, 0.0f, 0.0f);

    void FixedUpdate() {
        var playerPosition = GameObject.Find("Player").transform.position;

        var step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
    }
}
