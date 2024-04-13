using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyUnitAI : MonoBehaviour
{
    public enum State
    {
        Roaming,
        ChaseTarget,
        GoBack
    }

    [SerializeField] private LayerMask solidLayer;

    private UnitAnimation anim;
    private EnemyUnitNavMesh nav;
    private EnemyUnitDetector detector;

    public State state;
    private Vector3 startingPosition;
    private Vector3 moveVec;
    private const float ROAM_RANGE = 3f;
    private bool isMoving;
    public float arrivalThreshold = 0.1f;

    private void Awake()
    {
        anim = GetComponentInChildren<UnitAnimation>();
        nav = GetComponent<EnemyUnitNavMesh>();
        detector = GetComponent<EnemyUnitDetector>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        state = State.Roaming;
        startingPosition = transform.position;

        Vector3 roamingPos = GetRoamingPosition();

    }

    private void FixedUpdate()
    {
        FindTarget();

        switch (state)
        {
            case State.Roaming:
                HandleRoaming();
                break;
            case State.ChaseTarget:
                MissTarget();
                HandleChasing();
                break;
            case State.GoBack:
                ArriveStartingPos();
                HandleGoBack();
                break;
        }
    }

    private void LateUpdate()
    {
        anim.PlayMoveAnim(moveVec);
    }

    private void HandleRoaming()
    {
        if (!isMoving)
        {
            StartCoroutine(Roaming());
        }
    }

    private void HandleChasing()
    {
        if (detector.Target != null)
        {
            nav.SetDestination(detector.Target.transform.position, ref moveVec);
        }
    }

    private void HandleGoBack()
    {
        if (Vector3.Distance(transform.position, startingPosition) < arrivalThreshold)
        {
            state = State.Roaming;
        }
        else
        {
            nav.SetDestination(startingPosition, ref moveVec);
        }
    }

    public IEnumerator Roaming()
    {
        isMoving = true;

        Vector3 target = GetRoamingPosition();

        // Set the agent's destination to the random point
        nav.SetDestination(target, ref moveVec);

        // Wait until the character has reached the destination
        while (Vector3.Distance(transform.position, target) > arrivalThreshold)
        {
            yield return null; // Wait for the next frame
        }

        // Character has arrived at the destination
        moveVec = Vector3.zero;

        yield return new WaitForSeconds(2f);

        isMoving = false;
    }

    private Vector3 GetRoamingPosition()
    {
        // Get a random direction
        Vector3 movePosition;

        if (Vector2.Distance((Vector2)startingPosition, (Vector2)transform.position) < 1f)
        {
            movePosition = new Vector3(startingPosition.x - 1f, startingPosition.y, 0);
        }
        else
        {
            movePosition = startingPosition;
        }

        return movePosition;
    }

    private void FindTarget()
    {
        if (detector.PlayerDetected)
        {
            state = State.ChaseTarget;
        }
    }

    private void MissTarget()
    {
        if (!detector.PlayerDetected)
        {
            state = State.GoBack;
        }
    }

    private void ArriveStartingPos()
    {
        if (Vector3.Distance(startingPosition, transform.position) < arrivalThreshold)
        {
            state = State.Roaming;
        }
    }
}
