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
            textbox.text = count.ToString();
        }
    }

    public UDictionary<ItemType, InvItem> inv = new UDictionary<ItemType, InvItem>();

    public int getItemCnt(ItemType item) {
        if (inv.ContainsKey(item)) {
            return inv[item].count;
        }
        return 0;
    }

    // called by Collector monobehaviour
    public void collectInv(Collectable item) {
        int cnt = getItemCnt(item.id);
        inv[item.id].count = cnt + item.cnt;
        Debug.Log(inv[item.id].count + " " + item.id + " in inv");    
    }

    public bool enough(ItemType costName, int costCnt) {
        if (costCnt == 0){
            return true;
        }
        if(inv.ContainsKey(costName) && inv[costName].count >= costCnt){
            return true;
        }
        return false;
    }

    public void actionCosts(UDictionary<ItemType, int> costs){
        foreach(var item in costs){
            ItemType costName = item.Key;
            int costCnt = item.Value;
            if (costCnt != 0) {
                inv[costName].count -= costCnt;
            }
        }
    }
}
