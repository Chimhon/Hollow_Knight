using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerState
{

    private int AttackAction;
    private int comboCounter;
    private float comboWindow = 0.2f;

    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        isAttacking = true;
        stateTimer = 0.333f;
        //if (Input.GetKey(KeyCode.RightArrow))
        //    player.fx.SetSwordSlash(1.2f, 0.265f, 180);
        //else if (Input.GetKey(KeyCode.LeftArrow))
        //    player.fx.SetSwordSlash(1.2f, 0.265f, 0);
        //else if (Input.GetKey(KeyCode.UpArrow))
        //    player.fx.SetSwordSlash(0, 1.68f, -90);
        //else if (Input.GetKey(KeyCode.DownArrow) && player.isJumping)
        //    player.fx.SetSwordSlash(0, -1.8f, 90);
        //else
        //    player.fx.SetSwordSlash(1.2f, 0.265f, player.facingDir == 1 ? 180 : 0);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            AttackAction = 0;
            player.anim.SetInteger("AttackAction", AttackAction);
            player.fx.SetSwordSlash(AttackAction);
        }

    }

    public override void Exit()
    {
        base.Exit();
        AttackAction = 0;
        isAttacking = false;
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        if (stateTimer < 0 && rb.velocity.y != 0)
            stateMachine.ChangeState(player.airState);
        if (stateTimer < 0 && player.isGroundDetected())
            stateMachine.ChangeState(player.idleState);

    }
}
