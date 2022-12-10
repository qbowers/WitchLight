using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script handles purely aesthetic things like particles, squash & stretch, and tilt

public class CharacterJuice : MonoBehaviour {
    [Header("Components")]
    CharacterMovement moveScript;
    CharacterJump jumpScript;
    [SerializeField] Animator myAnimator;
    [SerializeField] GameObject characterSprite;

    [Header("Components - Particles")]
    [SerializeField] private ParticleSystem moveParticles;
    [SerializeField] public ParticleSystem jumpParticles;
    [SerializeField] private ParticleSystem landParticles;

    [Header("Components - Audio")]
    [SerializeField] AudioSource jumpSFX;
    [SerializeField] AudioSource landSFX;
    [SerializeField] AudioSource runSFX;

    [Header("Settings - Squash and Stretch")]
    [SerializeField] bool squashAndStretch;
    [SerializeField, Tooltip("Width Squeeze, Height Squeeze, Duration")] Vector3 jumpSquashSettings;
    [SerializeField, Tooltip("Width Squeeze, Height Squeeze, Duration")] Vector3 landSquashSettings;
    [SerializeField, Tooltip("How powerful should the effect be?")] public float landSqueezeMultiplier;
    [SerializeField, Tooltip("How powerful should the effect be?")] public float jumpSqueezeMultiplier;
    [SerializeField] float landDrop = 1;

    [Header("Tilting")]

    [SerializeField] bool leanForward;
    [SerializeField, Tooltip("How far should the character tilt?")] public float maxTilt;
    [SerializeField, Tooltip("How fast should the character tilt?")] public float tiltSpeed;

    [Header("Calculations")]
    // [ReadOnlyField]
    float runningSpeed;
    // [ReadOnlyField]
    float maxSpeed;

    [Header("Current State")]
    // [ReadOnlyField]
    bool squeezing;
    // [ReadOnlyField]
    bool jumpSqueezing;
    // [ReadOnlyField]
    bool landSqueezing;
    // [ReadOnlyField]
    bool playerGrounded;

    [System.NonSerialized] public Transform parent;
    private Rigidbody2D rb;

    void Start() {
        moveScript = GetComponent<CharacterMovement>();
        jumpScript = GetComponent<CharacterJump>();
        rb = GetComponent<Rigidbody2D>();

        jumpParticles = Instantiate(jumpParticles);
        moveParticles = Instantiate(moveParticles);
        landParticles = Instantiate(landParticles);

        jumpParticles.transform.parent = GetComponent<Transform>().transform;
        moveParticles.transform.parent = GetComponent<Transform>().transform;
        landParticles.transform.parent = GetComponent<Transform>().transform;

        jumpParticles.transform.position = jumpParticles.transform.parent.TransformPoint(0f, -1f, 0f);
        moveParticles.transform.position = moveParticles.transform.parent.TransformPoint(0f, -1.05f, 0f);
        landParticles.transform.position = landParticles.transform.parent.TransformPoint(0f, -1f, 0f);
    }

    void Update() {
        tiltCharacter();
        // We need to change the character's running animation to suit their current speed
        runningSpeed = Mathf.Clamp(Mathf.Abs(moveScript.velocity.x), 0, maxSpeed);
        // myAnimator.SetFloat("runSpeed", runningSpeed);
        if (moveScript.velocity.x != 0 && jumpScript.onGround) {
            if (!runSFX.isPlaying && runSFX.enabled) {
                runSFX.Play();
            }
            if (!moveParticles.isPlaying) moveParticles.Play();
        }
        else{
            runSFX.Stop();
            if (moveParticles.isPlaying) moveParticles.Stop();
        }

        checkForLanding();
    }



