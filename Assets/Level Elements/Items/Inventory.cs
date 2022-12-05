using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor;
using TMPro;

public class Inventory : MonoBehaviour {

    [Serializable]
    public class InvItem {
        [Tooltip("Leave blank, this will be linked by InventoryPanel")]
        [NonSerialized] public TextMeshProUGUI textbox;

        public Sprite image;

        [SerializeField] private int _count = 0;
        public int count { 
            get { return _count; } 
            set { _count = value; UpdateText(); } // this automatically calls UpdateText any time the variable is set
        }

        public void UpdateText() {
            if (textbox == null) {
                Debug.Log("textbox does not exist");
            }
            textbox.text = count.ToString();
        }
    }

    public UDictionary<ItemType, InvItem> invIng = new UDictionary<ItemType, InvItem>();
    public UDictionary<ItemType, InvItem> invPot = new UDictionary<ItemType, InvItem>();

    public void ZeroInventory() {
        foreach(var ing in invIng) {
            ing.Value.count = 0;
        }
        foreach(var pot in invPot) {
            pot.Value.count = 0;
        }
    }

    public int getItemCnt(ItemType item) {
        if (invIng.ContainsKey(item)) {
            return invIng[item].count;
        }
        if (invPot.ContainsKey(item)) {
            return invPot[item].count;
        }
        return 0;
    }

    // called by Collector monobehaviour
    public void collectInv(Collectable item) {
        int cnt = getItemCnt(item.id);
        invIng[item.id].count = cnt + item.cnt;
        // Debug.Log(inv[item.id].count + " " + item.id + " in inv");    
    }

    // assumes only potions with checking enough
    public bool enough(ItemType costName, int costCnt) {
        if (costCnt == 0){
            return true;
        }
        if(invPot.ContainsKey(costName) && invPot[costName].count >= costCnt){
            return true;
        }
        return false;
    }

    // assumes only potions will be costs
    public void actionCosts(UDictionary<ItemType, int> costs){
        foreach(var item in costs){
            ItemType costName = item.Key;
            int costCnt = item.Value;
            if (costCnt != 0) {
                invPot[costName].count -= costCnt;
            }
        }
    }
}
