using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprRender;

    //private Player_Base playerBase;
    private Rigidbody2D rigid;
    private Animator animator;
    private TrailRenderer trailRender;

    private Vector2 moveVec;
    public float curMoveSpeed = 5f;
    public const float MOVE_SPEED = 5f;
    public const float DASH_SPEED = 20f;
    private bool isDashing = false;

    private void Awake()
    {
        //playerBase = GetComponent<Player_Base>();
        rigid = GetComponent<Rigidbody2D>();
        //sprRender = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        trailRender = GetComponentInChildren<TrailRenderer>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        animator.SetFloat("Speed", moveVec.magnitude);

        if (moveVec.x != 0)
        {
            sprRender.flipX = moveVec.x < 0;
        }
    }

    private void OnMove(InputValue value)
    {
        moveVec = value.Get<Vector2>();
    }

    private void OnDash()
    {
        HandleDash();
    }

    private void HandleMovement()
    {
        rigid.MovePosition(rigid.position + moveVec * curMoveSpeed * Time.fixedDeltaTime);
    }

    private void HandleDash()
    {
        if (!isDashing)
        {
            isDashing = true;
            curMoveSpeed = DASH_SPEED;
            trailRender.emitting = true;
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .3f;
        yield return new WaitForSeconds(dashTime);
        curMoveSpeed = MOVE_SPEED;
        trailRender.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
