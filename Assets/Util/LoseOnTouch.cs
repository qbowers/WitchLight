using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOnTouch : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name != "Player" || other.gameObject.GetComponent<CharacterMovement>().isImmune) {
            return;
        }

        CoreManager.instance.LoadMenu(Constants.GameOverMenuScene, LoadSceneMode.Single);
    }
}
