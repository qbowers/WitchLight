using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour {
    [Tooltip("Negative values will never regenerate")]public int regenerationTime;
    public ItemType id;
    public int cnt;

    private SpriteRenderer spriterenderer;
    private BoxCollider2D boxcollider;

    public void Start() {
        spriterenderer = GetComponent<SpriteRenderer>();
        boxcollider = GetComponent<BoxCollider2D>();
    }

    public void Collect() {
        // turn off the spriterenderer and box collider
        spriterenderer.enabled = false;
        boxcollider.enabled = false;
        if (regenerationTime >= 0) StartCoroutine(regenerate());
    }

    private IEnumerator regenerate() {
        yield return new WaitForSeconds(regenerationTime);

        // turn on the spriterenderer and box collider
        spriterenderer.enabled = true;
        boxcollider.enabled = true;
    }
}
