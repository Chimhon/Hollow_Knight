using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    private float jumpHight;

    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        
        //WalljumpFalling
        if (!player.isJumping)
        {
            rb.velocity = new Vector2 (0, rb.velocity.y);
        }

    }

    public override void Exit()
    {
        base.Exit();
        ////player.isJumping = false;
        player.isWallJumping = false;


    }

    public override void Update()
    {
        base.Update();

        //JumpHightController
        

        //if (jumpTimer > 0 && player.isWallJumping)
        //{ 
        //    if (Input.GetKeyUp(KeyCode.Z))
        //    {
        //        jumpTimer = 0;
        //        rb.velocity = new Vector2(rb.velocity.x, jumpHight);
        //    }
        //    jumpTimer -= Time.deltaTime * 2f;
        //    jumpTimer = Mathf.Clamp(jumpTimer, 0f, 1f);
        //    rb.velocity = new Vector2(rb.velocity.x, jumpHight * (jumpTimer * 2.2f));
        //}

        //AirMoveSpeed
        if (xInput != 0)
            player.SetVelocity(player.moveSpeed  * xInput, rb.velocity.y);

        //ChangeState
        if (player.isGroundDetected())
            stateMachine.ChangeState(player.idleState);
        if (player.isWallDetected())
            stateMachine.ChangeState(player.wallSliderState);

        if (Input.GetKeyDown(KeyCode.X))
            stateMachine.ChangeState(player.attackState);
    }
}
