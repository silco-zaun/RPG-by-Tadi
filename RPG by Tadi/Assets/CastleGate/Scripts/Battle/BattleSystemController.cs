using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleDataManager;

public class BattleSystemController : MonoBehaviour
{
    private BattleSystemUI battleUI;
    private BattleSystemUnits battleUnits;
    private BattleState state;
    private BattleUnitBehavior selectedBehavior = BattleUnitBehavior.None;
    private int behaveUnitIndex = 0;

    public BattleState State { get { return state; } }

    private void Awake()
    {
        battleUI = GetComponent<BattleSystemUI>();
        battleUnits = GetComponent<BattleSystemUnits>();
    }

    private void Start()
    {
        battleUnits.SetBattleUnit();

        battleUI.BehaviorMenuItemsInfo = new List<ItemInfo>()
        {
            new ItemInfo(BattleUnitBehaviorKor.공격.ToString()),
            new ItemInfo(BattleUnitBehaviorKor.방어.ToString()),
            new ItemInfo(BattleUnitBehaviorKor.스킬.ToString()),
            new ItemInfo(BattleUnitBehaviorKor.아이템.ToString()),
            new ItemInfo(BattleUnitBehaviorKor.파티.ToString()),
            new ItemInfo(BattleUnitBehaviorKor.진형.ToString()),
            new ItemInfo(BattleUnitBehaviorKor.도망.ToString())
        };

        battleUI.CreateBattleMenu();

        SelectBehaviorUnit();
    }

    public void SelectBehaviorUnit()
    {
        state = BattleState.SelectBehaviorUnit;
        battleUI.SetUIToSelectBehaviorUnit();
    }

    public void SelectBehavior()
    {
        state = BattleState.SelectBehavior;
        battleUI.SetUIToSelectBehavior();
    }

    public void SelectTargetUnit()
    {
        state = BattleState.SelectTargetUnit;
        battleUI.SetUIToSelectTargetUnit();
        battleUnits.SelectTargetUnit(true, Vector2.zero);
    }

    public void DoBehavior()
    {
        state = BattleState.DoBehavior;
        battleUI.SetUIToDoBehavior();

        BattleUnitController unit = battleUnits.BattleUnitsOrderBySpeed[behaveUnitIndex];

        unit.DoBehavior(() => { BehaviorComplete(); });
    }

    private void BehaviorComplete()
    {
        behaveUnitIndex++;

        if (behaveUnitIndex < battleUnits.BattleUnitsOrderBySpeed.Count)
        {
            DoBehavior();
        }
        else
        {
            // 1. Check battle over
            BattleResult battleResult = battleUnits.CheckBattleResult();

            // 2-1. If not game over, continue battle.
            if (battleResult == BattleResult.None)
            {
                behaveUnitIndex = 0;
                battleUnits.ResetPlayersBehavior();
                battleUI.ResetAllMenu();
                SelectBehaviorUnit();
            }
            // 2-2. else if game over, end battle.
            else
            {
                // Battle Over
                Debug.Log("Battle Over");
            }
        }
    }

    public void NavigateUI(Vector2 vector)
    {
        if (state == BattleState.SelectTargetUnit)
        {
            battleUnits.SelectTargetUnit(true, vector);
        }
        else if (
            state == BattleState.SelectBehaviorUnit ||
            state == BattleState.SelectBehavior)
        {
            if (vector.y != 0)
            {
                battleUI.NavigateMenu(vector);
            }
        }
    }

    public void SubmitUI()
    {
        if (state == BattleState.SelectBehaviorUnit)
        {
            int itemIndex = battleUI.SubmitMenu(state);
            battleUnits.SetSelectedPlayerUnitIndex(itemIndex);
            SelectBehavior();
        }
        else if (state == BattleState.SelectBehavior)
        {
            int itemIndex = battleUI.SubmitMenu(state);

            selectedBehavior = (BattleUnitBehavior)(itemIndex + 1);

            bool selectTarget = selectedBehavior == BattleUnitBehavior.Attack;

            if (selectTarget)
            {
                SelectTargetUnit();
            }
            else
            {
                SetPlayersBehavior();
            }
        }
        else if (state == BattleState.SelectTargetUnit)
        {
            battleUnits.SetTargetUnit();
            SetPlayersBehavior();
        }
    }

    public void CancelUI()
    {
        switch (state)
        {
            case BattleState.SelectBehaviorUnit:
                break;
            case BattleState.SelectBehavior:
                SelectBehaviorUnit();
                break;
        }
    }

    public void SetPlayersBehavior()
    {
        bool selectBehaviorComplete = battleUnits.SetPlayersBehavior(selectedBehavior);

        if (selectBehaviorComplete)
        {
            battleUnits.SetAIUnitBehavior();
            DoBehavior();
        }
        else
        {
            SelectBehaviorUnit();
        }
    }

}