using System;
using System.Collections.Generic;
using UnityEngine;

// file to hold statics for ingredients and recipies
[Serializable]
public struct Recipe
{
    public string potionName;
    public Color potionColor;
    public Ingredient[] ingredients;
    public float successfulStirTime; // how long on the spoon until you succeed?
    public float failedStirTime; // how long off the spoon until you fail?
}

public enum Ingredient
{
    RED_FLOWER,
    FROG_LEG,
    FRESH_DEW
}
