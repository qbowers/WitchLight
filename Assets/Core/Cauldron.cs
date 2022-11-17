using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cauldron : MonoBehaviour
{
    // config inspector values
    public int validDropDistance;
    public Transform potionShelf;
    public CraftedPotion potionPrefab;
    public Button stirButton;
    // when the player should be stirring but isn't, how fast do they lose progress?
    // higher number = more punishing
    public float unstirRatio;
    public List<Recipe> recipes;

    // adding ingredients
    private List<Ingredient> addedIngredients;
    private Recipe currentRecipe;

    // stirring
    private bool canStir = false;
    private bool isStirring = false;
    private float stirTime = 0;

    public void Start()
    {
        addedIngredients = new List<Ingredient>();
        if (stirButton == null) stirButton = transform.GetComponentInChildren<Button>();
        stirButton.gameObject.SetActive(false);
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
                stirButton.gameObject.SetActive(true);
                currentRecipe = r;
                stirTime = 0;
                stirButton.image.fillAmount = 0;
                canStir = true;
                isStirring = false;
            }
        }
    }

    public void StirButtonOnMouseDown(BaseEventData basedata)
    {
        if (canStir) 
        {
            isStirring = true;
        }
    }

    public void StirButtonOnMouseUpOrExit(BaseEventData basedata)
    {
        isStirring = false;
    }

    public void Update()
    {
        if (canStir)
        {
            // add stir time if applicable
            if (isStirring)
            {
                stirTime += Time.deltaTime;
            }
            else
            {
                stirTime -= Time.deltaTime * unstirRatio;
                if (stirTime < 0) stirTime = 0;
            }

            // fill graphics
            stirButton.image.fillAmount = (stirTime / currentRecipe.successfulStirTime);

            // check if we've been stirring for long enough
            if (stirTime >= currentRecipe.successfulStirTime)
            {
                Debug.Log("Finished stirring!");
                var newPotion = Instantiate(potionPrefab, potionShelf);
                newPotion.Set(currentRecipe.potionName, currentRecipe.potionColor);
                // reset
                addedIngredients.Clear();
                stirButton.gameObject.SetActive(false);
                canStir = false;
                isStirring = false;
            }
        }
    }

}
