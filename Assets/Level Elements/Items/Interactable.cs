using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour {
    
    public InteractEvent onInteract;

    void Start() {
        if (onInteract == null) onInteract = new InteractEvent();
    }

    public void Interact(Interactor interactor) {
        OnInteract(interactor);
        onInteract.Invoke(interactor);
    }
    public virtual void OnInteract(Interactor interactor) {
        Debug.LogError("OnInteract not implemented for " + name);
    }
}
