using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientShelf : MonoBehaviour
{
    public DraggableIngredient ingredientPrefab;
    
    private GridLayoutGroup grid;

    // Start is called before the first frame update
    public void Start()
    {
        grid = GetComponent<GridLayoutGroup>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        grid.enabled = false;
    }

    public void RefundIngredients(Cauldron cauldron, List<Ingredient> ingredients)
    {
        foreach(Ingredient i in ingredients)
        {
            DraggableIngredient ingredient = Instantiate(ingredientPrefab, transform);
            ingredient.cauldron = cauldron;
            ingredient.ingredientName = i;
        }
        // force layout rebuild
        grid.enabled = true;
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
        grid.enabled = false;
    }
}
