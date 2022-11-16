using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
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
        ChangeAnimation(playerAnimationState);
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Flip()
    {
        base.Flip();
    }

    public override void Jump()
    {
        base.Jump();
    }

    private void ChangeAnimation(PlayerAnimationState animationState)
    {
        playerAnimationState = animationState;
        animator.SetInteger("AnimationState", (int)playerAnimationState);
    }
}
