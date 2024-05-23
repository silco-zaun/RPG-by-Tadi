
using UnityEngine;
using Tadi.Datas.Unit;

[RequireComponent(typeof(Animator))]
public class UnitAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprBody;
    [SerializeField] private SpriteRenderer sprLeftHand;
    [SerializeField] private SpriteRenderer sprRightHand;
    [SerializeField] private SpriteRenderer sprLeftWeapon;
    [SerializeField] private SpriteRenderer sprRightWeapon;
    [SerializeField] private TrailRenderer trailRender;

    private Animator animator;
    private bool isFacingLeft = false;
    private bool isDefending = false;

    public System.Action OnAnimStart;
    public System.Action OnAnimComplete;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Init(UnitAnimRes res)
    {
        animator.runtimeAnimatorController = res.Animator;
        sprBody.sprite = res.Body;
        sprLeftHand.sprite = res.LeftHand;
        sprRightHand.sprite = res.RightHand;
        sprLeftWeapon.sprite = res.LeftWeapon;
        sprRightWeapon.sprite = res.RightWeapon;

        PlayMoveAnim(Vector2.zero);
    }

    public void OnAttackAnimStart(string name)
    {
        OnAnimStart?.Invoke();
    }

    public void OnAttackAnimComplete(string name)
    {
        OnAnimComplete?.Invoke();
    }

    public void PlayMoveAnim(Vector2 moveVec)
    {
        animator.SetFloat("Speed", moveVec.magnitude);

        if (moveVec.x != 0)
        {
            bool isLeftDir = moveVec.x < 0;

            if (isLeftDir != isFacingLeft)
            {
                RotateCharacter(isLeftDir);
            }
        }
    }

    public void PlayLookAnim(Vector2 moveVec)
    {
        if (moveVec.x != 0)
        {
            bool isLeftDir = moveVec.x < 0;

            if (isLeftDir != isFacingLeft)
            {
                RotateCharacter(isLeftDir);
            }
        }
    }

    public void PlayFireAnim(System.Action OnAnimStart, System.Action OnAnimComplete)
    {
        PlayFireAnim();

        this.OnAnimStart = OnAnimStart;
        this.OnAnimComplete = OnAnimComplete;
    }

    public void PlayFireAnim()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDefenseAnim(bool defending)
    {
        // 방어 상태가 바뀌었으면
        if (defending != isDefending)
        {
            if (defending)
            {
                animator.SetTrigger("Defense");
            }

            isDefending = defending;
            animator.SetBool("IsDefending", isDefending);
        }
    }

    public void PlayDeathAnim()
    {
        animator.SetTrigger("Death");
    }

    public void PlayTrailAnim(bool emitting)
    {
        trailRender.emitting = emitting;
    }
    
    public void RotateCharacter(bool isFacingLeft)
    {
        //Vector3 mousePos = Input.mousePosition;
        //float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        this.isFacingLeft = isFacingLeft;

        if (isFacingLeft)
        {
            transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
