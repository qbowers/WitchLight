using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GhostMonsterMovement : MonoBehaviour {
    public float speed = 1.0f;
    
    public Transform player;

    void FixedUpdate() {
        var playerPosition = player.position;

        var step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, playerPosition, step);
    }
}
