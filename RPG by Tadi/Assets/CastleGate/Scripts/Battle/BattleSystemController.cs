using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleDataManager;

public class BattleSystemController : MonoBehaviour
{
    private BattleSystemUI battleSystemUI;
    private BattleSystemUnits battleSystemUnits;

    // Bat Info
    private BattleState state;
    private int selectedPlayerUnitIndex = 0;
    private BattleUnitAction selectedAction = BattleUnitAction.None;
    private BattleUnitParty targetUnitParty;
    private int selectedTargetUnitIndex = 0;
    private int selectedSkillIndex = 0;

    // Bat UI Info
    private List<string> unitNames;
    private List<string> actionNames;

    public BattleState State { get { return state; } }

    private void Awake()
    {
        battleSystemUI = GetComponent<BattleSystemUI>();
        battleSystemUnits = GetComponent<BattleSystemUnits>();
    }

    private void Start()
    {
        battleSystemUnits.InitializeBattleUnits();

        unitNames = battleSystemUnits.GetAliveUnitsNames(BattleUnitParty.PlayerParty);
        actionNames = new List<string>()
        {
            BattleUnitActionKor.공격.ToString(),
            BattleUnitActionKor.방어.ToString(),
            BattleUnitActionKor.스킬.ToString(),
            //BattleUnitActionKor.아이템.ToString(),
            //BattleUnitActionKor.파티.ToString(),
            //BattleUnitActionKor.진형.ToString(),
            BattleUnitActionKor.도망.ToString()
        };

        battleSystemUI.CreateBattleMenu(unitNames, actionNames);
        SelectPlayerUnit();
    }

    private void SelectPlayerUnit()
    {
        state = BattleState.SelectPlayerUnit;
        int index = battleSystemUI.SetUIToSelectPlayerUnit();

        battleSystemUnits.NavigateUnit(BattleUnitParty.PlayerParty, index, ref selectedPlayerUnitIndex);
    }

    private void SelectAction()
    {
        state = BattleState.SelectAction;
        battleSystemUI.SetUIToSelectAction();
    }

    private void SelectTarget()
    {
        state = BattleState.SelectTarget;

        if (selectedAction == BattleUnitAction.Attack)
            targetUnitParty = BattleUnitParty.EnemyParty;

        List<string> names = battleSystemUnits.GetAliveUnitsNames(targetUnitParty);
        battleSystemUI.SetUIToSelectTargetUnit(names);
        battleSystemUnits.NavigateUnit(targetUnitParty, 0, ref selectedTargetUnitIndex);
    }

    private void SelectSkillTarget()
    {
        state = BattleState.SelectSkillTarget;

        if (selectedAction == BattleUnitAction.Skill)
            targetUnitParty = BattleUnitParty.EnemyParty;

        List<string> names = battleSystemUnits.GetAliveUnitsNames(targetUnitParty);
        battleSystemUI.SetUIToSelectSkillTarget(names);
        battleSystemUnits.NavigateUnit(targetUnitParty, 0, ref selectedTargetUnitIndex);
    }

    private void SelectSkill()
    {
        state = BattleState.SelectSkill;

        List<string> skillNames = battleSystemUnits.GetAliveUnitsSkillNames(BattleUnitParty.PlayerParty, selectedPlayerUnitIndex);

        battleSystemUI.SetUIToSelectSkill(skillNames);
    }

    private void ProgressRound()
    {
        state = BattleState.ProgressRound;
        battleSystemUI.SetUIToProgressRound();
        battleSystemUnits.SetEnemyUnitBehavior();
        battleSystemUnits.SetTurnOrder();

        BattleUnitController unit = battleSystemUnits.GetBehaveUnit();
        ProgressTurn(unit);
    }

    private void ProgressTurn(BattleUnitController unit)
    {
        PreTurnAction();
        ExcuteTurnAction(unit);
        PostTurnAction();
    }

    private void PreTurnAction()
    {

    }

    private void ExcuteTurnAction(BattleUnitController unit)
    {
        unit.ExcuteAction(() => { TurnComplete(); });
    }

    private void PostTurnAction()
    {

    }

