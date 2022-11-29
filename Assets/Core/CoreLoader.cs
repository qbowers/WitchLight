using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreLoader : MonoBehaviour {
    // Start is called before the first frame update
    public bool loadLevelSystems = false;

    void Awake() {
        if (CoreManager.instance == null) {
            SceneManager.LoadScene("Core", LoadSceneMode.Additive);

            // Game Levels also need LevelSystems
            if (loadLevelSystems) {
                SceneManager.LoadScene(Constants.LevelSystemsScene, LoadSceneMode.Additive);
            }
        }

        Destroy(this.gameObject);
    }
}
