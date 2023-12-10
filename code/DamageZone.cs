using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();
        // ef ruby snertir þá minnkar health um 1
        if (controller != null)
        {
            controller.ChangeHealth(-1);
        }
    }
}
