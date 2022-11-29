using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientShelf : MonoBehaviour {
    public DraggableIngredient ingredientPrefab;
    
    private GridLayoutGroup grid;

    public void RefundIngredients(Cauldron cauldron, List<IngredientType> ingredients) {
        foreach(IngredientType ingredient in ingredients) {
            DraggableIngredient ingredientObject = Instantiate(ingredientPrefab, transform);
            ingredientObject.cauldron = cauldron;
            ingredientObject.ingredientType = ingredient;
        }
        // force layout rebuild
        grid.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        grid.enabled = false;
    }
}
