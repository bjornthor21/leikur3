using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // geumir rigidbody skotsins
    Rigidbody2D rigidbody2d;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // public fall sem skýtur skotinu í gefna átt með gefnu afli
    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }

    void Update()
    {
        // athugar ef skotið er langt frá upphaflegu staðsetningu ef svo eyðir skotinu
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    // collision detection sem er kallað í þegar skotið snertir annan rigidbody collider
    void OnCollisionEnter2D(Collision2D other)
    {
        // ef colliderinn sem skotið hitti er óvinur
        EnemyController e = other.collider.GetComponent<EnemyController>();
        if (e != null)
        {
            // þá er Fix fallið í óvina scriptunni kallað
            e.Fix();
        }

        // eyðir alltaf skotinu þegar það snerir annan collider
        Destroy(gameObject);
    }
}
