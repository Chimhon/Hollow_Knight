using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerAttackState : PlayerState
{

    private int AttackAction;

    private int comboCounter;
    private float comboWindow = 0.2f;
    private float lastTimeAttacked;

    private bool AttackfacingRight;

    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAttacking = true;
        stateTimer = 0.35f;
        AttackfacingRight = player.facingRight;

         if (Input.GetKey(KeyCode.UpArrow))
            CreateSwordSlash(2, 1.2f, 3.5f);
        else if (Input.GetKey(KeyCode.DownArrow) && !player.isGroundDetected())
            CreateSwordSlash(3, 1.2f, -5f);
        else if (Random.Range(0, 100) > 20 && Time.time <= comboWindow + lastTimeAttacked && comboCounter == 1)
        {
            CreateSwordSlash(1, 4,0,8);
            comboCounter = 0;
        }
        else
        {
            CreateSwordSlash(0, 5, 0.5f);
            comboCounter = 1;
        }
        
        


    }

    public override void Exit()
    {
        base.Exit();
        AttackAction = 0;
        isAttacking = false;
        lastTimeAttacked = Time.time;
        player.fx.DestroySwordSlash();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        if (stateTimer < 0 && rb.velocity.y != 0)
            stateMachine.ChangeState(player.airState);

        if (stateTimer < 0 && player.isGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if(AttackfacingRight != player.facingRight && xInput != 0)
            stateMachine.ChangeState(player.walkState);


    }

    private void CreateSwordSlash(int _AttackAction,float _SwordXPosition,float _SwordYPosition,int _Mode2Rotation = 0)
    {
        AttackAction = _AttackAction;
        player.anim.SetInteger("AttackAction", AttackAction);
        player.fx.SetSwordSlash(AttackAction, _SwordXPosition, _SwordYPosition, _Mode2Rotation);
    }
}
