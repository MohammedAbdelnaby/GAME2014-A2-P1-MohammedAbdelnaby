using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character : MonoBehaviour
{
    [Header("Character Properties")]
    [SerializeReference] protected float HorizontalForce;
    [SerializeReference] protected float HorizontalSpeed;
    [SerializeReference] protected float VerticalForce;
    [SerializeReference] protected float AirFactor;
    [SerializeReference] protected Transform GroundPoint;
    [SerializeReference] protected float GroundRadius;
    [SerializeReference] protected LayerMask GroundLayerMask;
    [SerializeReference] protected bool IsGrounded;

    protected Rigidbody2D rigidbody2D;

    protected virtual void Start()
    {
        Debug.Log(this.gameObject.name);
    }

    protected virtual void Update()
    {

    }

    protected virtual void Move() { }
    protected virtual void Jump() { }
    protected virtual void Flip(float value) 
    {
        if (value != 0.0f)
        {
            transform.localScale = new Vector3((value > 0.0f) ? 1.0f : -1.0f, 1.0f, 1.0f);
        }
    }

    protected virtual void Flip()
    {
        var X = transform.localScale.x * -1.0f;
        transform.localScale = new Vector3(X, 1.0f, 1.0f);
    }
}
