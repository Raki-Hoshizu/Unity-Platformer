using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerCombat : MonoBehaviour
{
    Animator PlayerAnimator;
    Transform AttackPoint;

    [SerializeField] int AttackDamage = 1;
    [SerializeField] float Knockback = 4f;
    [SerializeField] float AttackRange = 0.5f;
    [SerializeField] LayerMask EnemyLayers;

    private void Awake()
    {
        PlayerAnimator = GetComponent<Animator>();
        AttackPoint = transform.Find("AttackPoint").transform;
    }

    void OnFire()
    {
        Attack();
    }

    void Attack()
    {
        PlayerAnimator.SetTrigger("Attack");
        Collider2D[] HitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, EnemyLayers);
        foreach(Collider2D Enemy in HitEnemies)
        {
            if (Enemy.gameObject.GetComponent<EnemyHealth>() != null)
            {
                Enemy.gameObject.GetComponent<EnemyHealth>().TakeDamage(AttackDamage, Enemy.transform.position - AttackPoint.position, Knockback);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (AttackPoint == null) { return; }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
