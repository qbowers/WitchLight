using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrewingManager : MonoBehaviour {
    public Cauldron cauldron;
    public Transform ingredientShelf;
    public Transform potionShelf;

    public DraggableIngredient ingredientPrefab;
    public GameObject potionPrefab;
    // Start is called before the first frame update
    void Start() {

        // TODO: generalize this in some nice loop through all inventory objects or smth

        // Get number of blue flowers from the inventory
        int numFlowers = CoreManager.instance.inventory.getItemCnt(ItemType.BLUE_FLOWER);
        // int numFlowers = 2;

        // add this many flowers to scene
        for (int i = 0; i < numFlowers; i++) {
            DraggableIngredient ingredientObject = Instantiate(ingredientPrefab, ingredientShelf);

            ingredientObject.cauldron = cauldron;
            ingredientObject.ingredientType = ItemType.BLUE_FLOWER;
        }

        // same for potions
        int numPotions = CoreManager.instance.inventory.getItemCnt(ItemType.DOUBLE_JUMP);

        for (int i = 0; i < numPotions; i++) {
            Instantiate(potionPrefab, potionShelf);
            // TODO: set data for potion (once we have >1 potion)
        }

        FormatShelf(ingredientShelf);
        // FormatShelf(potionShelf);
    }


    public void CreatePotion(Recipe recipe) {
        var inventory = CoreManager.instance.inventory; // readability

        // lose ingredients
        foreach (ItemType ingredient in recipe.ingredients)
        {
            inventory.inv[ingredient].count--;
        }
        
        // gain potion
        Instantiate(potionPrefab, potionShelf);
        
        // int prevPotions = inventory.getItemCnt(recipe.potionName);
        inventory.inv[recipe.potionName].count++;// = prevPotions + 1;
    }

    void FormatShelf(Transform transform) {
        GridLayoutGroup grid = transform.gameObject.GetComponent<GridLayoutGroup>();
        grid.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        grid.enabled = false;
    }
}
