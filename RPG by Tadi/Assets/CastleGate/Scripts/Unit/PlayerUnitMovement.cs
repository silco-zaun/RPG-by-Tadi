using System.Collections;
using UnityEngine;

public class PlayerUnitMovement: MonoBehaviour
{
    [SerializeField] private UnitAnimation anim;
    [SerializeField] private LayerMask solidLayer;

    public float curMoveSpeed = 5f;
    public const float MOVE_SPEED = 5f;
    public const float DASH_SPEED = 20f;
    private Vector2 moveVec;
    private bool isDashing = false;

    public bool IsDefencing { get; set; }

    private void Awake()
    {
        //playerControls.Player.FireBulletToTarget.performed += OnFire; // example
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void LateUpdate()
    {
        anim.PlayMoveAnim(moveVec);
        HandleDefence();
    }

    private void HandleMovement()
    {
        if (moveVec != Vector2.zero)
        {
            Vector3 targetPos = transform.position + (Vector3)moveVec * curMoveSpeed * Time.fixedDeltaTime;

            //bool walking = IsWalkable(targetPos);

            //if (walking)
                transform.position = targetPos;
        }
    }

    public void HandleDefence()
    {
        anim.PlayDefenseAnim(IsDefencing);
    }

    public void SetMoveVector(Vector2 moveVec)
    {
        this.moveVec = moveVec;
    }

    public void Fire()
    {
        anim.PlayFireAnim();
    }

    public void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            curMoveSpeed = DASH_SPEED;
            anim.PlayTrailAnim(true);
            StartCoroutine(DashRoutine());
        }
    }

    private IEnumerator DashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .3f;
        yield return new WaitForSeconds(dashTime);
        curMoveSpeed = MOVE_SPEED;
        anim.PlayTrailAnim(false);
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }

    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, solidLayer) != null)
        {
            return false;
        }

        return true;
    }
}
