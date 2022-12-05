using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoreLoader : MonoBehaviour {
    void Awake() {
        // Debug.Log("CoreLoader Awake");

        if (CoreManager.instance == null) {
            SceneManager.LoadScene("Core", LoadSceneMode.Additive);
        }
        Destroy(this.gameObject);
    }
}
