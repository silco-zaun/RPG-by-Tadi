using System.Collections.Generic;
using UnityEngine;
using Tadi.Datas.BattleSystem;
using Tadi.Utils;

public class BattleSystemController : MonoBehaviour
{
    private BattleSystemUI ui;
    private BattleSystemUnits units;

    // Bat Info
    private BattleState state;
    private int selectedPlayerUnitIndex = 0;
    private UnitAction selectedAction = UnitAction.None;
    private UnitParty targetUnitParty;
    private int selectedTargetUnitIndex = 0;
    private int selectedSkillIndex = 0;

    // Bat UI Info
    private List<string> unitNames;
    private List<string> actionNames;

    public BattleState State { get { return state; } }

    private void Awake()
    {
        ui = GetComponent<BattleSystemUI>();
        units = GetComponent<BattleSystemUnits>();
    }

    private void Start()
    {
        ui.SetDialogText();
        unitNames = units.GetAliveUnitsNames(UnitParty.PlayerParty);
        actionNames = new List<string>()
        {
            UnitActionKor.공격.ToString(),
            UnitActionKor.방어.ToString(),
            UnitActionKor.스킬.ToString(),
            //BattleUnitActionKor.아이템.ToString(),
            //BattleUnitActionKor.파티.ToString(),
            //BattleUnitActionKor.진형.ToString(),
            UnitActionKor.도망.ToString()
        };
        ui.CreateBattleMenu(unitNames, actionNames);
        SelectPlayerUnit();
    }

    private void SelectPlayerUnit()
    {
        ui.TypeSentence("행동할 유닛을 선택합니다.");
        state = BattleState.SelectPlayerUnit;
        int index = ui.SetUIToSelectPlayerUnit();

        units.NavigateUnit(UnitParty.PlayerParty, index, ref selectedPlayerUnitIndex);
    }

    private void SelectAction()
    {
        ui.TypeSentence("행동을 정합니다.");
        state = BattleState.SelectAction;
        ui.SetUIToSelectAction();
    }

    private void SelectTarget()
    {
        ui.TypeSentence("타겟을 정합니다.");
        state = BattleState.SelectTarget;

        if (selectedAction == UnitAction.Attack)
            targetUnitParty = UnitParty.EnemyParty;

        List<string> names = units.GetAliveUnitsNames(targetUnitParty);
        ui.SetUIToSelectTargetUnit(names);
        units.NavigateUnit(targetUnitParty, 0, ref selectedTargetUnitIndex);
    }

    private void SelectSkill()
    {
        //ui.TypeSentence("스킬을 선택 합니다.");
        state = BattleState.SelectSkill;

        List<string> skillNames = units.GetAliveUnitsSkillNames(UnitParty.PlayerParty, selectedPlayerUnitIndex);

        ui.SetUIToSelectSkill(skillNames);

        CombatSkill_ skill = units.GetSkillInfo(UnitParty.PlayerParty, selectedPlayerUnitIndex, 0);
        string description = skill.Description;
        ui.DisplaySentence(description);
    }

    private void SelectSkillTarget()
    {
        ui.TypeSentence("타겟을 정합니다.");
        state = BattleState.SelectSkillTarget;

        if (selectedAction == UnitAction.Skill)
            targetUnitParty = UnitParty.EnemyParty;

        List<string> names = units.GetAliveUnitsNames(targetUnitParty);
        ui.SetUIToSelectSkillTarget(names);
        units.NavigateUnit(targetUnitParty, 0, ref selectedTargetUnitIndex);
    }

    private void ProgressRound()
    {
        ui.TypeSentence("행동을 시작 합니다.");
        state = BattleState.ProgressRound;
        ui.SetUIToProgressRound();
        units.SetEnemyUnitBehavior();
        units.SetTurnOrder();

        BattleUnitController unit = units.GetBehaveUnit();
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
        BattleCondition condition = units.CheckBattleCondition();
        BattleUnitController unit = units.GetBehaveUnit();

        // Bat Over
        if (condition != BattleCondition.None)
        {
            BattleOver();
        }
        // All Utils behavior completed
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
        ui.TypeSentence("전투가 종료 되었습니다.");
    }

    public void Navigate(Vector2 vector)
    {
        if (state == BattleState.SelectPlayerUnit)
        {
            if (vector.y != 0)
            {
                int itemIndex = ui.NavigateMenu(vector);
                units.NavigateUnit(UnitParty.PlayerParty, itemIndex, ref selectedPlayerUnitIndex);
            }
        }
        else if (state == BattleState.SelectAction)
        {
            if (vector.y != 0)
            {
                int itemIndex = ui.NavigateMenu(vector);
                selectedAction = (UnitAction)(itemIndex + 1);
            }
        }
        else if (state == BattleState.SelectTarget)
        {
            if (vector.y != 0)
            {
                int itemIndex = ui.NavigateMenu(vector);
                units.NavigateUnit(targetUnitParty, itemIndex, ref selectedTargetUnitIndex);
            }
        }
        else if (state == BattleState.SelectSkill)
        {
            if (vector.y != 0)
            {
                selectedSkillIndex = ui.NavigateMenu(vector);
                CombatSkill_ skill = units.GetSkillInfo(UnitParty.PlayerParty, selectedPlayerUnitIndex, selectedSkillIndex);
                string description = skill.Description;
                ui.DisplaySentence(description);
            }
        }
        else if (state == BattleState.SelectSkillTarget)
        {
            if (vector.y != 0)
            {
                int itemIndex = ui.NavigateMenu(vector);
                units.NavigateUnit(targetUnitParty, itemIndex, ref selectedTargetUnitIndex);
            }
        }
    }

    public void SubmitUI()
    {
        if (state == BattleState.SelectPlayerUnit)
        {
            ui.SubmitMenu(state);
            SelectAction();
        }
        else if (state == BattleState.SelectAction)
        {
            int itemIndex = ui.SubmitMenu(state);
            selectedAction = (UnitAction)(itemIndex + 1);

            if (selectedAction == UnitAction.Attack)
            {
                SelectTarget();
            }
            else if (selectedAction == UnitAction.Skill)
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
            units.SetTarget(UnitParty.PlayerParty, selectedPlayerUnitIndex, targetUnitParty, selectedTargetUnitIndex);
            SetPlayersAction();
        }
        else if (state == BattleState.SelectSkill)
        {
            selectedSkillIndex = ui.SubmitMenu(state);
            SelectSkillTarget();
        }
        else if (state == BattleState.SelectSkillTarget)
        {
            units.SetUsingSkill(UnitParty.PlayerParty, selectedPlayerUnitIndex, selectedSkillIndex);
            units.SetTarget(UnitParty.PlayerParty, selectedPlayerUnitIndex, targetUnitParty, selectedTargetUnitIndex);
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
        bool allPlayerUnitSelectingAction = units.SetPlayerUnitAction(selectedPlayerUnitIndex, selectedAction);
        ui.SetUnitMenuItemColorState(ItemInfo.ItemColorState.DeactivatedColor);

        selectedAction = UnitAction.None;

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

        units.ResetBattleUnits();

        unitNames = units.GetAliveUnitsNames(UnitParty.PlayerParty);
        ui.CreateBattleMenu(unitNames, actionNames);
    }

}
