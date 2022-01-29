using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D EnemyRigidBody;

    [SerializeField] float Speed = 200f;

    void Start()
    {
        EnemyRigidBody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        EnemyRigidBody.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * Speed * Time.deltaTime, EnemyRigidBody.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Ground")
        {
            transform.localScale = new Vector2(-(Mathf.Sign(EnemyRigidBody.velocity.x)), 1f);
        }
    }
}
