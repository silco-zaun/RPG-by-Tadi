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
    private bool isDefencing = false;

    //public UnityAnimationEvent OnAnimationStart;
    //public UnityAnimationEvent OnAnimationComplete;

    public System.Action OnAnimationStart;
    public System.Action OnAnimationComplete;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetAnimData(CharacterBaseData data)
    {
        animator.runtimeAnimatorController = AnimationManager.Instance.AnimatorControllers[(int)data.Character];
        sprBody.sprite = data.SprBody;
        sprLeftHand.sprite = data.SprLeftHand;
        sprLeftWeapon.sprite = data.SprLeftWeapon;
        sprRightHand.sprite = data.SprRightHand;
        sprRightWeapon.sprite = data.SprRightWeapon;
    }

    public void HandleKnightSwordAttackAnimationStart(string name)
    {
        Debug.Log($"{name} animation start.");
        //OnAnimationStart?.Invoke(name);
        OnAnimationStart?.Invoke();
    }

    public void HandleKnightSwordAttackAnimationComplete(string name)
    {
        Debug.Log($"{name} animation complete.");
        //OnAnimationStart?.Invoke(name);
        OnAnimationComplete?.Invoke();
    }

    public void HandleOrcSwordAttackAnimationStart(string name)
    {
        Debug.Log($"{name} animation start.");
        //OnAnimationStart?.Invoke(name);
        OnAnimationStart?.Invoke();
    }

    public void HandleOrcSwordAttackAnimationComplete(string name)
    {
        Debug.Log($"{name} animation complete.");
        //OnAnimationStart?.Invoke(name);
        OnAnimationComplete?.Invoke();
    }

    public void PlayMoveAnim(Vector2 moveVec)
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

    public void PlayDefenceAnim(bool defencing)
    {
        // 방어 상태가 바뀌었으면
        if (defencing != isDefencing)
        {
            if (defencing)
            {
                animator.SetTrigger("Defence");
            }

            isDefencing = defencing;
            animator.SetBool("IsDefencing", isDefencing);
        }
    }

    public void PlayFireAnim(System.Action OnAnimationStart, System.Action OnAnimationComplete)
    {
        PlayFireAnim();

        this.OnAnimationStart = OnAnimationStart;
        this.OnAnimationComplete = OnAnimationComplete;
    }

    public void PlayFireAnim()
    {
        animator.SetTrigger("SwordAttack");
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
            //weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            //weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
