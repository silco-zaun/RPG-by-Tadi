using UnityEngine;

public class NPCUnitAI : UnitAI
{
    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        behave = UnitBehave.Roaming;

        detector.AddDetector(5f, OnEnterFOVDetector, OnExitFOVDetector);
        detector.AddDetector(1f, OnEnterContactingDetector, OnExitContactingDetector);
    }

    private void OnEnterFOVDetector(GameObject target)
    {
        LookingTarget(0);
    }

    private void OnExitFOVDetector()
    {
        MissTarget(0);
    }

    private void OnEnterContactingDetector(GameObject target)
    {
        Talk(1, target);
    }

    private void OnExitContactingDetector()
    {
        LookingTarget(1);
    }
}
