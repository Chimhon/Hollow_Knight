using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSliderState : PlayerState
{
    public PlayerWallSliderState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(0,rb.velocity.y * 0.8f);
        if (player.isGroundDetected() )
            stateMachine.ChangeState(player.idleState);
        if(!player.isWallDetected())
            stateMachine.ChangeState(player.airState);
        if (Input.GetKeyDown(KeyCode.Z))
            stateMachine.ChangeState(player.wallJumpState);


    }
}
