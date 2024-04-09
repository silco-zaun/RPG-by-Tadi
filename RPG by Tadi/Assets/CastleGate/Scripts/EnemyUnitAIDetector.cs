using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyUnitAIDetector : MonoBehaviour
{
    [Header("OverlapBox parameters")]
    [SerializeField] private Transform detectorOrigin;
    [SerializeField] private float detectorRadius = 10f;
    [SerializeField] private Vector2 detectorOriginOffset = Vector2.zero;
    [SerializeField] private float detectionDelay = 0.3f;
    [SerializeField] private LayerMask targetLayerMask;

    [Header("Gizmo parameters")]
    [SerializeField] private Color gizmoIdleColor = Color.green;
    [SerializeField] private Color gizmoDetectedColor = Color.red;
    [SerializeField] private bool showGizmos = true;

    public Vector2 DirectionToTarget => Target.transform.position - detectorOrigin.position;
    public UnityEvent<GameObject> OnPlayerDetected;

    public GameObject Target { get; private set; }
    public bool PlayerDetected { get; private set; }

    private void Start()
    {
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);

        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    public void PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)detectorOrigin.position + detectorOriginOffset, detectorRadius, targetLayerMask);
        
        if (collider != null)
        {
            PlayerDetected = true;
            Target = collider.gameObject;
            OnPlayerDetected?.Invoke(Target);
        }
        else
        {
            PlayerDetected = false;
            Target = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos && detectorOrigin != null)
        {
            if (PlayerDetected)
                Gizmos.color = gizmoDetectedColor;
            else
                Gizmos.color = gizmoIdleColor;

            Gizmos.DrawSphere((Vector2)detectorOrigin.position + detectorOriginOffset, detectorRadius);
        }
    }
}
