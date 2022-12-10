using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ItemAction : MonoBehaviour {
    public UDictionary<ItemType, int> costs;
    public string bindingName;
    public AudioSource aud;

    protected CharacterMovement controller;

    protected PlayerControls.PlayerActions playerMap;
    protected PlayerControls playerControls;
    protected Inventory inv;
    InputAction playerMapAction;
    
    void Awake() {
        controller = GetComponent<CharacterMovement>();


        inv = CoreManager.instance.inventory;
        playerControls = CoreManager.instance.playerControls;
        playerMap = playerControls.Player;

        playerMapAction = playerControls.FindAction(bindingName, false);
        playerMapAction.performed += OnPerform;
        playerMapAction.started += OnStart;
        playerMapAction.canceled += OnCancel;
    }

    void OnDestroy() {
        playerMapAction.performed -= OnPerform;
        playerMapAction.started -= OnStart;
        playerMapAction.canceled -= OnCancel;
    }

    void OnStart(InputAction.CallbackContext context) {
        Debug.Log("Started");
        if (!costAvailable()) {
            Debug.Log("Not Enough Cost");
            DisableAction();
            EnableAction();
        } else {
            // Debug.Log("Enough Cost");
            // make pretty things happen
        }
    }
    void OnPerform(InputAction.CallbackContext context) {
        // Debug.Log($"Performed with Interaction: {context.interaction}");

        if (costAvailable()) {
            // Remove required items from inventory
            inv.actionCosts(costs);
            perform();
        }
    }
    void OnCancel(InputAction.CallbackContext context) {
        // Stop pretty things
        // Debug.Log("Cancelled!!");
    }


    public void DisableAction() {
        // Debug.Log("Action Disabled");
        playerMapAction.Disable();
    }

    public void EnableAction() {
        // Debug.Log("Action Enabled");
        playerMapAction.Enable();
    }
    
    public bool costAvailable() {
        // Ensure there is enough of the item in the inventory
        foreach(var item in costs) {
            ItemType costName = item.Key;
            int costCnt = item.Value;
            // Debug.Log("Action Cost:" + costName.ToString() + " " + costCnt);
            if(!inv.enough(costName, costCnt)){
                return false;
            }
        }

        // Remove required items from inventory
        inv.actionCosts(costs);
        aud.Play();
        return true;
    }

    public abstract void perform();
}
