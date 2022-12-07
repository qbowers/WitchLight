using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : ItemAction {
    public float jumpHeight;
    public float jumpDuration;
    public bool allowWhileJumping;
    private Rigidbody2D body;
    private CharacterJump charjump;
    
    void Start(){
        body = GetComponent<Rigidbody2D>();
        charjump = GetComponent<CharacterJump>();
    }

    public override void perform(){
        if (allowWhileJumping || !charjump.currentlyJumping) {
            StartCoroutine(jump());
        }
        //     body.velocity = new Vector2(body.velocity.x, 0f);
        //     body.AddForce(jumpHeight* Vector3.up, ForceMode2D.Impulse);
        // }
    }


    IEnumerator jump() {
        // Disable Gravity (by fixing y position)
        // controller.body.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;

        // Set controller parameters (disable the controller's ability to effect player position for a moment)
        // controller.isImmune = blink;
        // controller.isEnabled = false;
        charjump.isEnabled = false;
        // cache gravity
        float gravityScale = controller.body.gravityScale;
        controller.body.gravityScale = 0;
        
        // Set velocity
        controller.body.velocity = new Vector2(0,jumpHeight/jumpDuration);
        

        yield return new WaitForSeconds(jumpDuration);
        
        // Restore Controller parameters
        // controller.isImmune = false;
        // controller.isEnabled = true;
        charjump.isEnabled = true;
        controller.body.gravityScale = gravityScale;
        // controller.body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}