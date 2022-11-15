using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cauldron : MonoBehaviour
{
    // config inspector values
    public int validDropDistance;
    public Transform potionShelf;
    public CraftedPotion potionPrefab;
    public Transform spoon;
    public float spoonSpeed;
    public float spoonMagnitude;
    public List<Recipe> recipes;

    // adding ingredients
    private List<Ingredient> addedIngredients;
    private Recipe currentRecipe;

    // stirring
    private bool canStir = false;
    private float successfulStirTime;
    private float failedStirTime;
    private float stirStartTime;
    private Collider2D spoonCollider;

    public void Start()
    {
        addedIngredients = new List<Ingredient>();
        spoonCollider = spoon.GetComponent<Collider2D>();
    }

    // called by a DraggableIngredient
    // returns whether it was successfully dropped (ie is close enough)
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
                Debug.Log("Recipe made! Ready to stir " + r.potionName);
                currentRecipe = r;
                successfulStirTime = 0;
                failedStirTime = 0;
                stirStartTime = Time.time;
                canStir = true;
            }
        }
    }

    public void SpoonOnMouseDrag(BaseEventData basedata)
    {

    }

    public void Update()
    {
        if (canStir)
        {
            // move the spoon
            Vector3 newPosition = spoon.localPosition;
            newPosition.x = Mathf.Sin((Time.time - stirStartTime) * Mathf.PI * spoonSpeed) * spoonMagnitude;
            spoon.localPosition = newPosition;

            // check for mouse over spoon
            if (spoonCollider.OverlapPoint(Input.mousePosition))
            {
                Debug.Log("overlapping point");
                successfulStirTime += Time.deltaTime;
            }
            else
            {
                failedStirTime += Time.deltaTime;
            }

            // check if we've been stirring for long enough
            if (successfulStirTime >= currentRecipe.successfulStirTime)
            {
                Debug.Log("Finished stirring!");
                var newPotion = Instantiate(potionPrefab, potionShelf);
                newPotion.Set(currentRecipe.potionName, currentRecipe.potionColor);
                // reset
                addedIngredients.Clear();
                canStir = false;
            }
            // or we've failed long enough
            else if (failedStirTime >= currentRecipe.failedStirTime)
            {
                Debug.Log("Failed potion.");
                // TODO: report error to user better
                // reset
                addedIngredients.Clear();
                canStir = false;
            }
        }
    }

}
