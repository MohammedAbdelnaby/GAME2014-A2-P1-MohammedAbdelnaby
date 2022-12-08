using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Enemy Properties")]
    public Transform FrontWhisker;

    protected bool HitWall;
    protected Vector2 Direction;
    protected SoundManager soundManager;
    protected override void Start()
    {
        base.rigidbody2D = GetComponent<Rigidbody2D>();
        Direction = Vector2.left;
        soundManager = FindObjectOfType<SoundManager>();
    }
    protected override void Update()
    {
        if(FrontWhisker != null)
            HitWall = Physics2D.Linecast(transform.position, FrontWhisker.position, GroundLayerMask);
        Move();
    }

    protected override void Move()
    {
        if (HitWall)
        {
            Flip();
            Direction *= -1.0f;
        }
        rigidbody2D.AddForce(Direction * HorizontalForce);
        var clampedXVelocity = Mathf.Clamp(rigidbody2D.velocity.x, -HorizontalSpeed, HorizontalSpeed);
        rigidbody2D.velocity = new Vector2(clampedXVelocity, rigidbody2D.velocity.y);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && collision.transform.position.y - collision.gameObject.GetComponent<BoxCollider2D>().size.y/2 > transform.position.y + GetComponent<BoxCollider2D>().size.y / 2)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5.0f, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Player>().SetScore(500);
            soundManager.PlaySoundFX(Sound.ENEMYDEATH, Channel.ENEMY_SOUND_ENEMYDEATH);
            Destroy(this.gameObject);
            return;
        }
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if (FrontWhisker != null)
            Gizmos.DrawLine(transform.position, FrontWhisker.position);
    }
}