    private void tiltCharacter() {
        //See which direction the character is currently running towards, and tilt in that direction
        float directionToTilt = 0;
        if (moveScript.velocity.x != 0) {
            directionToTilt = Mathf.Sign(moveScript.velocity.x);
            if (!runSFX.isPlaying && runSFX.enabled) {
                runSFX.Play();
            }
        }

        Quaternion currentRotation = characterSprite.transform.rotation;
        //Create a vector that the character will tilt towards
        Vector3 targetRotVector = new Vector3(0, 0, -Mathf.Lerp(-maxTilt, maxTilt, Mathf.InverseLerp(-1, 1, directionToTilt)));
        //And then rotate the character in that direction
        characterSprite.transform.rotation = Quaternion.RotateTowards(currentRotation, Quaternion.Euler(targetRotVector), tiltSpeed * Time.deltaTime);
    
    }

    private void checkForLanding() {
        if (!playerGrounded && jumpScript.onGround) {
            //By checking for this, and then immediately setting playerGrounded to true, we only run this code once when the player hits the ground 
            playerGrounded = true;

            //Play an animation, some particles, and a sound effect when the player lands
            myAnimator.SetTrigger("Landed");
            if (!landParticles.isPlaying) landParticles.Play();

            if (!landSFX.isPlaying && landSFX.enabled) {
                landSFX.Play();
            }

            //Start the landing squash and stretch coroutine.
            if (!landSqueezing && landSqueezeMultiplier > 1) {
                StartCoroutine(JumpSqueeze(landSquashSettings.x * landSqueezeMultiplier, landSquashSettings.y / landSqueezeMultiplier, landSquashSettings.z, landDrop, false));
            }

            moveParticles.Play();

        } else if (playerGrounded && !jumpScript.onGround) {
            // Player has left the ground, so stop playing the running particles
            playerGrounded = false;
            if (moveParticles.isPlaying) moveParticles.Stop();
        } 
    }


    public void JumpEffects() {
        // Play these effects when the player jumps, courtesy of jump script
        // myAnimator.ResetTrigger("Landed");
        // myAnimator.SetTrigger("Jump");

        if (jumpSFX.enabled) {
            jumpSFX.Play();
        }

        if (!jumpSqueezing && jumpSqueezeMultiplier > 1) {
            StartCoroutine(JumpSqueeze(jumpSquashSettings.x / jumpSqueezeMultiplier, jumpSquashSettings.y * jumpSqueezeMultiplier, jumpSquashSettings.z, 0, true));
        }
        if (!jumpParticles.isPlaying) jumpParticles.Play();
    }

    IEnumerator JumpSqueeze(float xSqueeze, float ySqueeze, float seconds, float dropAmount, bool jumpSqueeze) {
        //We log that the player is squashing/stretching, so we don't do these calculations more than once
        if (jumpSqueeze) { jumpSqueezing = true; }
        else { landSqueezing = true; }
        squeezing = true;

        Vector3 originalSize = Vector3.one;
        Vector3 newSize = new Vector3(xSqueeze, ySqueeze, originalSize.z);

        Vector3 originalPosition = Vector3.zero;
        Vector3 newPosition = new Vector3(0, -dropAmount, 0);

        //We very quickly lerp the character's scale and position to their squashed and stretched pose...
        float t = 0f;
        while (t <= 1.0) {
            t += Time.deltaTime / 0.01f;
            characterSprite.transform.localScale = Vector3.Lerp(originalSize, newSize, t);
            characterSprite.transform.localPosition = Vector3.Lerp(originalPosition, newPosition, t);
            yield return null;
        }

        //And then we lerp back to the original scale and position at a speed dicated by the developer
        //It's important to do this to the character's sprite, not the gameobject with a Rigidbody an/or collision detection
        t = 0.5f;
        while (t <= 1.0) {
            t += Time.deltaTime / seconds;
            characterSprite.transform.localScale = Vector3.Lerp(newSize, originalSize, t);
            characterSprite.transform.localPosition = Vector3.Lerp(newPosition, originalPosition, t);
            yield return null;
        }

        if (jumpSqueeze) { jumpSqueezing = false; }
        else { landSqueezing = false; }
    }
}