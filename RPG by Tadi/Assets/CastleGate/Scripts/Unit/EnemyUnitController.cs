using System.Collections.Generic;
using Tadi.Datas.BattleSystem;
using Tadi.Datas.Unit;
using Tadi.Interface.unit;
using UnityEngine;

public class EnemyUnitController : MonoBehaviour, IInteractable
{
    [SerializeField] private UnitType unitType;
    [SerializeField] private int unitLevel = 1;
    [SerializeField] private List<BattleUnitInfo> battleUnitInfo;

    private EnemyUnitAI unitAI;
    private UnitController unit;

    public List<BattleUnitInfo> BattleUnitInfo { get { return battleUnitInfo; } }

    private void Awake()
    {
        unitAI = GetComponent<EnemyUnitAI>();
        unit = GetComponentInChildren<UnitController>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        unit.Init(unitType, unitLevel);

        unitAI.UnitType = unitType;
    }

    public void Interact(IInteractable.InteractState state)
    {

    }
}