    private void TurnComplete()
    {
        BattleCondition condition = battleSystemUnits.CheckBattleCondition();
        BattleUnitController unit = battleSystemUnits.GetBehaveUnit();

        // Bat Over
        if (condition != BattleCondition.None)
        {
            BattleOver();
        }
        // All units behavior completed
        else if (unit == null)
        {
            RoundComplete();
        }
        // If there are next unit
        else
        {
            ProgressTurn(unit);
        }
    }

    private void RoundComplete()
    {
        ResetRound();
        SelectPlayerUnit();
    }

    private void ApplyStatusEffects()
    {
        // Application: Apply status effects (e.g., poison, paralysis) and conditions (e.g., buffs, debuffs) 
        // Duration and Removal: Manage the duration of status effects and conditions
    }

    private void BattleOver()
    {
        Debug.Log("Bat Over");
    }

    public void Navigate(Vector2 vector)
    {
        if (state == BattleState.SelectPlayerUnit)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
                battleSystemUnits.NavigateUnit(BattleUnitParty.PlayerParty, itemIndex, ref selectedPlayerUnitIndex);
            }
        }
        else if (state == BattleState.SelectAction)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
                selectedAction = (BattleUnitAction)(itemIndex + 1);
            }
        }
        else if (state == BattleState.SelectTarget)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
                battleSystemUnits.NavigateUnit(targetUnitParty, itemIndex, ref selectedTargetUnitIndex);
            }
        }
        else if (state == BattleState.SelectSkill)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
            }
        }
        else if (state == BattleState.SelectSkillTarget)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
                battleSystemUnits.NavigateUnit(targetUnitParty, itemIndex, ref selectedTargetUnitIndex);
            }
        }
    }

    public void SubmitUI()
    {
        if (state == BattleState.SelectPlayerUnit)
        {
            battleSystemUI.SubmitMenu(state);
            SelectAction();
        }
        else if (state == BattleState.SelectAction)
        {
            int itemIndex = battleSystemUI.SubmitMenu(state);
            selectedAction = (BattleUnitAction)(itemIndex + 1);

            if (selectedAction == BattleUnitAction.Attack)
            {
                SelectTarget();
            }
            else if (selectedAction == BattleUnitAction.Skill)
            {
                SelectSkill();
            }
            else
            {
                SetPlayersAction();
            }
        }
        else if (state == BattleState.SelectTarget)
        {
            battleSystemUnits.SetTarget(BattleUnitParty.PlayerParty, selectedPlayerUnitIndex, targetUnitParty, selectedTargetUnitIndex);
            SetPlayersAction();
        }
        else if (state == BattleState.SelectSkill)
        {
            selectedSkillIndex = battleSystemUI.SubmitMenu(state);
            SelectSkillTarget();
        }
        else if (state == BattleState.SelectSkillTarget)
        {
            battleSystemUnits.SetUsingSkill(BattleUnitParty.PlayerParty, selectedPlayerUnitIndex, selectedSkillIndex);
            battleSystemUnits.SetTarget(BattleUnitParty.PlayerParty, selectedPlayerUnitIndex, targetUnitParty, selectedTargetUnitIndex);
            SetPlayersAction();
        }
    }

    public void CancelUI()
    {
        switch (state)
        {
            case BattleState.SelectPlayerUnit:
                break;
            case BattleState.SelectAction:
                SelectPlayerUnit();
                break;
            case BattleState.SelectTarget:
            case BattleState.SelectSkill:
                SelectAction();
                break;
            case BattleState.SelectSkillTarget:
                SelectSkill();
                break;
        }
    }

    private void SetPlayersAction()
    {
        bool allPlayerUnitSelectingAction = battleSystemUnits.SetPlayerUnitAction(selectedPlayerUnitIndex, selectedAction);
        battleSystemUI.SetUnitMenuItemColorState(ItemInfo.ItemColorState.DeactivatedColor);

        selectedAction = BattleUnitAction.None;

        if (allPlayerUnitSelectingAction)
        {
            ProgressRound();
        }
        else
        {
            SelectPlayerUnit();
        }
    }

    private void ResetRound()
    {
        selectedPlayerUnitIndex = 0;
        selectedTargetUnitIndex = 0;

        battleSystemUnits.ResetBattleUnits();
        unitNames = battleSystemUnits.GetAliveUnitsNames(BattleUnitParty.PlayerParty);
        battleSystemUI.CreateBattleMenu(unitNames, actionNames);
    }

}
