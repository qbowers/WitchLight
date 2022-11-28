using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PotionSceneTrigger : MonoBehaviour
{
    public string level = "PotionBrewing";
    public bool additive = true;
    public bool pauseEnemy = false;

    public void OnTriggerEnter2D(Collider2D other) {

        if (other.gameObject.name != "Player") {
            return;
        }

        CoreManager.instance.levelManager.PauseInput();

        SceneManager.LoadScene(level, LoadSceneMode.Additive);
    }

    public void OnClick()
    {
        SceneManager.UnloadSceneAsync(level);
        CoreManager.instance.levelManager.Play();
    }
}
