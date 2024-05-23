using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorSetting
{
    private static Color alpha = new Color(1f, 1f, 1f, 0.3f);

    public Transform DetectorOrigin { get; set; }
    public GameObject DetectedTarget { get; set; }
    public bool TargetDetected { get; set; }
    public Vector2 DirectionToTarget { get { return DetectedTarget.transform.position - DetectorOrigin.position; } }
    public bool Detecting { get; set; }

    public LayerMask TargetLayerMask { get; set; } = LayerMask.GetMask("Player");
    public Vector2 DetectorOriginOffset { get; set; } = Vector2.zero;
    public float DetectRadius { get; set; } = 5f;

    public Color GizmoDetectColor { get; set; } = Color.green * alpha;
    public Color GizmoDetectedColor { get; set; } = Color.red * alpha;

    public Action<GameObject> OnEnterDetector { get; set; }
    public Action OnExitDetector { get; set; }

    public DetectorSetting(Transform transform, float detectRadius, Action<GameObject> OnEnterDetector, Action OnExitDetector)
    {
        DetectorOrigin = transform;
        DetectRadius = detectRadius;
        this.OnEnterDetector = OnEnterDetector;
        this.OnExitDetector = OnExitDetector;
    }
}

public class UnitAIDetector : MonoBehaviour
{
    [SerializeField] private bool showGizmos = true;

    private List<DetectorSetting> settings = new List<DetectorSetting>();

    private void Start()
    {
        //StartCoroutine(DetectionCoroutine());
    }

    private void Update()
    {
        PerformDetection();
    }

    public void AddDetector(float detectRadius, Action<GameObject> OnEnterDetector, Action OnExitDetector)
    {
        DetectorSetting setting = new DetectorSetting(transform, detectRadius, OnEnterDetector, OnExitDetector);

        settings.Add(setting);
    }

    public GameObject GetDetectedTarget(int detectorIndex)
    {
        return settings[detectorIndex].DetectedTarget;
    }

    //private IEnumerator DetectionCoroutine()
    //{
    //    yield return new WaitForSeconds(detectionDelay);
    //
    //    PerformDetection();
    //    StartCoroutine(DetectionCoroutine());
    //}

    public void PerformDetection()
    {
        foreach (DetectorSetting setting in settings)
        {
            Collider2D collider = Physics2D.OverlapCircle((Vector2)setting.DetectorOrigin.position + setting.DetectorOriginOffset, setting.DetectRadius, setting.TargetLayerMask);

            if (collider != null)
            {
                if (setting.TargetDetected == false)
                    setting.OnEnterDetector?.Invoke(collider.gameObject);

                setting.TargetDetected = true;
                setting.DetectedTarget = collider.gameObject;
            }
            else
            {
                if (setting.TargetDetected == true)
                    setting.OnExitDetector?.Invoke();

                setting.TargetDetected = false;
                setting.DetectedTarget = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            foreach (DetectorSetting setting in settings)
            {
                if (setting.TargetDetected)
                    Gizmos.color = setting.GizmoDetectedColor;
                else
                    Gizmos.color = setting.GizmoDetectColor;

                Gizmos.DrawSphere((Vector2)setting.DetectorOrigin.position + setting.DetectorOriginOffset, setting.DetectRadius);
            }
        }
    }
}
