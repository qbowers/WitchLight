using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryPanel : MonoBehaviour {
    public GameObject invItemPrefab;

    // Start is called before the first frame update
    void Start() {
        // clear children
        foreach(Transform child in transform) Destroy(child.gameObject);

        // create invItemPrefabs for each item in the inventory
        foreach(var invitem in CoreManager.instance.inventory.inv) {
            var newItem = Instantiate(invItemPrefab, transform);
            newItem.GetComponentInChildren<Image>().sprite = invitem.Value.image;
            // link textbox
            invitem.Value.textbox = newItem.GetComponentInChildren<TextMeshProUGUI>();
        }
    }
}
