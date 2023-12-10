using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Public fyrir speed, orientation, change time, og audio clips.
    public float speed;
    public bool vertical;
    public float changeTime = 3.0f;
    public AudioClip playerHitClip;
    public AudioClip botHitClip;

    // partice effect
    public ParticleSystem SmokeEffect;

    Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;

    Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // athugar hvort broken er false ef svo þá hreyfist hann ekkert
        if (!broken)
        {
            return;
        }

        // breytir timer
        timer -= Time.deltaTime;
        
        // ef timer er minna en 0 þá breytir hann um átt og timer er endursettur
        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
    }

    void FixedUpdate()
    {
        // athugar hvort broken er false ef svo þá hreyfist hann ekkert
        if (!broken)
        {
            return;
        }

        // hreyfi logic.
        Vector2 position = rigidbody2D.position;

        
        // ef hann er vertical þá fer hann upp og niður annars hægri vinstir.
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // athugar hvort player snertir
        RubyController player = other.gameObject.GetComponent<RubyController>();

        // ef svo er þá minnkar líf players um 1 og hljóð er spilað
        if (player != null)
        {
            // Change the player's health and play the hit sound.
            player.ChangeHealth(-1);
            player.PlaySound(playerHitClip);
        }
    }

    // þetta er notað þegar player skýtur óvin til að hann hættir að hreyfast
    public void Fix()
    {
        // setur broken í false
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");

        // stoppar particle effect
        SmokeEffect.Stop();
    }
}
