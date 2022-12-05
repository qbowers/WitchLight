using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable {
    public string nextLevel;
    public int doorNumber;
    public int targetDoorNumber;


    public override void Interact(Interactor interactor) {
        // if (!Utils.IsPlayer(other.gameObject)) return;

        if (nextLevel.Length == 0) return;

        Debug.Log("Door Opened");
        CoreManager.instance.LoadLevel(nextLevel, false, targetDoorNumber);
    }
}
