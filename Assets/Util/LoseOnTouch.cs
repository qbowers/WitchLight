using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOnTouch : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (!Utils.IsPlayer(other.gameObject) || other.gameObject.GetComponent<CharacterMovement>().isImmune) {
            return;
        }
        
        // zero out inventory
        CoreManager.instance.inventory.ZeroInventory();

        CoreManager.instance.LoadMenu(Constants.GameOverMenuScene, LoadSceneMode.Single);
    }
}
