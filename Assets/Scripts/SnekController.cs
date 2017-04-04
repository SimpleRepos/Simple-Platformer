using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnekController : MonoBehaviour {

    public float WALK_SPEED;
    public float WALK_DELAY;
    public float FALL_CUTOFF;

    public Scorekeeper scoreboard;

    private float walkDelay;
    private Rigidbody2D rb;
    private BoxCollider2D myBox;
    private EdgeCollider2D edgeFinder;
    private bool dead = false;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
        edgeFinder = GetComponent<EdgeCollider2D>();
        myBox = GetComponent<BoxCollider2D>();
        walkDelay = WALK_DELAY;
    }

    void Update () {
        if (transform.position.y < FALL_CUTOFF)
        {
            Destroy(gameObject);
            return;
        }

        if (dead)
        {
            transform.Rotate(new Vector3(0, 0, 1), Time.deltaTime * 1000);
            return;
        }

        if (!edgeFinder.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            turnAround();
        }

        walkDelay -= Time.deltaTime;
        if (walkDelay <= 0)
        {
            Vector2 vel = rb.velocity;
            vel.x = WALK_SPEED;
            if (rb.transform.localScale.x < 0) { vel.x = -vel.x; }
            rb.velocity = vel;
            walkDelay = WALK_DELAY;
        }

    }

    void turnAround()
    {
        Vector3 scale = rb.transform.localScale;
        scale.x = -scale.x;
        rb.transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!dead)
        {
            var other = collision.gameObject;
            
            if (!other.GetComponent<BoxCollider2D>().IsTouching(myBox))
            {
                var script = other.GetComponent<KingController>();
                if (script) { script.damage(1); }
            }

            turnAround();
        }
    }

    public void damage(int amount)
    {
        foreach (var col in GetComponents<Collider2D>()) { Destroy(col); }
        dead = true;
        scoreboard.Population = scoreboard.Population - 1;
    }

}
