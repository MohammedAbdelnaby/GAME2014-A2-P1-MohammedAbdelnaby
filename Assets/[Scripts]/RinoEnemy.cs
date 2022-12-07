using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RinoEnemy : Enemy
{
    private Transform playerTransform;
    private bool InLOS;
    private Vector2 playerDirectionVector;
    private float playerDirection;
    private float Raduis;
    private bool playerDetected;
    private Animator animator;

    protected override void Start()
    {
        base.rigidbody2D = GetComponent<Rigidbody2D>();
        Direction = Vector2.left;
        Raduis = GetComponent<CircleCollider2D>().radius;
        animator = GetComponent<Animator>();
    }
    protected override void Update()
    {
        if (FrontWhisker != null)
            HitWall = Physics2D.Linecast(transform.position, FrontWhisker.position, GroundLayerMask);
        base.Move();
        LOS();
        Charge();
    }
    private void LOS()
    {
        if (playerDetected)
        {
            playerDirectionVector = (playerTransform.position - transform.position);
            playerDirection = (playerDirectionVector.x > 0) ? 1.0f : -1.0f;

            InLOS = ((playerDirection == Direction.x));
        }
    }

    private void Charge()
    {
        if (InLOS)
        {
            HorizontalForce = 8;
            HorizontalSpeed = 8;
            ChangeAnimation(2);
        }
        else
        {                                     
            HorizontalForce = 4;
            HorizontalSpeed = 4;
            ChangeAnimation(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerTransform = collision.transform;
            playerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            playerDetected = false;
            InLOS = false;
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && collision.transform.position.y - collision.gameObject.GetComponent<BoxCollider2D>().size.y / 2 > transform.position.y + GetComponent<BoxCollider2D>().size.y / 2)
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 5.0f, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Player>().SetScore(500);
            Destroy(this.gameObject);
            return;
        }
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }

    private void ChangeAnimation(int animState)
    {
        animator.SetInteger("RinoState", animState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = (InLOS) ? Color.green : Color.red;
        Gizmos.DrawWireSphere(transform.position, Raduis);

        Gizmos.color = Color.green;
        if (FrontWhisker != null)
            Gizmos.DrawLine(transform.position, FrontWhisker.position);
    }
}
