using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class Player : Entity
{
    [Header("BaseParameter")]
    public float moveSpeed;
    public float jumpForce;

    [Header("Jump")]
    public bool isJumping;
    private int maxFallSpeed = 50;
    public float jumpTime;
    public float jumpTimer;
    public float jumpCutGravityMult;
    public bool isWallJumping;

    [Header("Checker")]
    public Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatGround;
    public Transform wallCheck;
    [SerializeField] private float wallCheckDistance;

    public PlayerFX fx { get; private set; }

    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerWalkState walkState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSliderState wallSliderState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerLookUpState lookUpState { get; private set; }
    public PlayerLookDownState lookDownState { get; private set; }
    #endregion
    


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        walkState = new PlayerWalkState(this, stateMachine, "Walk");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallSliderState = new PlayerWallSliderState(this, stateMachine, "WallSlider");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
        lookUpState = new PlayerLookUpState(this, stateMachine, "LookUp");
        lookDownState = new PlayerLookDownState(this, stateMachine, "LookDown");

    }

    protected override void Start()
    {
        base.Start();
        fx = GetComponent<PlayerFX>(); 
        stateMachine.Initialize(idleState);
        gravityScale = rb.gravityScale;
    }

    protected override void Update()
    {
        base.Update();

        SetGravity();
        stateMachine.currentState.Update();

        jumpTimer -= Time.deltaTime * 2f;
        JumpContorller();

    }

    private void JumpContorller()
    {
        if (Input.GetKeyDown(KeyCode.Z) && isGroundDetected())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * 2f);
        }
        if (isJumping)
        {
            if (Input.GetKeyUp(KeyCode.Z) && rb.velocity.y > 0)
            {
                jumpTimer = 0;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                isJumping = false;
            }
            jumpTimer = Mathf.Clamp(jumpTimer, 0f, 1f);

            if (jumpTimer > 0)
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * (jumpTimer * 7f));
            else
                rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y);
        }      
    }

        private void SetGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * jumpCutGravityMult;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = gravityScale;         
        }
    }

    #region CollisionChecker
    public bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatGround);
    public bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right, wallCheckDistance, whatGround);
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }
    #endregion

}
