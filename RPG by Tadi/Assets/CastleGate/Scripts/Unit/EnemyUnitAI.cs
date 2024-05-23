using Tadi.Datas.BattleSystem;
using UnityEngine;

public class EnemyUnitAI : UnitAI
{
    [SerializeField] private GameObject deactivateObject; 

    // Start is called before the first frame update
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        OnBattleEnd = BattleEnd;

        behave = UnitBehave.Roaming;

        detector.AddDetector(5f, OnEnterFOVDetector, OnExitFOVDetector);
        detector.AddDetector(1f, OnEnterContactingDetector, OnExitContactingDetector);
    }

    private void OnEnterFOVDetector(GameObject target)
    {
        ChasingTarget(0);
    }

    private void OnExitFOVDetector()
    {
        MissTarget(0);
    }

    private void OnEnterContactingDetector(GameObject target)
    {
        Battle(1, target);
    }

    private void OnExitContactingDetector()
    {
        LookingTarget(1);
    }

    private void BattleEnd(BattleCondition result)
    {
        if (result == BattleCondition.Win)
        {
            if (deactivateObject != null)
                deactivateObject.SetActive(false);
        }
    }
}
