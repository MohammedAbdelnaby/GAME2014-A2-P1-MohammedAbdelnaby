using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : Character
{
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

    [Header("Controls")]
    public FloatingJoystick leftStick;

    [Header("Animation")]
    public Animator animator;
    public PlayerAnimationState playerAnimationState;

    private int LifeLevelUp;
    private SoundManager soundManager;

    protected override void Start()
    {
        Debug.Log("Start");
        //leftStick = GameObject.Find("LeftStick").GetComponent<FloatingJoystick>();
        base.rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        soundManager = FindObjectOfType<SoundManager>();
        LifeLevelUp = 1000;
    }

    protected override void Update()
    {
        //Debug.Log("Update");
        if (Life != null && Life.LifeCount <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        IsGrounded = Physics2D.OverlapCircle(GroundPoint.position, GroundRadius, this.GroundLayerMask);
        OnWall = Physics2D.Linecast(new Vector3(this.transform.position.x, this.transform.position.y, 0.0f),
                                    new Vector3(WallDetectionLength.position.x, WallDetectionLength.position.y, 0.0f), GroundLayerMask);
        this.Move();
        this.AirCheck();
        this.IsOnWall();
        this.DeathPlane();
    }

    void DeathPlane()
    {
        if (transform.position.y <= DeathPlaneValue)
        {
            Die();
        }
    }

    public void SetScore(int value)
    {
        Score += value;
        ScoreText.text = "x " + Score.ToString();
        if (Score >= LifeLevelUp)
        {
            Life.LifeCount++;
            LifeLevelUp += 1000;
        }
    }

    protected override void Move()
    {
        float X = Input.GetAxisRaw("Horizontal") + leftStick.Horizontal;
        if (X != 0.0f)
        {
            Flip(X);
            X = (X > 0.0f) ? 1.0f : -1.0f;
            rigidbody2D.AddForce(Vector2.right * X * HorizontalForce * ((IsGrounded) ? 1.0f : AirFactor));

            var clampedXVelocity = Mathf.Clamp(rigidbody2D.velocity.x, -HorizontalSpeed, HorizontalSpeed);
            rigidbody2D.velocity = new Vector2(clampedXVelocity, rigidbody2D.velocity.y);

            ChangeAnimation(PlayerAnimationState.RUN);
            Debug.Log(HorizontalSpeed);
        }
        if (IsGrounded && X == 0)
        {
            ChangeAnimation(PlayerAnimationState.IDLE);
        }
    }

    void IsOnWall()
    {
        if (OnWall && !IsGrounded)
        {
            ChangeAnimation(PlayerAnimationState.ON_WALL);
        }
    }

    protected override void Jump()
    {
        Debug.Log("Jump");
        if (OnWall && !IsGrounded)
        {
            rigidbody2D.AddForce(((transform.localScale.x > 0.0f) ? Vector2.left : Vector2.right) + Vector2.up * VerticalForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(Sound.JUMP, Channel.PLAYER_SOUND_JUMP);
        }

        if ((IsGrounded))
        {
            rigidbody2D.AddForce(Vector2.up * VerticalForce, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(Sound.JUMP, Channel.PLAYER_SOUND_JUMP);
            CanDoubleJump = true;
        }
         if(CanDoubleJump && !IsGrounded)
        {
            rigidbody2D.AddForce(Vector2.up * VerticalForce/1.5f, ForceMode2D.Impulse);
            soundManager.PlaySoundFX(Sound.JUMP, Channel.PLAYER_SOUND_JUMP);
            CanDoubleJump = false;
        }
    }

    public void Die()
    {
        transform.position = CheckPointPostion.position;
        Life.LifeCount--;
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
            SetScore(100);
            Destroy(collision.gameObject);
            soundManager.PlaySoundFX(Sound.FRUIT_PICKUP, Channel.PLAYER_SOUND_FRUIT_PICKUP);
        }
    }
}
