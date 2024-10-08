using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (Input.GetKey(KeyCode.Z))
            player.jumpTimer = player.jumpTime / 2;
        stateTimer = 0.1f;
        player.SetVelocity(10.5f * -player.facingDir, player.jumpForce * 3);
        xInput = 0;
        player.isWallJumping = true;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            stateMachine.ChangeState(player.airState);
        
        
        if(player.isGroundDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
