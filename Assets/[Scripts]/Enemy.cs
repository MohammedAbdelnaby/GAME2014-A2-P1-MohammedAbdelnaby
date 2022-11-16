using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    protected override void Start()
    {
        Debug.Log(this.gameObject.name);
    }

    protected override void Update()
    {
        base.Update();
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
}
