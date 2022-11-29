using UnityEditor;
using UnityEngine;

public class Constants {
    [SerializeField] public const string StartMenuScene = "Start Menu";
    public const string PauseMenuScene = "Pause Menu";
    public const string GameOverMenuScene = "Game Over";
    public const string WinMenuScene = "Win Menu";
    public const string PotionScene = "PotionBrewing";

    public static string[] Menus = new string[]{StartMenuScene, PauseMenuScene, GameOverMenuScene, WinMenuScene, PotionScene};


    public const string LevelSystemsScene = "LevelSystems";
    public const string LevelOne = "Level 2";

    public const string mouseAimBinding = "FPS_player";
    public const string keyboardAimBinding = "Platformer_player";
}

public enum ItemType {
    // ingredients
    BLUE_FLOWER,
    // potions
    DOUBLE_JUMP,
    
}