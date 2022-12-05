using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour {
    // public GameObject playerPrefab;

    private PlayerControls.OverarchingActions controlMap;
    private PlayerControls.PlayerActions playerMap;


    void Start() {
        // CoreManager.instance.levelManager = this;

        controlMap = CoreManager.instance.playerControls.Overarching;
        playerMap = CoreManager.instance.playerControls.Player;


        controlMap.Enable();
        playerMap.Enable();

        StartLevel(CoreManager.instance.activeDoor);
    }

    public void StartLevel(int doorNumber) {
        // Debug.Log("Level Start!");

        CinemachineVirtualCamera vcam = CoreManager.instance.vcamera;
        CinemachineConfiner2D vcamConfiner = vcam.gameObject.GetComponent<CinemachineConfiner2D>();
        GameObject levelConfiner = GameObject.FindWithTag(Constants.LevelConfinerTag);

        // Find the door
        GameObject door = null;
        GameObject[] doorObjects = GameObject.FindGameObjectsWithTag(Constants.DoorTag);
        foreach(GameObject doorObject in doorObjects) {
            Door doorCandidate = doorObject.GetComponent<Door>();
            if (doorCandidate.doorNumber == doorNumber) {
                door = doorObject;
            }
        }

        if (door == null) {
            Debug.Log("No Matching Door Found");
            return;
        }

        // Spawn the player at the door
        GameObject player = Instantiate(CoreManager.instance.playerPrefab, door.transform.position + new Vector3(2.5f,0,0), Quaternion.identity);


        // Hook up Monsters
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(Constants.MonsterTag);
        foreach(GameObject monster in monsters) {
            try {
                monster.GetComponent<GhostMonsterMovement>().player = player.transform;
            } catch {}

            try {
                monster.GetComponent<MonsterAI>().target = player.transform;
            } catch {}
        }


        // Hook up camera
        vcam.Follow = player.transform;
        vcam.transform.position = player.transform.position;

        vcamConfiner.m_BoundingShape2D = levelConfiner.GetComponent<Collider2D>();

        
        // Enable controls, resume simulation time
        CoreManager.instance.Play();
    }

    
}