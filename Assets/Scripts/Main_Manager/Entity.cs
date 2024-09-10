using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }


    protected float gravityScale;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    protected virtual void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public void SetVelocity(float _xInput, float _yInput)
    {
        rb.velocity = new Vector2(_xInput, _yInput);
        FlipController(_xInput);
    }
    public void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }


    public void FlipController(float _xInput)
    {
        if (_xInput > 0 && !facingRight) Flip();
        if (_xInput < 0 && facingRight) Flip();
    }

}
