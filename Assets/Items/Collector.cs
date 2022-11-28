using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour {
    public Inventory inventory;
    void Awake() {
        if (inventory == null) {
            this.inventory = GetComponent<Inventory>();
        } 
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        Collectable item = collision.GetComponent<Collectable>();
        
        if (item) {
            Destroy(collision.gameObject);
            inventory.collectInv(item);
        }
    }
}
