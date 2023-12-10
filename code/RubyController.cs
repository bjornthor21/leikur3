using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Public breytur fyrir hreyfi hraða, heildar líf, skot, og audio clip.
    public float speed = 3.0f;
    public int maxHealth = 5;
    public GameObject projectilePrefab;
    public AudioClip shootClip;

    // nær í núverandi líf
    public int health { get { return currentHealth; } }
    int currentHealth;
    
    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    AudioSource audioSource;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
    }
    void Update()
    {
        // fær input fyrir hreyfingu.
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        // býr til vector úr input
        Vector2 move = new Vector2(horizontal, vertical);

        // breytir look direction
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        // breytir animation
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        // ósýnileiki timer teljari.
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        // skjóta logic.
        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
            this.PlaySound(shootClip);
        }

        // ef ýtt er á x.
        if (Input.GetKeyDown(KeyCode.X))
        {
            // og raycastið hittir froskinn
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                // þá er kallað á froskinn til að opna textan
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }
    }

    void FixedUpdate()
    {
        // hreyfir ruby
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    // spilar hljóð
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // breytir lífi ruby
    public void ChangeHealth(int amount)
    {
        // ef ruby er ósýnileg þá minnkar ekki líf
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        // annars minnkar líf
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        // og teljarinn minnkar
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    // skýtur skoti
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}
