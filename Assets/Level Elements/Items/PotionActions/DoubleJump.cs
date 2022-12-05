using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// THIS ONE'S SPECIAL
// double jumping is implemented using CharacterJump
// so this ItemAction really only exists to handle cost/decrementing # double jump potions
public class DoubleJump : ItemAction {
    // public float jumpHeight;
    // private Rigidbody2D body;
    // private CharacterJump charjump;
    
    void Start(){
        // body = GetComponent<Rigidbody2D>();
        // charjump = GetComponent<CharacterJump>();
    }

    public override void perform(){
        // if (charjump.currentlyJumping) {
        //     body.velocity = new Vector2(body.velocity.x, 0f);
        //     body.AddForce(jumpHeight* Vector3.up, ForceMode2D.Impulse);
        // }
    }
}