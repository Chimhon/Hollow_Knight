using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    private float lookTimer = 0.2f;
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (!player.isGroundDetected()/* && !player.isJumping*/)
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Z) /*&& player.isGroundDetected()*/)
            stateMachine.ChangeState(player.jumpState);
        if (isAttacking) return;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            lookTimer -= Time.deltaTime;
            if (lookTimer <= 0)
                stateMachine.ChangeState(player.lookUpState);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            lookTimer -= Time.deltaTime;
            if (lookTimer <= 0)
                stateMachine.ChangeState(player.lookDownState);
        }
        else lookTimer = 0.2f;

        if (Input.GetKeyDown(KeyCode.X))
            stateMachine.ChangeState(player.attackState);


    }
}
