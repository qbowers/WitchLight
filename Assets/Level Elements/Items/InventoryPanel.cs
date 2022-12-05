using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryPanel : MonoBehaviour {
    public GameObject invItemPrefab;

    // Start is called before the first frame update
    void Start() {
        // clear children
        Debug.Log("Create Textboxes");

        foreach(Transform child in transform) Destroy(child.gameObject);

        // create invItemPrefabs for each item in the inventory
        foreach(var invitem in CoreManager.instance.inventory.invIng) {
            var newItem = Instantiate(invItemPrefab, transform);
            newItem.GetComponentInChildren<Image>().sprite = invitem.Value.image;
            // link textbox
            invitem.Value.textbox = newItem.GetComponentInChildren<TextMeshProUGUI>();
            // show text
            invitem.Value.UpdateText();
        }
        // potion inventory
        foreach(var invitem in CoreManager.instance.inventory.invPot) {
            var newItem = Instantiate(invItemPrefab, transform);
            newItem.GetComponentInChildren<Image>().sprite = invitem.Value.image;
            // link textbox
            invitem.Value.textbox = newItem.GetComponentInChildren<TextMeshProUGUI>();
            // show text
            invitem.Value.UpdateText();
        }
    }
}
