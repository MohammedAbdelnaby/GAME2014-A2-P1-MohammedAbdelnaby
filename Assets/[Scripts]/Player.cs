using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    /*TODO:
     * - Player Movement
     * - Player Jump
     * - Player animation changes
     */

    [Header("Player Properties")]
    public bool CanDoubleJump;

    [Header("Animation")]
    public Animator animator;
    public PlayerAnimationState playerAnimationState;

    protected override void Start()
    {
        base.rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        IsGrounded = Physics2D.OverlapCircle(GroundPoint.position, GroundRadius, GroundLayerMask);
        Move();
    }

    public override void Move()
    {
        float X = Input.GetAxisRaw("Horizontal");
        if (X != 0.0f)
        {
            base.Flip(X);
            X = (X > 0.0f) ? 1.0f : -1.0f;

            rigidbody2D.AddForce(Vector2.right * X * HorizontalForce * ((IsGrounded) ? 1.0f : AirFactor));

            var clampedXVelocity = Mathf.Clamp(rigidbody2D.velocity.x, -HorizontalSpeed, HorizontalSpeed);
            rigidbody2D.velocity = new Vector2(clampedXVelocity, rigidbody2D.velocity.y);

            ChangeAnimation(PlayerAnimationState.RUN);
        }
        if (IsGrounded && X == 0)
        {
            ChangeAnimation(PlayerAnimationState.IDLE);
        }
    }

    public override void Flip(float value)
    {
        base.Flip(value);
    }

    public override void Jump()
    {

        if ((IsGrounded))
        {
            rigidbody2D.AddForce(Vector2.up * VerticalForce, ForceMode2D.Impulse);
        }
    }

    private void ChangeAnimation(PlayerAnimationState animationState)
    {
        playerAnimationState = animationState;
        animator.SetInteger("AnimationState", (int)playerAnimationState);
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundPoint.position, GroundRadius);
    }
}
