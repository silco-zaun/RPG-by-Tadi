
using System.Collections.Generic;
using Tadi.Datas.BattleSystem;
using Tadi.Datas.Unit;
using Tadi.Interface.unit;
using UnityEngine;

public class PlayerUnitController : Singleton<PlayerUnitController>, IInteractable
{
    [SerializeField] private List<BattleUnitInfo> battleUnitInfo;

    private PlayerUnitCommands command;
    private UnitController unit;

    public List<BattleUnitInfo> BattleUnitInfo { get { return battleUnitInfo; } }

    private new void Awake() 
    {
        base.Awake();

        command = GetComponent<PlayerUnitCommands>();
        unit = GetComponentInChildren<UnitController>();
    }

    private void Start()
    {
        //Managers.Ins.Stat.PlayerUnit = gameObject;
        unit.Init(UnitType.Knight, 1);
    }

    public void Interact(IInteractable.InteractState state)
    {
        if (state == IInteractable.InteractState.ShowDialog)
        {
            command.SetToShowDialogState();
        }
    }
}
