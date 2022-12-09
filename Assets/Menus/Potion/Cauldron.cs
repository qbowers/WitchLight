using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cauldron : MonoBehaviour {
    // config inspector values
    public int validDropDistance;
    public Transform potionShelf;
    public CraftedPotion potionPrefab;
    public Button stirButton;
    public Button trashButton;
    public IngredientShelf ingredientShelf;
    public BrewingManager brewingManager;
    public bool refundTrashedIngredients;
    // when the player should be stirring but isn't, how fast do they lose progress?
    // higher number = more punishing
    public float unstirRatio;
    public List<Recipe> recipes;

    // adding ingredients
    private List<ItemType> addedIngredients;
    private Recipe currentRecipe;

    // stirring
    private bool canStir = false;
    private bool isStirring = false;
    private float stirTime = 0;

    public void Start() {
        addedIngredients = new List<ItemType>();
        stirButton.gameObject.SetActive(false);
        trashButton.gameObject.SetActive(false);
    }

    // called by a DraggableIngredient
    // returns whether it was successfully dropped (ie is close enough)
    public bool DropIngredient(DraggableIngredient ingredient) {
        // Debug.Log("DropIngredient called");
        if ((ingredient.transform.position - transform.position).magnitude < validDropDistance) {
            addedIngredients.Add(ingredient.ingredientType);
            // move the ingredient to the cauldron and make it not draggable by destroying the DraggableIngredient script
            ingredient.transform.SetParent(transform);
            ingredient.transform.SetSiblingIndex(0); // go under the stir button
            Destroy(ingredient);
            // Destroy(ingredient.gameObject);
            // Debug.Log(string.Join(", ", addedIngredients));

            trashButton.gameObject.SetActive(true);

            CheckAgainstRecipes();
            return true;
        }
        return false;
    }

    private void CheckAgainstRecipes() {
        foreach (Recipe r in recipes) {
            if (addedIngredients.SequenceEqual(r.ingredients)) {
                // Debug.Log("Recipe made! Ready to stir " + r.potionName);
                stirButton.image.color = r.potionColor;
                stirButton.gameObject.SetActive(true);
                currentRecipe = r;

                canStir = true;
                isStirring = false;
                stirTime = 0;
                return;
            }
        }
        stirButton.gameObject.SetActive(false);

        canStir = false;
        isStirring = false;
        stirTime = 0;
    }

    public void TrashButtonOnClick() {
        if (refundTrashedIngredients) {
            ingredientShelf.RefundIngredients(this, addedIngredients);
        }
        addedIngredients.Clear();

        // destroy ingredients used
        foreach (Transform child in transform) {
            // if not a button, destroy
            if (child.GetComponent<Button>() == null) Destroy(child.gameObject);
        }

        stirButton.gameObject.SetActive(false);
        trashButton.gameObject.SetActive(false);

        canStir = false;
        isStirring = false;
        stirTime = 0;
    }

    public void StirButtonOnMouseDown(BaseEventData basedata) {
        if (canStir) {
            isStirring = true;
        }
    }

    public void StirButtonOnMouseUpOrExit(BaseEventData basedata) {
        isStirring = false;
    }

    public void Update() {
        if (canStir) {
            // add stir time if applicable
            if (isStirring) {
                stirTime += Time.deltaTime;
            } else {
                stirTime -= Time.deltaTime * unstirRatio;
                if (stirTime < 0) stirTime = 0;
            }

            // fill graphics
            stirButton.image.fillAmount = (stirTime / currentRecipe.successfulStirTime);

            // check if we've been stirring for long enough
            if (stirTime >= currentRecipe.successfulStirTime) {
                // Debug.Log("Finished stirring!");

                brewingManager.CreatePotion(currentRecipe);
                
                // reset
                addedIngredients.Clear();
                stirButton.gameObject.SetActive(false);
                trashButton.gameObject.SetActive(false);

                // destroy ingredients used
                foreach (Transform child in transform) {
                    // if not a button, destroy
                    if (child.GetComponent<Button>() == null) Destroy(child.gameObject);
                }

                canStir = false;
                isStirring = false;
                stirTime = 0;
            }
        }
    }
}
