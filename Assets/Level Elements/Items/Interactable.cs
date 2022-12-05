using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Interactable : MonoBehaviour {
    // Start is called before the first frame update
    [Serializable] public class InteractEvent : UnityEvent<Interactor> {}
    public InteractEvent onInteract;

    void Start() {
        if (onInteract == null) onInteract = new InteractEvent();
        onInteract.AddListener(Interact);
    }

    public abstract void Interact(Interactor interactor);
}
