using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    public string nextLevel;
    public int doorNumber;
    public int targetDoorNumber;


    void OnTriggerEnter2D(Collider2D other) {
        if (!Utils.IsPlayer(other.gameObject)) {
            return;
        }

        if (nextLevel.Length == 0) {
            return;
        }

        Debug.Log("Door Opened");
        CoreManager.instance.LoadLevel(nextLevel, false, targetDoorNumber);
    }
}
