using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : ItemAction {
    public float dashDistance;
    public float dashDuration;
    [Tooltip("Grant immunity during dash?")]public bool blink = true;
    
    public override void perform(){
        StartCoroutine(dash());
    }

    IEnumerator dash() {
        // Disable Gravity (by fixing y position)
        controller.body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;

        // Set controller parameters (disable the controller's ability to effect player position for a moment)
        controller.isImmune = blink;
        controller.isEnabled = false;
        
        // Set velocity
        controller.body.velocity = new Vector2(dashDistance/dashDuration * controller.transform.localScale.x, 0);
        

        yield return new WaitForSeconds(dashDuration);
        
        // Restore Controller parameters
        controller.isImmune = false;
        controller.isEnabled = true;
        controller.body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
