using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Collector : MonoBehaviour {
    private Inventory inventory;
    private Collectable item;
    public bool autocollect;
    public AudioSource aud;
    
    void Start() {
        this.inventory = CoreManager.instance.inventory;
        CoreManager.instance.playerMap.Interact.performed += OnInteractPerformed;
    }

    void OnDestroy() {
        CoreManager.instance.playerMap.Interact.performed -= OnInteractPerformed;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        item = collision.GetComponent<Collectable>();
        if (autocollect && item) {
            inventory.collectInv(item);
            Destroy(item.gameObject);
            item = null;
            aud.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        item = null;
    }

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        if (item) {
            inventory.collectInv(item);
            Destroy(item.gameObject);
            item = null;
        }
    }
}
