using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PotionSceneTrigger : MonoBehaviour {
    public string level = "PotionBrewing";
    private bool isColliding = false;

    void Start() {
        CoreManager.instance.playerMap.Interact.performed += OnInteractPerformed;
    }

    void OnDestroy() {
        CoreManager.instance.playerMap.Interact.performed -= OnInteractPerformed;
    }

    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.name != "Player") {
            return;
        }
        isColliding = true;
    }

    public void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.name != "Player") {
            return;
        }
        isColliding = false;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (isColliding) {
            CoreManager.instance.OpenPotionScreen();
        }
    }
    
}
