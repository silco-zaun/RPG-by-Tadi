using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleUnitMovement : MonoBehaviour
{
    private UnitAnimation characterAnimation;
    //private GameObject targetUnitObject;

    private State state;
    private float slideSpeed = 10f;
    //private float bulletInitSpeed = 10f;
    //private float bulletCurSpeed = 10f;
    //private float bulletAccel = 50f;
    private Vector3 actorPosition;
    private Vector3 slideTargetPosition;
    //private Vector3 fireTargetPosition;
    private float reachedDistance = 1f;
    private Transform bullet;

    private System.Action OnSlideComplete;

    private enum State
    {
        Idle,
        Sliding,
        FireBullet,
        Attack,
        Busy,
    }

    private void Awake()
    {
        actorPosition = transform.position;
        characterAnimation = GetComponentInChildren<UnitAnimation>();
    }

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Sliding:
                HandleUnitSliding();
                break;
            case State.FireBullet:
                break;
        }
    }

    public void ExcuteAttack(Tadi.Datas.Combat.AttackType attackType, Tadi.Datas.Weapon.BulletType bulletType, Vector3 targetPosition, System.Action OnAttackTarget, System.Action OnTurnComplete)
    {
        if (attackType == Tadi.Datas.Combat.AttackType.Melee)
        {
            ExcuteMeleeAttack(targetPosition, OnAttackTarget, OnTurnComplete);
        }
        else if (
            attackType == Tadi.Datas.Combat.AttackType.Ranged)
        {
            ExcuteRangedAttack(bulletType, targetPosition, OnAttackTarget, OnTurnComplete);
        }
    }

    public void ExcuteMeleeAttack(Vector3 targetPosition, System.Action OnAttackTarget, System.Action OnTurnComplete)
    {
        Vector3 attackDir = (targetPosition - actorPosition).normalized;
        Vector3 slideTargetPosition = targetPosition - attackDir * reachedDistance;

        // Slide to Target
        SlideUnitToTarget(slideTargetPosition,
            () =>
            {
                // Arrived at Target, attack him
                state = State.Busy;

                characterAnimation.PlayFireAnim(null,
                    () =>
                    {
                        OnAttackTarget();

                        // ExcuteMeleeAttack completed, slide back
                        SlideUnitToTarget(actorPosition,
                                () =>
                                {
                                    // Slide back completed, back to idle
                                    state = State.Idle;
                                    characterAnimation.PlayMoveAnim(Vector2.zero);
                                    OnTurnComplete();
                                });
                    });
            });
    }

    public void ExcuteRangedAttack(Tadi.Datas.Weapon.BulletType bulletType, Vector3 targetPosition, System.Action OnAttackTarget, System.Action OnTurnComplete)
    {
        // Attack him
        state = State.FireBullet;

        characterAnimation.PlayFireAnim(null,
            () =>
            {
                FireBulletToTarget(bulletType, targetPosition,
                    () =>
                    {
                        state = State.Idle;
                        OnAttackTarget();
                        OnTurnComplete();
                    });
            });
    }

    private void HandleUnitSliding()
    {
        transform.position += (slideTargetPosition - transform.position) * slideSpeed * Time.fixedDeltaTime;

        bool isArriving = Vector3.Distance(transform.position, slideTargetPosition) < 0.1f;

        if (isArriving)
        {
            // Arrived at Slide Target Position
            transform.position = slideTargetPosition;
            OnSlideComplete();
        }
    }

    private void SlideUnitToTarget(Vector3 slideTargetPosition, System.Action OnSlideComplete)
    {
        this.slideTargetPosition = slideTargetPosition;
        this.OnSlideComplete = OnSlideComplete;
        state = State.Sliding;
    }

    private void FireBulletToTarget(Tadi.Datas.Weapon.BulletType bulletType, Vector3 fireTargetPosition, System.Action OnFireComplete)
    {
        bullet = Managers.Ins.Res.GetObjectFromPool((int)ResourceManager.ResPrefabIndex.Bullet).transform;
        bullet.GetComponent<BulletController>().SetBullet(bulletType, transform.position, fireTargetPosition, OnFireComplete);
    }

    public void PlayDeathAnimation()
    {
        characterAnimation.PlayDeathAnim();
    }

    public void PlayDefenseAnimation(bool defending)
    {
        characterAnimation.PlayDefenseAnim(defending);
    }

    public void PlayDefenseAnimation(bool defending, System.Action OnTurnComplete)
    {
        characterAnimation.PlayDefenseAnim(defending);
        OnTurnComplete();
    }

    public void RotateCharacter(bool isFacingLeft)
    {
        characterAnimation.RotateCharacter(isFacingLeft);
    }
}
