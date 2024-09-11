using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{

    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.isJumping = true;

    }

    public override void Exit()
    {
        base.Exit();
        if (Input.GetKey(KeyCode.Z))
            player.jumpTimer = player.jumpTime;
    }
    public override void Update()
    {
        base.Update();
        if (!player.isGroundDetected())
            stateMachine.ChangeState(player.airState);

    }
}
