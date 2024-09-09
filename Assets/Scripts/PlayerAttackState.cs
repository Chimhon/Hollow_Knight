using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttackState : PlayerState
{

    private int AttackAction;

    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.15f;


        if (Input.GetKey(KeyCode.RightArrow))
            player.fx.swordSlashFX.SetSwordSlash(1.2f, 0.265f, 180);
        else if (Input.GetKey(KeyCode.LeftArrow))
            player.fx.swordSlashFX.SetSwordSlash(1.2f, 0.265f, 0);
        else if (Input.GetKey(KeyCode.UpArrow))
            player.fx.swordSlashFX.SetSwordSlash(0, 1.68f, -90);
        else if (Input.GetKey(KeyCode.DownArrow) && player.isJumping)
            player.fx.swordSlashFX.SetSwordSlash(0, -1.8f, 90);
        else
            player.fx.swordSlashFX.SetSwordSlash(1.2f, 0.265f, player.facingDir == 1 ? 180 : 0);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
            stateMachine.ChangeState(player.idleState);
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }
}
