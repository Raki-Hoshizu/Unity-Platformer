using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Animator TrapAnimator;
    [SerializeField] float Cooldown = 2f;
    
    BoxCollider2D TriggerCollider;

    void Start()
    {
        TrapAnimator = GetComponent<Animator>();
        TriggerCollider = GetComponent<BoxCollider2D>();
    }

    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        TrapAnimator.SetTrigger("Trigger");
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(1);
            other.gameObject.GetComponent<PlayerMovement>().Jump();
        }
        TriggerCollider.enabled = false;
        yield return new WaitForSeconds(Cooldown);
        TriggerCollider.enabled = true;
    }
}
