using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUnitMovement: MonoBehaviour
{
    [SerializeField] private CharacterAnimation characterAnimation;

    public float curMoveSpeed = 5f;
    public const float MOVE_SPEED = 5f;
    public const float DASH_SPEED = 20f;

    private Rigidbody2D rigid;
    private Vector2 moveVec;
    private bool isDashing = false;

    public bool IsDefencing { get; set; }

    private void Awake()
    {
        //playerControls.Player.FireBulletToTarget.performed += OnFire; // example

        rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        characterAnimation.PlayMoveAnimation(moveVec);
        HandleDefence();
    }

    private void HandleMovement()
    {
        rigid.MovePosition(rigid.position + moveVec * curMoveSpeed * Time.fixedDeltaTime);
    }

    public void HandleDefence()
    {
        characterAnimation.PlayDefenseAnimation(IsDefencing);
    }

    public void SetMoveVector(Vector2 moveVec)
    {
        this.moveVec = moveVec;
    }

    public void Fire()
    {
        characterAnimation.PlayFireAnimation();
    }

    public void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            curMoveSpeed = DASH_SPEED;
            characterAnimation.PlayTrailAnimation(true);
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .3f;
        yield return new WaitForSeconds(dashTime);
        curMoveSpeed = MOVE_SPEED;
        characterAnimation.PlayTrailAnimation(false);
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
