using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Vector2 MoveInput;
    Rigidbody2D PlayerRigidBody;
    CapsuleCollider2D BodyCollider;
    BoxCollider2D FeetCollider;
    Animator PlayerAnimator;

    int GroundLayersInt;

    [SerializeField] float Speed = 500f;
    [SerializeField] float JumpSpeed = 15f;
    [SerializeField] List<LayerMask> GroundLayers;

    void Start()
    {
        PlayerRigidBody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
        BodyCollider = GetComponent<CapsuleCollider2D>();
        FeetCollider = GetComponentInChildren<BoxCollider2D>();

        for(int i = 0; i < GroundLayers.Count; i++)
        {
            GroundLayersInt = GroundLayersInt | GroundLayers[i].value;
        }
    }

    void Update()
    {
        Push();
    }

    private void FixedUpdate()
    {
        Run();
        FlipSprite();
    }

    void OnMove(InputValue Value)
    {
        MoveInput = Value.Get<Vector2>();
    }

    void OnJump(InputValue Value)
    {
        if (!FeetCollider.IsTouchingLayers(GroundLayersInt)) { return; }
        if (Value.isPressed)
        {
            Jump();
        }
    }

    public void Jump()
    {
        PlayerRigidBody.velocity = new Vector2(PlayerRigidBody.velocity.x, JumpSpeed);
    }

    void Run()
    {
        Vector2 PlayerVelocity = new Vector2(MoveInput.x * Speed * Time.deltaTime, PlayerRigidBody.velocity.y);
        PlayerRigidBody.velocity = PlayerVelocity;

        bool bHorizontalyMoving = Mathf.Abs(PlayerRigidBody.velocity.x) > Mathf.Epsilon;

        if (bHorizontalyMoving)
        {
            PlayerAnimator.SetBool("isRunning", true);
        }
        else
        {
            PlayerAnimator.SetBool("isRunning", false);
        }
    }

    void Push()
    {
        if (!BodyCollider.IsTouchingLayers(LayerMask.GetMask("Pushable")))
        {
            PlayerAnimator.SetBool("isPushing", false);
        }
        else
        {
            PlayerAnimator.SetBool("isPushing", true);
        }
    }

    void FlipSprite()
    {
        bool bHorizontalyMoving = Mathf.Abs(PlayerRigidBody.velocity.x) > Mathf.Epsilon;

        if (bHorizontalyMoving)
        {
            transform.localScale = new Vector2 (Mathf.Sign(PlayerRigidBody.velocity.x), 1f);
        }
    }
}
