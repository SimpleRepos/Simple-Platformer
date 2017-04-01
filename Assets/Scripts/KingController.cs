using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingController : MonoBehaviour {

    private Animator anim;
    private Rigidbody2D rb;
    private Collider2D sensor;
    private SpriteRenderer spriteRenderer;
    private AudioSource[] sounds;
    private enum SoundID
    {
        STEP_LEFT = 0,
        STEP_RIGHT = 1,
        JUMP = 2,
        DIE = 3,
        HOP_ON_FOE = 4
    };

    public float JUMP_POOL_SECONDS;
    public float JUMP_SPEED;
    public float WALK_SPEED;
    public float FALL_CUTOFF;
    public float STEP_SOUND_DELAY;

    private float jumpPool;
    private float stepSoundDelay;
    private bool secondStep = false;
    private bool dead = false;
    private bool canJumpSound = false;

    public void damage(int amount)
    {
        foreach (var col in GetComponents<Collider2D>()) { Destroy(col); }
        dead = true;
        sounds[(int)SoundID.DIE].Play();
    }

    void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sensor = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sounds = GetComponents<AudioSource>();
        stepSoundDelay = STEP_SOUND_DELAY;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        var script = collision.gameObject.GetComponent<SnekController>();
        if (script) {
            sounds[(int)SoundID.HOP_ON_FOE].Play();
            script.damage(1);

            jumpPool = JUMP_POOL_SECONDS;

            Vector2 vel = rb.velocity;
            vel.y = JUMP_SPEED;
            rb.velocity = vel;

        }
    }

    void Update () {
        if (transform.position.y < FALL_CUTOFF) {
            Destroy(gameObject);
            return;
        }

        if (dead)
        {
            transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * 1000);
            return;
        }

        //gather data
        bool grounded = sensor.IsTouchingLayers(LayerMask.GetMask("Ground"));
        bool jumpKey = Input.GetButton("Jump");
        float horz = Input.GetAxis("Horizontal");

        //get working copy of velocity
        Vector2 vel = rb.velocity;

        //do horizontal motion
        vel.x = horz * WALK_SPEED;
        if (horz > 0) { spriteRenderer.flipX = false; }
        if (horz < 0) { spriteRenderer.flipX = true; }

        if (grounded && (Mathf.Abs(horz) > 0.001f)) { stepSoundDelay -= Time.deltaTime; }
        if (stepSoundDelay <= 0)
        {
            SoundID index = secondStep ? SoundID.STEP_LEFT : SoundID.STEP_RIGHT;
            sounds[(int)index].Play();
            secondStep = !secondStep;
            stepSoundDelay = STEP_SOUND_DELAY;
        }

        //process jumping
        if (!grounded && !jumpKey) {
            jumpPool = 0;
        }

        if (jumpKey && (jumpPool > 0)) {
            if (canJumpSound) { sounds[(int)SoundID.JUMP].Play(); }
            vel.y = JUMP_SPEED;
            jumpPool -= Time.deltaTime;
        }

        canJumpSound = grounded && !jumpKey;
        if (canJumpSound) { jumpPool = JUMP_POOL_SECONDS; }

        rb.velocity = vel;

        anim.SetBool("grounded", grounded);
        anim.SetFloat("vertical velocity", rb.velocity.y);
        anim.SetFloat("horizontal velocity", rb.velocity.x);
    }
}
