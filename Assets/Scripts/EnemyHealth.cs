using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    int Health;
    [Range(1, 5)][SerializeField] int MaxHealth = 2;

    Animator EnemyAnimator;
    Rigidbody2D EnemyRigidBody;
    CapsuleCollider2D EnemyCollider;
    EnemyMovement Movement;

    void Start()
    {
        Health = MaxHealth;
        EnemyAnimator = GetComponent<Animator>();
        EnemyRigidBody = GetComponent<Rigidbody2D>();
        EnemyCollider = GetComponent<CapsuleCollider2D>();
        Movement = GetComponent<EnemyMovement>();
    }

    void Update()
    {
        if (Health > MaxHealth)
            Health = MaxHealth;
        else if (Health < 0)
            Health = 0;

        if (!IsAlive())
        {
            Movement.enabled = false;
            EnemyCollider.enabled = false;
            EnemyRigidBody.bodyType = RigidbodyType2D.Kinematic;
            EnemyRigidBody.velocity = Vector2.zero;
            EnemyAnimator.SetTrigger("Die");
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int Damage, Vector2 Direction, float Knockback)
    {
        Health -= Damage;
        EnemyAnimator.SetTrigger("Hit");
        Movement.enabled = false;
        EnemyRigidBody.velocity = new Vector2(Direction.x * Knockback, EnemyRigidBody.velocity.y);
        StartCoroutine(RecoverFromStun());
    }

    IEnumerator RecoverFromStun()
    {
        yield return new WaitForSeconds(0.3f);
        Movement.enabled = true;
    }

    public bool IsAlive() { return Health > 0; }
}
