using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleUnitMovement : MonoBehaviour
{
    private CharacterAnimation characterAnimation;
    //private GameObject targetUnitObject;

    private State state;
    private float slideSpeed = 10f;
    private Vector3 behaviorPosition;
    private Vector3 slideTargetPosition;
    private float reachedDistance = 1f;

    private System.Action OnSlideComplete;

    private enum State
    {
        Idle,
        Sliding,
        Attack,
        Busy,
    }

    private void Awake()
    {
        behaviorPosition = transform.position;
        characterAnimation = GetComponentInChildren<CharacterAnimation>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Busy:
                break;
            case State.Sliding:
                HandleSliding();
                break;
        }
    }

    private void HandleSliding()
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

    public void Attack(Vector3 targetPosition, System.Action OnAttackTarget, System.Action OnBehaviorComplete)
    {
        Vector3 attackDir = (targetPosition - behaviorPosition).normalized;
        Vector3 slideTargetPosition = targetPosition - attackDir * reachedDistance;

        // Slide to Target
        SlideToPosition(slideTargetPosition,
            () =>
            {
                // Arrived at Target, attack him
                state = State.Busy;

                characterAnimation.PlayFireAnim(OnAttackTarget,
                    () =>
                    {
                        // Attack completed, slide back
                        SlideToPosition(behaviorPosition,
                            () =>
                            {
                                // Slide back completed, back to idle
                                state = State.Idle;
                                characterAnimation.PlayMoveAnim(Vector2.zero);
                                OnBehaviorComplete();
                            });
                    });
            });
    }

    private void SlideToPosition(Vector3 slideTargetPosition, System.Action OnSlideComplete)
    {
        this.slideTargetPosition = slideTargetPosition;
        this.OnSlideComplete = OnSlideComplete;
        state = State.Sliding;

        if (slideTargetPosition.x > 0)
        {
            //characterBase.PlayAnimSlideRight();
        }
        else
        {
            //characterBase.PlayAnimSlideLeft();
        }
    }

    public void RotateCharacter(bool isFacingLeft)
    {
        characterAnimation.RotateCharacter(isFacingLeft);
    }
}
