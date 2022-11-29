using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    // TODO: rename appropriately. Managers should be singletons. this is an interface that connects menus to appropriate managers

    // instantiated with each menu scene; there may be more than one of these in the world
    // Refers to CoreManager singleton, of which there will only be one


    void Start() {
        CoreManager.instance.openMenu = gameObject.scene.name;
    }

    public void Restart() {
        CoreManager.instance.RestartLevel();
    }

    public void ReturnToMainMenu() {
        CoreManager.instance.LoadMenu(Constants.StartMenuScene, LoadSceneMode.Single);
    }

    public void ExitGame() {
        CoreManager.instance.ExitGame();
    }

    public void Resume() {
        CoreManager.instance.levelManager.Resume();
    }

    public void LoadLevel() {
        CoreManager.instance.LoadLevel(Constants.LevelOne);
    }

    public void SwitchToMouseBindingGroup() {
        CoreManager.instance.SwitchBindingGroup(Constants.mouseAimBinding);
    }

    public void SwitchToKeyboardBindingGroup() {
        CoreManager.instance.SwitchBindingGroup(Constants.keyboardAimBinding);
    }
}