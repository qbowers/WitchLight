using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class PotionSceneTrigger : Interactable {
    public string level = "PotionBrewing";
    // private bool isColliding = false;

    public override void OnInteract(Interactor interactor) {
        CoreManager.instance.OpenPotionScreen();
    }
    // void Start() {
    //     CoreManager.instance.playerMap.Interact.performed += OnInteractPerformed;
    // }

    // void OnDestroy() {
    //     CoreManager.instance.playerMap.Interact.performed -= OnInteractPerformed;
    // }

    // public void OnTriggerEnter2D(Collider2D other) {
        
    //     if (!Utils.IsPlayer(other.gameObject)) {
    //         return;
    //     }
    //     isColliding = true;
    // }

    // public void OnTriggerExit2D(Collider2D other) {
    //     if (!Utils.IsPlayer(other.gameObject)) {
    //         return;
    //     }
    //     isColliding = false;
    // }

    // private void OnInteractPerformed(InputAction.CallbackContext context) {
    //     // Debug.Log("Interaction Performed");
    //     if (isColliding) {
    //         CoreManager.instance.OpenPotionScreen();
    //     }
    // }

    
}
