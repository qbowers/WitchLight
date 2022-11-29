using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : ItemAction {
    public float jumpHeight;
    private Rigidbody2D body;
    void Start(){
        body = GetComponent<Rigidbody2D>();
    }

    public override void perform(){
        body.velocity = new Vector2(body.velocity.x, 0f);
        body.AddForce(jumpHeight* Vector3.up, ForceMode2D.Impulse);
    }
}