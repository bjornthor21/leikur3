using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HealthCollectible : MonoBehaviour
{

    // hljóð sem er spilað þegar player tekur upp healthcollectible
    public AudioClip collectedClip;
    void OnTriggerEnter2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        // ef player snertir health collectible
        if (controller != null)
        {
            // og líf ruby er minna en maxhealth semsagt búinn að missa einhver líf
            if (controller.health < controller.maxHealth)
            {
                // hækkar líf ruby um 1, eyðir object og spilar hljóð
                controller.ChangeHealth(1);
                Destroy(gameObject);
                controller.PlaySound(collectedClip);
            }
        }
    }
}
