using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PotionSceneTrigger : MonoBehaviour {
    public string level = "PotionBrewing";

    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.name != "Player") {
            return;
        }

        CoreManager.instance.levelManager.OpenPotionScreen();
    }
}
