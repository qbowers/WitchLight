using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour {
    // Keep a queue of all things to interact with
    List<Interactable> interactionQueue = new List<Interactable>(); 

    // Manage Callbacks
    void Start() {
        CoreManager.instance.playerMap.Interact.performed += doInteract;
    }
    void OnDestroy() {
        CoreManager.instance.playerMap.Interact.performed -= doInteract;
    }

    // Manage interactable queue
    void OnTriggerEnter2D(Collider2D other) {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null) interactionQueue.Add(interactable);
    }

    void OnTriggerExit2D(Collider2D other) {
        Interactable interactable = other.GetComponent<Interactable>();
        if (interactable != null) interactionQueue.Remove(interactable);
    }

    // Interaction callback! Handoff to interactable delegate
    void doInteract(InputAction.CallbackContext context) {
        
        Debug.Log("Interactor!");
        Debug.Log(gameObject.name);
        Debug.Log(interactionQueue.Count);
        if (interactionQueue.Count != 0) {
            interactionQueue[0].onInteract.Invoke(this);
        }
    }
}
