using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprBody;
    [SerializeField] private SpriteRenderer sprLeftHand;
    [SerializeField] private SpriteRenderer sprLeftWeapon;
    [SerializeField] private SpriteRenderer sprRightHand;
    [SerializeField] private SpriteRenderer sprRightWeapon;
    [SerializeField] private TrailRenderer trailRender;

    public float curMoveSpeed = 5f;
    public const float MOVE_SPEED = 5f;
    public const float DASH_SPEED = 20f;

    private PlayerControls playerControls;
    private Rigidbody2D rigid;
    private Vector2 moveVec;
    private bool isFacingLeft = false;
    private bool isDefencing = false;
    private bool isDashing = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        //playerControls.Player.Fire.performed += OnFire; // example

        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        //playerControls.Player.Fire.Enable(); // example
        playerControls.Enable();
    }

    private void OnDisable()
    {
        //playerControls.Player.Fire.Disable(); // example
        playerControls.Disable();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        HandleMovementAnim();
        HandleDefence();
    }

    private void OnMove(InputValue value)
    {
        moveVec = value.Get<Vector2>();
    }

    private void OnFire()
    {
        Fire();
    }

    private void OnDefence()
    {
        animator.SetTrigger("Defence");
    }

    private void OnDash()
    {
        Dash();
    }

    private void HandleMovement()
    {
        rigid.MovePosition(rigid.position + moveVec * curMoveSpeed * Time.fixedDeltaTime);
    }

    private void HandleMovementAnim()
    {
        animator.SetFloat("Speed", moveVec.magnitude);

        if (moveVec.x != 0)
        {
            bool isLeftDir = moveVec.x < 0;

            if (isLeftDir != isFacingLeft)
            {
                isFacingLeft = isLeftDir;

                RotateCharacter();
            }
        }
    }

    private void HandleDefence()
    {
        bool isDefenceBtnPressed = playerControls.Player.Defence.IsPressed();

        if (isDefenceBtnPressed != isDefencing)
        {
            isDefencing = isDefenceBtnPressed;
            animator.SetBool("IsDefencing", isDefencing);
        }
    }

    private void Fire()
    {
        animator.SetTrigger("SwordAttack");
    }
    
    private void Dash()
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

    private void RotateCharacter()
    {
        Vector3 mousePos = Input.mousePosition;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (isFacingLeft)
        {
            character.rotation = Quaternion.Euler(0, -180, 0);
            //weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            character.rotation = Quaternion.Euler(0, 0, 0);
            //weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
