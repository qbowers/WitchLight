using System;
using System.Collections.Generic;
using UnityEngine;

// file to hold statics for ingredients and recipies
[Serializable]
public struct Recipe {
    public string potionName;
    public Color potionColor;
    public IngredientType[] ingredients;
    public float successfulStirTime; // how long until you succeed?
}
