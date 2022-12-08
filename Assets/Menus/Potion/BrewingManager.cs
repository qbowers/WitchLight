using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class BrewingManager : MonoBehaviour {
    public Cauldron cauldron;
    public Transform ingredientShelf;
    public Transform potionShelf;
    public int shelfCapacity;

    public DraggableIngredient ingredientPrefab;
    public GameObject potionPrefab;

    public MenuManager menuManager;

    Inventory inventory;
    // Start is called before the first frame update
    void Start() {

        CoreManager.instance.playerMap.Escape.performed += OnEscapePerformed;

        inventory = CoreManager.instance.inventory;

        foreach(var ingredient in inventory.invIng) {
            int num = inventory.getItemCnt(ingredient.Key);
            
            for (int i = 0; i < num && i < shelfCapacity; i++) {
                DraggableIngredient ingredientObject = Instantiate(ingredientPrefab, ingredientShelf);

                ingredientObject.cauldron = cauldron;
                ingredientObject.ingredientType = ingredient.Key;
                ingredientObject.GetComponent<Image>().sprite = ingredient.Value.image;
            }
        }

        foreach(var potion in inventory.invPot) {
            int num = inventory.getItemCnt(potion.Key);
            for (int i = 0; i < num && i < shelfCapacity; i++) {
                GameObject potionObject = Instantiate(potionPrefab, potionShelf);
                potionObject.GetComponent<Image>().sprite = potion.Value.image;
            }
        }
        FormatShelf(ingredientShelf);
        FormatShelf(potionShelf);
    }

    private void OnDestroy() {
        CoreManager.instance.playerMap.Escape.performed -= OnEscapePerformed;
    }

    private void OnEscapePerformed(InputAction.CallbackContext context) {
        menuManager.Resume();
    }


    public void CreatePotion(Recipe recipe) {

        // lose ingredients
        foreach (ItemType ingredient in recipe.ingredients) {
            inventory.invIng[ingredient].count--;
        }
        
        // gain potion
        for (int i = 0; i < recipe.numPotionsMade; i++) {
            GameObject potionObject = Instantiate(potionPrefab, potionShelf);
            potionObject.GetComponent<Image>().sprite = inventory.invPot[recipe.potionName].image;
            inventory.invPot[recipe.potionName].count++;
        }
        FormatShelf(potionShelf);
    }

    void FormatShelf(Transform transform) {
        GridLayoutGroup grid = transform.gameObject.GetComponent<GridLayoutGroup>();
        grid.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        grid.enabled = false;
    }
}
