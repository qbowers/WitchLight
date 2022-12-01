using System;
using System.Collections.Generic;
using UnityEngine;

// file to hold statics for recipies
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Recipe", order = 1)]
public class Recipe : ScriptableObject {
    public ItemType potionName;
    public int numPotionsMade = 1;
    public Color potionColor;
    public ItemType[] ingredients;
    public float successfulStirTime; // how long until you succeed?

    public void OnValidate()
    {
        if (successfulStirTime <= 0) Debug.LogWarning("Stir time for potion " + potionName.ToString() + " is too small");
    }
}
