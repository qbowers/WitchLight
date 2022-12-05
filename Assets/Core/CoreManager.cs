using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Cinemachine;

public class CoreManager : MonoBehaviour {
    public static CoreManager instance = null;

    
    [NonSerialized] public string openLevel = Constants.LevelOne;
    [NonSerialized] public string openMenu = null;

    // [NonSerialized] public LevelManager levelManager;
    public Inventory inventory;
    public PlayerControls playerControls;
    public PlayerControls.OverarchingActions controlMap;
    public PlayerControls.PlayerActions playerMap;

    public GameObject camera;
    public GameObject playerPrefab;
    public CinemachineVirtualCamera vcamera;

    // Change this flag to switch between FPS and platformer controls
    public string bindingGroupFilter = Constants.mouseAimBinding;

    public void Awake() {
        Debug.Log("CoreManager Awake");

        if (CoreManager.instance != null) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
            CoreManager.instance = this;

            // Required to allow both AIM and MOVE input actions to reference the left/right arrow keys
            InputSystem.settings.SetInternalFeatureFlag("DISABLE_SHORTCUT_SUPPORT", true);

            SpawnManagers();
        }
    }

    public void SpawnManagers() {
        // Inputsystem
        playerControls = new PlayerControls();
        controlMap = playerControls.Overarching;
        playerMap = playerControls.Player;
        
        var bindingGroup = playerControls.controlSchemes.First(x => x.name == bindingGroupFilter).bindingGroup;
            // Set as binding mask on actions. Any binding that doesn't match the mask will be ignored.
            playerControls.bindingMask = InputBinding.MaskByGroup(bindingGroup);
        
        // pause button binding
        controlMap.TogglePause.performed += (context) => {
            if (openMenu != null) {
                // A menu is open; game is paused
                Resume();
            } else {
                Pause();
            }
        };

        // Find or create instances of all other required managers, DontDestroyOnLoad as required
        // e.g. audiomanager, levelmanager, etc.

        this.camera = transform.Find("Main Camera").gameObject;
    }

    // public T FindOrCreate<T>() where T:Component {
    //     // Find existing component
    //     T component = gameObject.GetComponent<T>();
    //     // If its not there, create it
    //     if (component == null) {
    //         component = gameObject.AddComponent<T>();
    //     }

    //     return component;
    // } 


    
    public int activeDoor;
    public void LoadLevel(string levelName, bool additive = false, int doorNumber = 0) {

        // Debug.Log("Load Level!");
        activeDoor = doorNumber;

        if (additive) SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        else SceneManager.LoadScene(levelName, LoadSceneMode.Single);
        // additively load scene
        // SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
        // ping the levelmanager that the level is open
        // levelManager.StartLevel(doorNumber);

        // this.openLevel = levelName;
    }

    public void UnloadLevel() {
        // SceneManager.UnloadSceneAsync(Constants.LevelSystemsScene);
        SceneManager.UnloadSceneAsync(this.openLevel);
    }
    public void RestartLevel() {
        LoadLevel(this.openLevel);
    }

    public void LoadMenu(string menuName, LoadSceneMode mode = LoadSceneMode.Additive) {
        SceneManager.LoadSceneAsync(menuName, mode);
        // the spawned menu manager will set openMenu to its scene
        
        PauseInput();
    }

    public void UnloadMenu(string menuName) {
        SceneManager.UnloadSceneAsync(menuName);
        this.openMenu = null;
    }

    // public void Pause() {
    //     this.levelManager.Pause();
    //     LoadMenu(Constants.PauseMenuScene);
    // }

    // public void Resume() {
    //     // TODO: unload pause menu
    //     this.levelManager.Resume();
    // }

    public void ExitGame() {
        // do any required cleanup

        // https://answers.unity.com/questions/161858/startstop-playmode-from-editor-script.html
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com");
        #else
        Application.Quit();
        #endif
    }

    public void SwitchBindingGroup(string filter) {
        if (filter != bindingGroupFilter) {
            bindingGroupFilter = filter;
            var bindingGroup = playerControls.controlSchemes.First(x => x.name == filter).bindingGroup;
            // Set as binding mask on actions. Any binding that doesn't match the mask will be ignored.
            playerControls.bindingMask = InputBinding.MaskByGroup(bindingGroup);
            // GameObject.FindWithTag("Player").GetComponent<ItemActionsController>().bindActions();
        }
    }


    public void Pause() {
        // this.paused = true;
        // disable any movement input
        PauseInput();
        Time.timeScale = 0;
        CoreManager.instance.LoadMenu(Constants.PauseMenuScene);
        // poke scene_manager.menu_manager
    }

    public void OpenPotionScreen() {
        CoreManager.instance.LoadMenu(Constants.PotionScene);
        PauseInput();
    }

    public void PauseInput() {
        this.playerMap.Disable();
    }
    
    public void Play() {
        this.playerMap.Enable();
        Time.timeScale = 1;
    }
    public void Resume() {
        CoreManager.instance.UnloadMenu(CoreManager.instance.openMenu);
        Play();
    }

}