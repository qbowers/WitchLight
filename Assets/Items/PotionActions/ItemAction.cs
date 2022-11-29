using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemAction : MonoBehaviour {
    public UDictionary<ItemType, int> costs;
    public string bindingName;

    protected CharacterMovement controller;
    void Awake() {
        controller = GetComponent<CharacterMovement>();
    }

    public bool cost(Inventory inv) {
        foreach(var item in costs) {
            ItemType costName = item.Key;
            int costCnt = item.Value;
            Debug.Log("Action Cost:" + costName.ToString() + " " + costCnt);
            if(!inv.enough(costName, costCnt)){
                return false;
            }
        }
        inv.actionCosts(costs);
        return true;
    }

    public abstract void perform();
}
