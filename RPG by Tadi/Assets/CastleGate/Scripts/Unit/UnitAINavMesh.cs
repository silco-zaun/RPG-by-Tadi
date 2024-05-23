using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitAINavMesh : MonoBehaviour
{
    [SerializeField] private bool showPath;
    [SerializeField] private bool showAhead;

    private NavMeshAgent agent;
    private float moveSpeed = 3f; // Desired move speed in AIUnits per second

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // 한번만 호출하면 됨
    public void SetDestination(Vector3 target)
    {
        agent.SetDestination(target);
    }

    public bool IsArrive()
    {
        // Check if the NavMeshAgent has arrived at its destination
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        DrawGizmos(agent, showPath, showAhead);
    }

    private void DrawGizmos(NavMeshAgent agent, bool showPath, bool showAhead)
    {
        if (Application.isPlaying && agent != null)
        {
            if (showPath && agent.hasPath)
            {
                var corners = agent.path.corners;
                if (corners.Length < 2) { return; }
                int i = 0;
                for (; i < corners.Length - 1; i++)
                {
                    Debug.DrawLine(corners[i], corners[i + 1], Color.blue);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(agent.path.corners[i + 1], 0.03f);
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(agent.path.corners[i], agent.path.corners[i + 1]);
                }
                Debug.DrawLine(corners[0], corners[1], Color.blue);
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(agent.path.corners[1], 0.03f);
                Gizmos.color = Color.red;
                Gizmos.DrawLine(agent.path.corners[0], agent.path.corners[1]);
            }

            if (showAhead)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawRay(agent.transform.position, agent.transform.up * 0.5f);
            }
        }
    }
}
