using System;
using System.Collections;
using Tadi.Datas.BattleSystem;
using Tadi.Datas.Unit;
using Tadi.Interface.unit;
using UnityEngine;
using static Tadi.Interface.unit.IInteractable;

public class UnitAI : MonoBehaviour
{
    public enum UnitBehave
    {
        Roaming,
        ChasingTarget,
        LookingTarget,
        MissingTarget,
        Talking,
        Battle
    }

    [SerializeField] private bool disappearWhenTalkFinish = false;

    protected UnitBehave behave;
    protected UnitAIDetector detector;
    protected UnitAIMovement move;

    private UnitAIRoaming roaming;
    private int detectorIndex;
    private Vector3 startingPosition;
    private Vector3 roamingTargetPos;
    private bool isMoving = false;
    private bool isWaiting = false;
    private float arrivalThreshold = 0.1f;

    public UnitType UnitType { get; set; }

    protected Action<BattleCondition> OnBattleEnd;

    private void Awake()
    {
        detector = GetComponent<UnitAIDetector>();
        move = GetComponent<UnitAIMovement>();
        roaming = GetComponent<UnitAIRoaming>();
        startingPosition = transform.position;
        roamingTargetPos = transform.position;
    }

    // Start is called before the first frame update
    private void Start()
    {
        startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        HandleDetected();
        HandleNotDetected();
    }

    protected void HandleDetected()
    {
        GameObject target = detector.GetDetectedTarget(detectorIndex);

        if (target != null)
        {
            switch (behave)
            {
                case UnitBehave.LookingTarget:
                    move.PlayLookAnim(target.transform.position);
                    break;
                case UnitBehave.ChasingTarget:
                    move.PlayMoveAnim(target.transform.position);
                    move.Move(target.transform.position);
                    break;
                case UnitBehave.Talking:
                    break;
            }
        }
    }

    protected void HandleNotDetected()
    {
        switch (behave)
        {
            case UnitBehave.Roaming:
                roaming.HandleRoaming();
                break;
            case UnitBehave.MissingTarget:
                HandleGoingBack();
                break;
        }
    }

    protected void HandleRoaming()
    {
        // 1. If waiting end, move to roaming position
        if (isMoving == false && isWaiting == false)
        {
            isMoving = true;

            roamingTargetPos = GetRoamingPosition();

            // Set the agent's destination to the random point
            move.PlayMoveAnim(roamingTargetPos);
            move.Move(roamingTargetPos);
        }

        // 2. Check arriving roaming position

        Debug.Log(move.IsArrive());

        // Wait until the character has reached the destination
        float distance = Vector2.Distance((Vector2)transform.position, (Vector2)roamingTargetPos);

        // 3. If arrive, wait for 2 seconds
        //while (move.IsArrive() == false)
        if (distance < arrivalThreshold && isWaiting == false)
        {
            StartCoroutine(StopAndWaitRoutine());
        }
    }

    private IEnumerator StopAndWaitRoutine()
    {
        isMoving = false;
        isWaiting = true;

        // Character has arrived at the destination
        move.PlayStopAnim();

        yield return new WaitForSeconds(2f);

        isWaiting = false;
    }

    protected void HandleGoingBack()
    {
        if (Vector3.Distance(transform.position, startingPosition) < arrivalThreshold)
        {
            move.ArriveStartingPos();
            behave = UnitBehave.Roaming;
        }
        else
        {
            move.PlayMoveAnim(startingPosition);
        }
    }

    private Vector3 GetRoamingPosition()
    {
        Vector3 movePosition;
        bool arriveStartingPos = Vector2.Distance((Vector2)transform.position, (Vector2)startingPosition) < arrivalThreshold;

        if (arriveStartingPos)
        {
            movePosition = new Vector3(startingPosition.x - 1f, startingPosition.y, 0);
        }
        else
        {
            movePosition = startingPosition;
        }

        return movePosition;
    }

    private bool DetectTarget(int detectorIndex)
    {
        GameObject target = detector.GetDetectedTarget(detectorIndex);
        bool targetDetected = false;

        if (target != null)
        {
            targetDetected = true;
        }

        return targetDetected;
    }

    protected void LookingTarget(int detectorIndex)
    {
        this.detectorIndex = detectorIndex;
        behave = UnitBehave.LookingTarget;
        move.ActivateBalloon(true);
    }

    protected void ChasingTarget(int detectorIndex)
    {
        this.detectorIndex = detectorIndex;
        behave = UnitBehave.ChasingTarget;
        move.ActivateBalloon(true);
    }

    protected void MissTarget(int detectorIndex)
    {
        this.detectorIndex = detectorIndex;
        behave = UnitBehave.MissingTarget;
        move.MissTarget(startingPosition);
        move.Move(startingPosition);
    }

    protected void Talk(int detectorIndex, GameObject target)
    {
        this.detectorIndex = detectorIndex;
        behave = UnitBehave.Talking;
        Managers.Ins.Dlg.StartDialog(UnitType, TalkFinish);
        //Managers.Ins.Dlg.StartDialog(dialog, move.TalkFinish);
        move.Talk();

        target.GetComponent<IInteractable>().Interact(InteractState.ShowDialog);
    }

    protected void Battle(int detectorIndex, GameObject target)
    {
        this.detectorIndex = detectorIndex;
        behave = UnitBehave.Battle;

        PlayerUnitController player = target.GetComponent<PlayerUnitController>();
        EnemyUnitController enemy = GetComponent<EnemyUnitController>();

        Managers.Ins.Stat.BattleStart(player.BattleUnitInfo, enemy.BattleUnitInfo, enemy.gameObject, OnBattleEnd);
    }

    private void TalkFinish()
    {
        move.TalkFinish();

        if (disappearWhenTalkFinish)
        {
            Destroy(gameObject);
        }
    }
}
