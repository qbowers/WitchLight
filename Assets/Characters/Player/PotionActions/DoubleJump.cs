using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : ItemAction {
    public float jumpHeight;
    public bool allowWhileJumping;
    private Rigidbody2D body;
    private CharacterJump charjump;
    
    void Start(){
        body = GetComponent<Rigidbody2D>();
        charjump = GetComponent<CharacterJump>();
    }

    public override void perform(){
        if (allowWhileJumping || !charjump.currentlyJumping) {
            body.velocity = new Vector2(body.velocity.x, 0f);
            body.AddForce(jumpHeight* Vector3.up, ForceMode2D.Impulse);
        }
    }
}