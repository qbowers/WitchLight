using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils {

    public static bool IsPlayer(GameObject player) {
        return player.tag == Constants.PlayerTag;
    }
}
