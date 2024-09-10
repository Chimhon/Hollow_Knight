using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;

    protected float stateTimer;
    public bool isAttacking;

    private string animBoolName;

    public Rigidbody2D rb;



    protected float xInput;
    protected float yInput;

    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        player = _player;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        rb = player.rb;
        player.anim.SetBool(animBoolName,true);
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName,false);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

}
