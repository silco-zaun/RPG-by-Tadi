using Tadi.Datas.Unit;
using Tadi.Interface.unit;
using UnityEngine;

public class NPCUnitController : MonoBehaviour, IInteractable
{
    [SerializeField] private UnitType unitType;
    [SerializeField] private int unitLevel = 1;

    private NPCUnitAI unitAI;
    private UnitController unit;

    private void Awake()
    {
        unitAI = GetComponent<NPCUnitAI>();
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
