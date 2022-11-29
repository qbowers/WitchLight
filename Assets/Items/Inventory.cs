using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour {
    public UDictionary<string, int> inv = new UDictionary<string, int>();
    public UDictionary<string, TextMeshProUGUI> invUI = new UDictionary<string, TextMeshProUGUI>();

    public int getItemCnt(string item) {
        if (inv.ContainsKey(item)) {
            return inv[item];
        }
        return 0;
    }

    public void collectInv(Collectable item) {
        
        int cnt = getItemCnt(item.id);
        inv[item.id] = cnt + item.cnt;
        Debug.Log(inv[item.id]+ " " + item.id + " in inv");
        changeInvUI(item.id);
    
    }

    public bool enough(string costName, int costCnt) {
        if (costCnt == 0){
            return true;
        }
        if(inv.ContainsKey(costName) && inv[costName] >= costCnt){
            return true;
        }
        return false;
    }

    public void actionCosts(UDictionary<string, int> costs){
        foreach(var item in costs){
            string costName = item.Key;
            int costCnt = item.Value;
            if (costCnt != 0) {
                inv[costName] -= costCnt;
                changeInvUI(costName);
            }
        }
    }

    public void changeInvUI(string id) {
        if (invUI.ContainsKey(id)) {
                //increase txt cnt
                invUI[id].text = inv[id].ToString();
        }
    }
}
