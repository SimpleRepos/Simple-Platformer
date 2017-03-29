using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour {

    public float WALK_SPEED;
    public float WALK_DELAY;

    private float walkDelay;
    private Animator anim;
    private Rigidbody2D rb;
    private EdgeCollider2D edgeFinder;
    private BoxCollider2D colliderBox;

	void Start () {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        edgeFinder = GetComponent<EdgeCollider2D>();
        colliderBox = GetComponent<BoxCollider2D>();
        walkDelay = WALK_DELAY;
    }

    void Update () {
        Vector3 scale = rb.transform.localScale;
        if (!edgeFinder.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            scale.x = -scale.x;
            rb.transform.localScale = scale;
        }

        walkDelay -= Time.deltaTime;
        if (walkDelay <= 0)
        {
            Vector2 vel = rb.velocity;
            vel.x = WALK_SPEED;
            if (scale.x < 0) { vel.x = -vel.x; }
            rb.velocity = vel;
            walkDelay = WALK_DELAY;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var script = collision.gameObject.GetComponent<KingController>();
        if (script) { script.damage(1); }
    }
}
