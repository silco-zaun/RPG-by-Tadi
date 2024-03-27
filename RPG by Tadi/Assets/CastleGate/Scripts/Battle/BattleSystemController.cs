using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemController : MonoBehaviour
{
    private BattleSystemUI battleSystemUI;
    private BattleSystemUnits battleSystemUnits;

    // Bat Info
    private Datas.BattleState state;
    private int selectedPlayerUnitIndex = 0;
    private Datas.BattleUnitAction selectedAction = Datas.BattleUnitAction.None;
    private Datas.BattleUnitParty targetUnitParty;
    private int selectedTargetUnitIndex = 0;
    private int selectedSkillIndex = 0;

    // Bat UI Info
    private List<string> unitNames;
    private List<string> actionNames;

    public Datas.BattleState State { get { return state; } }

    private void Awake()
    {
        battleSystemUI = GetComponent<BattleSystemUI>();
        battleSystemUnits = GetComponent<BattleSystemUnits>();
    }

    private void Start()
    {
        battleSystemUnits.InitializeBattleUnits();

        unitNames = battleSystemUnits.GetAliveUnitsNames(Datas.BattleUnitParty.PlayerParty);
        actionNames = new List<string>()
        {
            Datas.BattleUnitActionKor.공격.ToString(),
            Datas.BattleUnitActionKor.방어.ToString(),
            Datas.BattleUnitActionKor.스킬.ToString(),
            //BattleUnitActionKor.아이템.ToString(),
            //BattleUnitActionKor.파티.ToString(),
            //BattleUnitActionKor.진형.ToString(),
            Datas.BattleUnitActionKor.도망.ToString()
        };

        battleSystemUI.CreateBattleMenu(unitNames, actionNames);
        SelectPlayerUnit();
    }

    private void SelectPlayerUnit()
    {
        state = Datas.BattleState.SelectPlayerUnit;
        int index = battleSystemUI.SetUIToSelectPlayerUnit();

        battleSystemUnits.NavigateUnit(Datas.BattleUnitParty.PlayerParty, index, ref selectedPlayerUnitIndex);
    }

    private void SelectAction()
    {
        state = Datas.BattleState.SelectAction;
        battleSystemUI.SetUIToSelectAction();
    }

    private void SelectTarget()
    {
        state = Datas.BattleState.SelectTarget;

        if (selectedAction == Datas.BattleUnitAction.Attack)
            targetUnitParty = Datas.BattleUnitParty.EnemyParty;

        List<string> names = battleSystemUnits.GetAliveUnitsNames(targetUnitParty);
        battleSystemUI.SetUIToSelectTargetUnit(names);
        battleSystemUnits.NavigateUnit(targetUnitParty, 0, ref selectedTargetUnitIndex);
    }

    private void SelectSkillTarget()
    {
        state = Datas.BattleState.SelectSkillTarget;

        if (selectedAction == Datas.BattleUnitAction.Skill)
            targetUnitParty = Datas.BattleUnitParty.EnemyParty;

        List<string> names = battleSystemUnits.GetAliveUnitsNames(targetUnitParty);
        battleSystemUI.SetUIToSelectSkillTarget(names);
        battleSystemUnits.NavigateUnit(targetUnitParty, 0, ref selectedTargetUnitIndex);
    }

    private void SelectSkill()
    {
        state = Datas.BattleState.SelectSkill;

        List<string> skillNames = battleSystemUnits.GetAliveUnitsSkillNames(Datas.BattleUnitParty.PlayerParty, selectedPlayerUnitIndex);

        battleSystemUI.SetUIToSelectSkill(skillNames);
    }

    private void ProgressRound()
    {
        state = Datas.BattleState.ProgressRound;
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
        Datas.BattleCondition condition = battleSystemUnits.CheckBattleCondition();
        BattleUnitController unit = battleSystemUnits.GetBehaveUnit();

        // Bat Over
        if (condition != Datas.BattleCondition.None)
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
        if (state == Datas.BattleState.SelectPlayerUnit)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
                battleSystemUnits.NavigateUnit(Datas.BattleUnitParty.PlayerParty, itemIndex, ref selectedPlayerUnitIndex);
            }
        }
        else if (state == Datas.BattleState.SelectAction)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
                selectedAction = (Datas.BattleUnitAction)(itemIndex + 1);
            }
        }
        else if (state == Datas.BattleState.SelectTarget)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
                battleSystemUnits.NavigateUnit(targetUnitParty, itemIndex, ref selectedTargetUnitIndex);
            }
        }
        else if (state == Datas.BattleState.SelectSkill)
        {
            if (vector.y != 0)
            {
                int itemIndex = battleSystemUI.NavigateMenu(vector);
            }
        }
        else if (state == Datas.BattleState.SelectSkillTarget)
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
        if (state == Datas.BattleState.SelectPlayerUnit)
        {
            battleSystemUI.SubmitMenu(state);
            SelectAction();
        }
        else if (state == Datas.BattleState.SelectAction)
        {
            int itemIndex = battleSystemUI.SubmitMenu(state);
            selectedAction = (Datas.BattleUnitAction)(itemIndex + 1);

            if (selectedAction == Datas.BattleUnitAction.Attack)
            {
                SelectTarget();
            }
            else if (selectedAction == Datas.BattleUnitAction.Skill)
            {
                SelectSkill();
            }
            else
            {
                SetPlayersAction();
            }
        }
        else if (state == Datas.BattleState.SelectTarget)
        {
            battleSystemUnits.SetTarget(Datas.BattleUnitParty.PlayerParty, selectedPlayerUnitIndex, targetUnitParty, selectedTargetUnitIndex);
            SetPlayersAction();
        }
        else if (state == Datas.BattleState.SelectSkill)
        {
            selectedSkillIndex = battleSystemUI.SubmitMenu(state);
            SelectSkillTarget();
        }
        else if (state == Datas.BattleState.SelectSkillTarget)
        {
            battleSystemUnits.SetUsingSkill(Datas.BattleUnitParty.PlayerParty, selectedPlayerUnitIndex, selectedSkillIndex);
            battleSystemUnits.SetTarget(Datas.BattleUnitParty.PlayerParty, selectedPlayerUnitIndex, targetUnitParty, selectedTargetUnitIndex);
            SetPlayersAction();
        }
    }

    public void CancelUI()
    {
        switch (state)
        {
            case Datas.BattleState.SelectPlayerUnit:
                break;
            case Datas.BattleState.SelectAction:
                SelectPlayerUnit();
                break;
            case Datas.BattleState.SelectTarget:
            case Datas.BattleState.SelectSkill:
                SelectAction();
                break;
            case Datas.BattleState.SelectSkillTarget:
                SelectSkill();
                break;
        }
    }

    private void SetPlayersAction()
    {
        bool allPlayerUnitSelectingAction = battleSystemUnits.SetPlayerUnitAction(selectedPlayerUnitIndex, selectedAction);
        battleSystemUI.SetUnitMenuItemColorState(ItemInfo.ItemColorState.DeactivatedColor);

        selectedAction = Datas.BattleUnitAction.None;

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

        unitNames = battleSystemUnits.GetAliveUnitsNames(Datas.BattleUnitParty.PlayerParty);
        battleSystemUI.CreateBattleMenu(unitNames, actionNames);
    }

}
