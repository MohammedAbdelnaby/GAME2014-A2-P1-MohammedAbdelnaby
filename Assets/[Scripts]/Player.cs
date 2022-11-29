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
    public float WallDetectionLength;
    public bool OnWall;

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
        OnWall = Physics2D.Linecast(new Vector3(transform.position.x, transform.position.y, 0.0f),
                        new Vector3(transform.position.x + WallDetectionLength, transform.position.y, 0.0f), GroundLayerMask);
        Move();
        AirCheck();
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

    void IsOnWall()
    {
        if (OnWall && !IsGrounded)
        {

        }
    }

    public override void Jump()
    {

        if ((IsGrounded))
        {
            rigidbody2D.AddForce(Vector2.up * VerticalForce, ForceMode2D.Impulse);
            CanDoubleJump = true;
        }
         if(CanDoubleJump && !IsGrounded)
        {
            rigidbody2D.AddForce(Vector2.up * VerticalForce/1.5f, ForceMode2D.Impulse);
            CanDoubleJump = false;
        }
    }

    private void ChangeAnimation(PlayerAnimationState animationState)
    {
        playerAnimationState = animationState;
        animator.SetInteger("AnimationState", (int)playerAnimationState);
    }

    private void AirCheck()
    {
        if (!IsGrounded && CanDoubleJump)
        {
            ChangeAnimation(PlayerAnimationState.JUMP);
        }
        else if(!IsGrounded && !CanDoubleJump)
        {
            ChangeAnimation(PlayerAnimationState.DOUBLE_JUMP);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(GroundPoint.position, GroundRadius);

        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, 0.0f),
                        new Vector3(transform.position.x + WallDetectionLength, transform.position.y, 0.0f));
    }
}
