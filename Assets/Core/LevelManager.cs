using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour {
    // public GameObject playerPrefab;

    private PlayerControls.OverarchingActions controlMap;
    private PlayerControls.PlayerActions playerMap;


    void Start() {
        Debug.Log("Level Manager Start");

        // CoreManager.instance.levelManager = this;

        controlMap = CoreManager.instance.playerControls.Overarching;
        playerMap = CoreManager.instance.playerControls.Player;


        controlMap.Enable();
        playerMap.Enable();

        StartLevel(CoreManager.instance.activeDoor);
    }

    public void StartLevel(int doorNumber) {
        Debug.Log("Level Start!");
        // Find the player start pad in the world
        // TODO: consider using tags or labels or layers or something. IDK.
        GameObject[] doorObjects = GameObject.FindGameObjectsWithTag(Constants.DoorTag);

        CinemachineVirtualCamera vcam = CoreManager.instance.vcamera;
        CinemachineConfiner2D vcamConfiner = vcam.gameObject.GetComponent<CinemachineConfiner2D>();


        // Spawn the player at start pad
        foreach(GameObject doorObject in doorObjects) {
            Door door = doorObject.GetComponent<Door>();
            if (door.doorNumber == doorNumber) {
                GameObject player = Instantiate(CoreManager.instance.playerPrefab, doorObject.transform.position + new Vector3(2.5f,0,0), Quaternion.identity);

                vcam.Follow = player.transform;
                
                GameObject[] monsters = GameObject.FindGameObjectsWithTag(Constants.MonsterTag);
                foreach(GameObject monster in monsters) {
                    try {
                        monster.GetComponent<GhostMonsterMovement>().player = player.transform;
                    } catch {}
                }
            }
        }

        GameObject levelConfiner = GameObject.FindWithTag(Constants.LevelConfinerTag);

        if (vcamConfiner == null) {
            Debug.Log("Huh this is bad");
        } else if (levelConfiner == null) {
            Debug.Log("No Level Confiner");
        }
        vcamConfiner.m_BoundingShape2D = levelConfiner.GetComponent<Collider2D>();

        
        // Start enemies, any other world components
        CoreManager.instance.Play();
    }

    
}