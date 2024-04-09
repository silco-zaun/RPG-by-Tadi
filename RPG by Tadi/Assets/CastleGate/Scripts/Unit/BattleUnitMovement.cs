
using UnityEngine;
using Tadi.Datas.Combat;
using UnityEditor.Animations;
using Tadi.Datas.Unit;

public class BattleUnitMovement : MonoBehaviour
{
    private UnitAnimation anim;

    private State state;
    private float slideSpeed = 10f;
    private Vector3 actorPosition;
    private Vector3 targetPosition;
    private float reachedDistance = 1f;
    private Transform bullet;
    private AnimatorController bulletAnimator;

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
        anim = GetComponentInChildren<UnitAnimation>();
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

    public void ExcuteAttack(UnitController actor, UnitController target, System.Action OnAttackTarget, System.Action OnTurnComplete)
    {
        actorPosition = transform.position;
        targetPosition = target.transform.position;
        bulletAnimator = actor.BulletAnim;

        if (actor.AttackType == AttackType.Melee)
        {
            MeleeAttackMovement(OnAttackTarget, OnTurnComplete);
        }
        else if (actor.AttackType == AttackType.Ranged)
        {
            RangedAttackMovement(OnAttackTarget, OnTurnComplete);
        }
    }

    public void ExcuteSkillAttack(UnitController actor, UnitController target, UnitCombatSkill skill, System.Action OnAttackTarget, System.Action OnTurnComplete)
    {
        actorPosition = transform.position;
        targetPosition = target.transform.position;
        bulletAnimator = skill.BulletAnim;

        if (skill.AttackType == AttackType.Melee)
        {
            MeleeAttackMovement(OnAttackTarget, OnTurnComplete);
        }
        else if (skill.AttackType == AttackType.Ranged)
        {
            RangedAttackMovement(OnAttackTarget, OnTurnComplete);
        }
    }

    public void MeleeAttackMovement(System.Action OnAttackTarget, System.Action OnTurnComplete)
    {
        Vector3 attackDir = (targetPosition - actorPosition).normalized;
        Vector3 slideTargetPosition = targetPosition - attackDir * reachedDistance;

        // Slide to Target
        SlideUnitToTarget(slideTargetPosition,
            () =>
            {
                // Arrived at Target, attack him
                state = State.Busy;

                anim.PlayFireAnim(null,
                    () =>
                    {
                        OnAttackTarget();

                        // MeleeAttackMovement completed, slide back
                        SlideUnitToTarget(actorPosition,
                                () =>
                                {
                                    // Slide back completed, back to idle
                                    state = State.Idle;
                                    anim.PlayMoveAnim(Vector2.zero);
                                    OnTurnComplete();
                                });
                    });
            });
    }

    public void RangedAttackMovement(System.Action OnAttackTarget, System.Action OnTurnComplete)
    {
        // PhysicalAttack him
        state = State.FireBullet;

        anim.PlayFireAnim(null,
            () =>
            {
                FireBulletToTarget(
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
        transform.position += (targetPosition - transform.position) * slideSpeed * Time.fixedDeltaTime;

        bool isArriving = Vector3.Distance(transform.position, targetPosition) < 0.1f;

        if (isArriving)
        {
            // Arrived at Slide Target Position
            transform.position = targetPosition;
            OnSlideComplete();
        }
    }

    private void SlideUnitToTarget(Vector3 targetPosition, System.Action OnSlideComplete)
    {
        this.targetPosition = targetPosition;
        this.OnSlideComplete = OnSlideComplete;
        state = State.Sliding;
    }

    private void FireBulletToTarget(System.Action OnFireComplete)
    {
        bullet = Managers.Ins.Res.GetObjectFromPool((int)ResourceManager.ResPrefabIndex.Bullet).transform;
        bullet.GetComponent<BulletController>().SetBullet(bulletAnimator, transform.position, targetPosition, OnFireComplete);
    }

    public void PlayDeathAnimation()
    {
        anim.PlayDeathAnim();
    }

    public void PlayDefenseAnimation(bool defending)
    {
        anim.PlayDefenseAnim(defending);
    }

    public void PlayDefenseAnimation(bool defending, System.Action OnTurnComplete)
    {
        anim.PlayDefenseAnim(defending);
        OnTurnComplete();
    }

    public void RotateCharacter(bool isFacingLeft)
    {
        anim.RotateCharacter(isFacingLeft);
    }
}
