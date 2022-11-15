using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    public int validDropDistance;
    public Transform potionShelf;
    public CraftedPotion potionPrefab;
    private List<Ingredient> addedIngredients;

    public List<Recipe> recipes;

    public void Start()
    {
        addedIngredients = new List<Ingredient>();
    }

    // called by a DraggableIngredient
    // returns whether it was successfully dropped
    public bool DropIngredient(DraggableIngredient ingredient)
    {
        if ((ingredient.transform.position - transform.position).magnitude < validDropDistance)
        {
            addedIngredients.Add(ingredient.ingredientName);
            Destroy(ingredient.gameObject);
            Debug.Log(string.Join(", ", addedIngredients));
            CheckAgainstRecipes();
            return true;
        }
        return false;
    }

    private void CheckAgainstRecipes()
    {
        foreach (Recipe r in recipes)
        {
            if (addedIngredients.SequenceEqual(r.ingredients))
            {
                Debug.Log("Recipe made! Successfully brewed " + r.potionName);
                var newPotion = Instantiate(potionPrefab, potionShelf);
                newPotion.Set(r.potionName, r.potionColor);
                addedIngredients.Clear();
            }
        }
    }
}
