using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitAI : MonoBehaviour
{
    public enum State
    {
        Roaming,
        ChaseTarget,
        GoBack
    }

    [SerializeField] private UnitAnimation anim;
    [SerializeField] private LayerMask solidLayer;

    private EnemyUnitAIDetector detector;

    public State state;
    private Vector3 startingPosition;
    private Vector3 moveVec;
    private const float MOVE_SPEED = 1f;
    private const float WAIT_SECOND = 1f;
    private const float ROAM_RANGE = 3f;
    private bool isMoving;

    private void Awake()
    {
        detector = GetComponent<EnemyUnitAIDetector>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        state = State.Roaming;
        startingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Roaming:
                HandleRoaming();
                break;
            case State.ChaseTarget:
                HandleChasing();
                break;
            case State.GoBack:
                HandleGoBack();
                break;
        }

        FindTarget();
    }

    private void LateUpdate()
    {
        anim.PlayMoveAnim(moveVec);
    }

    private void HandleRoaming()
    {
        if (!isMoving)
        {
            moveVec = Tadi.Utils.Utils.GetRandomXDir();

            Vector3 movePos = transform.position + moveVec;

            bool walking = IsWalkable(movePos);

            if (walking)
                StartCoroutine(Move(movePos));
        }
    }

    private void HandleChasing()
    {
        if (!isMoving && detector.Target != null)
        {
            Vector3 dir = (detector.Target.transform.position - transform.position).normalized;
            moveVec = Tadi.Utils.Utils.GetStraightDir(dir);

            Vector3 movePos = transform.position + moveVec;

            bool walking = IsWalkable(movePos);

            if (walking)
                StartCoroutine(Move(movePos));
        }
    }

    private void HandleGoBack()
    {
        if (!isMoving)
        {
            Vector3 dir = (startingPosition - transform.position).normalized;

            moveVec = Tadi.Utils.Utils.GetStraightDir(dir);

            Vector3 movePos = transform.position + moveVec;

            bool walking = IsWalkable(movePos);

            if (walking)
                StartCoroutine(Move(movePos));
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + Tadi.Utils.Utils.GetRandomDir() * Random.Range(10f, 70f);
    }

    private IEnumerator Move(Vector3 movePos)
    {
        isMoving = true;

        while ((movePos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, movePos, MOVE_SPEED * Time.fixedDeltaTime);

            yield return null;
        }

        transform.position = movePos;
        moveVec = Vector3.zero;

        yield return new WaitForSeconds(WAIT_SECOND);

        isMoving = false;
    }

    private bool IsWalkable(Vector3 movePos)
    {
        if (Physics2D.OverlapCircle(movePos, 0.2f, solidLayer) != null)
        {
            return false;
        }

        return true;
    }

    private void FindTarget()
    {
        if (detector.PlayerDetected)
        {
            state = State.ChaseTarget;
        }
        else if (Vector3.Distance(startingPosition, transform.position) > ROAM_RANGE)
        {
            state = State.GoBack;
        }
        else if (Vector3.Distance(startingPosition, transform.position) < 0.1f)
        {
            state = State.Roaming;
        }
    }

    private void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox((Vector2)transform.position, Vector2.one, 0);

        if (collider != null)
        {
            Debug.Log(collider.gameObject.name);
        }
    }
}
