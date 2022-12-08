using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillOn : MonoBehaviour
{
    public SpiderBrain brain;

    void OnTriggerEnter2D(Collider2D other) {
        if (!Utils.IsPlayer(other.gameObject) || other.gameObject.GetComponent<CharacterMovement>().isImmune) {
            return;
        }
        // zero out inventory
        CoreManager.instance.inventory.ZeroInventory();
        CoreManager.instance.LoadMenu(Constants.GameOverMenuScene, LoadSceneMode.Single);
    }

    IEnumerator killAnimation(GameObject player){
        brain.legs[0].moveToPoint(player.transform.position);
        brain.legs[1].moveToPoint(player.transform.position);
        yield return new WaitForSeconds(3f);
    }
}
