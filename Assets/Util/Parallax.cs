using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    GameObject camera_object;

    Transform camera_transform;
    Transform t;
    bool parallax = false;

    Vector3 starting_camera_position;
    List<Vector3> starting_positions = new List<Vector3>();

    // Start is called before the first frame update
    void Start() {
        // cache gameobject transform and camera transform
        t = GetComponent<Transform>();

        camera_object = CoreManager.instance.camera;
        camera_transform = camera_object.GetComponent<Transform>();
        
        // determine if the camera is orthographic
        parallax = camera_object.GetComponent<Camera>().orthographic;
        

        Vector3 camera_position = camera_transform.position;

        if (parallax) {
            // If camera is in orthographic mode, we record each child's current position for later use in the update loop
            Debug.Log("Starting Parallax Controller in Orthographic Mode");
            starting_camera_position = camera_position;

            for(int i = 0; i < t.childCount; i++) {
                Transform t_child = t.GetChild(i);
                starting_positions.Add(t_child.localPosition);
            }
        } else {
            // If camera is in perspective mode, we only have to scale and shift the children around
            Debug.Log("Starting Parallax Controller in Perspective Mode");
            // do scaling
            float cz = -camera_position.z;
            for(int i = 0; i < t.childCount; i++) {
                Transform child = t.GetChild(i);
                Vector3 child_position = child.localPosition;
                float r = (cz+child_position.z)/cz;
                Vector3 scale_vector = new Vector3(r,r,1);
                child.localScale = Vector3.Scale( child.localScale, scale_vector );

                child.localPosition = Vector3.Scale(child_position - camera_position, scale_vector) + camera_position;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        // if camera is not orthographic, this shifting is a bad idea
        if (!parallax) return;

        // Add offset due to camera starting location
        Vector3 camera_position = camera_transform.position - starting_camera_position;
        float camera_z = -camera_transform.position.z;

        // Foreach child transform
        for(int i = 0; i < t.childCount; i++) {
            Transform t_child = t.GetChild(i);

            // Reset child's position
            float z = t_child.localPosition.z;
            if (z == 0) continue;
            
            float x = camera_position.x * (1 - camera_z/(z + camera_z));
            float y = camera_position.y * (1 - camera_z/(z + camera_z));

            

            // Add offset due to object starting location
            t_child.localPosition = starting_positions[i] + new Vector3(x,y,0);
        }
    }
}
