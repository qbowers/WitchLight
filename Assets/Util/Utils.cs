using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Utils {

    public static bool IsPlayer(GameObject player) {
        return player.tag == Constants.PlayerTag;
    }
}

[Serializable] public class BoolEvent : UnityEvent<bool> {}
[Serializable] public class InteractEvent : UnityEvent<Interactor> {}
