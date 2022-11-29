using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
    private Inventory inventory;
    
    void Start() {
        this.inventory = CoreManager.instance.inventory; 
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Collectable item = collision.GetComponent<Collectable>();
        
        if (item) {
            Destroy(collision.gameObject);
            inventory.collectInv(item);
        }
    }
}
