using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemActionsController : MonoBehaviour {

    private PlayerControls.PlayerActions playerMap;
    private PlayerControls playerControls;
    private Inventory inv;
    public ItemAction[] actions;
    private Action<InputAction.CallbackContext>[] callbacks;

    void Start() {
        inv = CoreManager.instance.inventory;
        playerControls = CoreManager.instance.playerControls;
        playerMap = playerControls.Player;

        callbacks = new Action<InputAction.CallbackContext>[actions.Length];
        
        for (int i = 0; i < actions.Length; i++) {
            ItemAction action = actions[i];
            InputAction playerMapAction = playerControls.FindAction(action.bindingName, false);
            callbacks[i] = formatActionFunc(action);
            playerMapAction.performed += callbacks[i];
        }
    }

    Action<InputAction.CallbackContext> formatActionFunc(ItemAction action) {
        return (context) => {
            // Debug.Log(action.GetType().Name);
            if (action.cost(inv)) {
                action.perform();
            }
        };
    }

    void OnDestroy()
    {
        for (int i = 0; i < actions.Length; i++) {
            ItemAction action = actions[i];
            InputAction playerMapAction = playerControls.FindAction(action.bindingName, false);
            playerMapAction.performed -= callbacks[i];
        }
    }
}
