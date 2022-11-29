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


        // Get number of blue flowers from the inventory
        int numFlowers = CoreManager.instance.inventory.getItemCnt("Flower");
        // int numFlowers = 2;

        // add this many flowers to scene
        for (int i = 0; i < numFlowers; i++) {
            DraggableIngredient ingredientObject = Instantiate(ingredientPrefab, ingredientShelf);

            ingredientObject.cauldron = cauldron;
            ingredientObject.ingredientType = IngredientType.BLUE_FLOWER;
        }

        // same for potions
        int numPotions = CoreManager.instance.inventory.getItemCnt("Potion");
        // int numPotions = 2;
        for (int i = 0; i < numPotions; i++) {
            Instantiate(potionPrefab, potionShelf);
        }

        FormatShelf(ingredientShelf);
        // FormatShelf(potionShelf);

    }


    public void CreatePotion(PotionType potion) {
        Instantiate(potionPrefab, potionShelf);
        
        int prevPotions = CoreManager.instance.inventory.getItemCnt("Potion");
        CoreManager.instance.inventory.inv["Potion"] = prevPotions + 1;
    }

    void FormatShelf(Transform transform) {
        GridLayoutGroup grid = transform.gameObject.GetComponent<GridLayoutGroup>();
        grid.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        grid.enabled = false;
    }
}
