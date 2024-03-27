using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Animator))]
public class CharacterAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprBody;
    [SerializeField] private SpriteRenderer sprLeftHand;
    [SerializeField] private SpriteRenderer sprLeftWeapon;
    [SerializeField] private SpriteRenderer sprRightHand;
    [SerializeField] private SpriteRenderer sprRightWeapon;
    [SerializeField] private TrailRenderer trailRender;

    private Animator animator;
    private bool isFacingLeft = false;
    private bool isDefending = false;

    //public UnityAnimationEvent OnAnimationStart;
    //public UnityAnimationEvent OnAnimationComplete;

    public System.Action OnAnimationStart;
    public System.Action OnAnimationComplete;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimationData(CharacterSO data)
    {
        animator.runtimeAnimatorController = GameManager.Ins.Anim.CharacterAnimator[(int)data.CharacterType - 1];
        sprBody.sprite = data.SprBody;
        sprLeftHand.sprite = data.SprLeftHand;
        sprLeftWeapon.sprite = data.SprLeftWeapon;
        sprRightHand.sprite = data.SprRightHand;
        sprRightWeapon.sprite = data.SprRightWeapon;
    }

    public void OnAttackAnimationStart(string name)
    {
        OnAnimationStart?.Invoke();
    }

    public void OnAttackAnimationComplete(string name)
    {
        OnAnimationComplete?.Invoke();
    }

    public void PlayMoveAnimation(Vector2 moveVec)
    {
        animator.SetFloat("Speed", moveVec.magnitude);

        if (moveVec.x != 0)
        {
            bool isLeftDir = moveVec.x < 0;

            if (isLeftDir != isFacingLeft)
            {
                RotateCharacter(isFacingLeft);
            }
        }
    }

    public void PlayFireAnimation(System.Action OnAnimationStart, System.Action OnAnimationComplete)
    {
        PlayFireAnimation();

        this.OnAnimationStart = OnAnimationStart;
        this.OnAnimationComplete = OnAnimationComplete;
    }

    public void PlayFireAnimation()
    {
        animator.SetTrigger("Attack");
    }

    public void PlayDefenseAnimation(bool defending)
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

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("Death");
    }

    public void PlayTrailAnimation(bool emitting)
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
            //weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
