using System;
using System.Collections;
using Tadi.Interface.unit;
using UnityEngine;

public class UnitAIRoaming : MonoBehaviour
{
    private enum RoamingState
    {
        None,
        Moving,
        Arrive,
        Waiting
    }

    private Vector3 startingPosition;
    private Vector3 roamingPos;
    private float arrivalThreshold = 0.1f;

    private RoamingState state;

    private UnitAIMovement move;

    private void Awake()
    {
        move = GetComponent<UnitAIMovement>();
    }

    private void Start()
    {
        startingPosition = transform.position;
        roamingPos = transform.position;
    }

    public void HandleRoaming()
    {
        switch (state)
        {
            case RoamingState.None:
                MoveToRaomingPos();
                break;
            case RoamingState.Moving:
                CheckIsMoving();
                break;
            case RoamingState.Arrive:
                Waiting();
                break;
            case RoamingState.Waiting:
                break;
        }
    }

    private void MoveToRaomingPos()
    {
        state = RoamingState.Moving;

        roamingPos = GetRoamingPosition();

        // Set the agent's destination to the random point
        move.PlayMoveAnim(roamingPos);
        move.Move(roamingPos);
    }

    private void CheckIsMoving()
    {
        float distance = Vector2.Distance((Vector2)transform.position, (Vector2)roamingPos);

        //while (move.IsArrive() == false)
        if (distance < arrivalThreshold)
        {
            state = RoamingState.Arrive;
        }
    }

    private void Waiting()
    {
        state = RoamingState.Waiting;

        StartCoroutine(WaitingRoutine());
    }

    private IEnumerator WaitingRoutine()
    {
        // Character has arrived at the destination
        move.PlayStopAnim();

        yield return new WaitForSeconds(2f);

        state = RoamingState.None;
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

}
