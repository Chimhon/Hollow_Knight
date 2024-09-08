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
    public float attackDuraition = 0.2f;

    [Header("Jump")]
    public bool isJumping;
    private int maxFallSpeed = 50;
    public float jumpTime;
    public float jumpCutGravityMult;
    public bool isWallJumping;

    [Header("Checker")]
    public Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatGround;
    public Transform wallCheck;
    [SerializeField] private float wallCheckDistance;


    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerWallSliderState wallSliderState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    #endregion
    

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        wallSliderState = new PlayerWallSliderState(this, stateMachine, "WallSlider");

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        gravityScale = rb.gravityScale;
    }

    protected override void Update()
    {
        base.Update();

        SetGravity();

        stateMachine.currentState.Update();

        attackDuraition -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.X) && attackDuraition <=0)
            stateMachine.ChangeState(attackState);
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


    //public void CreateSwordAttack(float Xrotation,float Yposition)
    //{
    //    Quaternion Rotation = Quaternion.Euler(Xrotation, -90, 90);
    //    Rotation.Normalize();
    //    SwordSlash.GetComponent<Transform>().rotation = Rotation;
    //    Transform swordTransform = Transform.Instantiate(SwordSlash.transform, new Vector2(SwordSlash.transform.position.x, SwordSlash.transform.position.y + Yposition), Quaternion.identity);
    //    SwordSlash.GetComponent<Transform>().position = swordTransform.position;
    //    SwordSlash.Play();
    //}

    //public void CreateSwordNoDir(float Xposition)
    //{

    //    Quaternion Rotation = Quaternion.Euler(facingDir == -1 ? 0 : 180, -90, 90);
    //    SwordSlash.GetComponent<Transform>().rotation = Rotation;
    //    Transform swordTransform = Transform.Instantiate(SwordSlash.transform, new Vector2(SwordSlash.transform.position.x + Xposition * facingDir, SwordSlash.transform.position.y),Quaternion.identity);
    //    SwordSlash.GetComponent<Transform>().position = swordTransform.position;
    //    SwordSlash.Play();
    //}




}
