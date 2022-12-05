using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : Character
{
    /*TODO:
     * - 
     */

    [Header("Player Properties")]
    public bool CanDoubleJump;
    public Transform WallDetectionLength;
    public bool OnWall;
    public Transform CheckPointPostion;
    public float DeathPlaneValue;

    [Header("Player UI Properties")]
    public LifeSystem Life;
    public int Score;
    public TMP_Text ScoreText;

    [Header("Animation")]
    public Animator animator;
    public PlayerAnimationState playerAnimationState;

    private int LifeLevelUp;

    protected override void Start()
    {
        base.rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        LifeLevelUp = 1000;
    }

    protected override void Update()
    {
        if (Life != null && Life.LifeCount <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        IsGrounded = Physics2D.OverlapCircle(GroundPoint.position, GroundRadius, GroundLayerMask);
        OnWall = Physics2D.Linecast(new Vector3(transform.position.x, transform.position.y, 0.0f),
                                    new Vector3(WallDetectionLength.position.x, WallDetectionLength.position.y, 0.0f), GroundLayerMask);
        Move();
        AirCheck();
        IsOnWall();
        DeathPlane();
    }

    void DeathPlane()
    {
        if (transform.position.y <= DeathPlaneValue)
        {
            transform.position = CheckPointPostion.position;
            Life.LifeCount--;
        }
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
            ChangeAnimation(PlayerAnimationState.ON_WALL);
        }
    }

    public override void Jump()
    {
        if (OnWall && !IsGrounded)
        {
            rigidbody2D.AddForce(((transform.localScale.x > 0.0f) ? Vector2.left : Vector2.right) + Vector2.up * VerticalForce, ForceMode2D.Impulse);
        }

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
                        new Vector3(WallDetectionLength.position.x, WallDetectionLength.position.y, 0.0f));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fruit")
        {
            Score += 100;
            ScoreText.text = "x " + Score.ToString();
            Destroy(collision.gameObject);
            if (Score >= LifeLevelUp)
            {
                Life.LifeCount++;
                LifeLevelUp += 1000;
            }
        }
    }
}
