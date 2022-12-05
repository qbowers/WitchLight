using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : ItemAction {
    [Tooltip("Compare to CharacterMovement max speed")]public float dashSpeed;
    public float dashDistance;
    public float dashDuration;
    [Tooltip("Grant immunity during dash?")]public bool blink = true;
    
    public override void perform(){
        Debug.Log("Dashing. with blink? " + blink.ToString());
        // controller.ApplyForce(new Vector2(dashSpeed * controller.transform.localScale.x, 0f));
        StartCoroutine(dash());
    }

    IEnumerator dash() {
        // float oldSpeed = controller.maxSpeed;
        // controller.maxSpeed = dashSpeed;
        controller.body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;

        controller.isImmune = true;
        controller.isEnabled = false;
        // controller.body.velocity = new Vector2(dashSpeed * controller.transform.localScale.x, 0);
        controller.body.velocity = new Vector2(dashDistance/dashDuration * controller.transform.localScale.x, 0);
        // movement.directionX = 1;
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(dashDuration);
        
        //Do the action after the delay time has finished.
        Debug.Log("dash done");
        // controller.maxSpeed = oldSpeed;
        controller.isImmune = false;
        controller.isEnabled = true;
        // controller.body.velocity = Vector2.zero;
        controller.body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
