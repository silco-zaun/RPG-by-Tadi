using System.Collections.Generic;
using UnityEngine;
using Tadi.Datas.BattleSystem;
using Tadi.UI.ScrollView;
using System;
using System.Collections;

public class BattleSystemController : Singleton<BattleSystemController>
{
    private BattleSystemUI ui;
    private BattleSystemUnits units;
    private GameObject enemyUnitObject;

    // Bat Info
    private BattleState state;
    private int selectedPlayerUnitIndex = 0;
    private UnitAction selectedAction = UnitAction.None;
    private UnitParty targetUnitParty;
    private int selectedTargetUnitIndex = 0;
    private int selectedSkillIndex = 0;

    // Bat UI Info
    private List<ItemInfo> unitNameList;
    private List<ItemInfo> targetNameList;
    private List<ItemInfo> actionNameList;
    private List<List<ItemInfo>> skillNameList;

    public BattleState State { get { return state; } }

    Action<BattleCondition> OnBattleEnd;

    private new void Awake()
    {
        base.Awake();

        ui = GetComponent<BattleSystemUI>();
        units = GetComponent<BattleSystemUnits>();
    }

    private void Start()
    {
        //Managers.Ins.Stat.BattleSystem = gameObject;
    }

    private void OnEnable()
    {
        if (ui != null) { }
    }

    private void OnDisable()
    {
        if (ui != null) { }
    }

    public void Init(List<BattleUnitInfo> playersBattleUnitInfo, List<BattleUnitInfo> enemysBattleUnitInfo, GameObject enemyUnitObject, Action<BattleCondition> OnBattleEnd)
    {
        this.OnBattleEnd = OnBattleEnd;
        this.enemyUnitObject = enemyUnitObject;
        units.InitBattleUnit(playersBattleUnitInfo, enemysBattleUnitInfo);
        SetItemInfoList();
        ui.CreateBattleMenu(unitNameList, targetNameList, actionNameList, skillNameList);
        SelectPlayerUnit();
    }

    private void SetItemInfoList()
    {
        unitNameList = units.GetAliveUnitInfoList(UnitParty.PlayerParty);
        targetNameList = units.GetAliveUnitInfoList(UnitParty.EnemyParty);
        actionNameList = new List<ItemInfo>()
        {
            new ItemInfo(UnitActionKor.공격.ToString(), MenuType.TargetMenu),
            new ItemInfo(UnitActionKor.방어.ToString(), MenuType.None),
            new ItemInfo(UnitActionKor.스킬.ToString(), MenuType.SkillMenu),
            //new ItemInfo(UnitActionKor.아이템.ToString(), MenuType.None),
            //new ItemInfo(UnitActionKor.파티.ToString(), MenuType.None),
            //new ItemInfo(UnitActionKor.진형.ToString(), MenuType.None),
            new ItemInfo(UnitActionKor.도망.ToString(), MenuType.None),
        };
        skillNameList = units.GetAliveUnitsSkillInfos(UnitParty.PlayerParty);
    }

    private void SelectPlayerUnit()
    {
        StartCoroutine(ui.TypeSentence("행동할 유닛을 선택합니다."));
        state = BattleState.SelectPlayerUnit;
        int index = ui.SetUIToSelectPlayerUnit();

        units.NavigateUnit(UnitParty.PlayerParty, index, ref selectedPlayerUnitIndex);
    }

    private void SelectAction()
    {
        StartCoroutine(ui.TypeSentence("행동을 정합니다."));
        state = BattleState.SelectAction;
        ui.SetUIToSelectAction();
    }

    private void SelectTarget()
    {
        StartCoroutine(ui.TypeSentence("타겟을 정합니다."));
        state = BattleState.SelectTarget;

        if (selectedAction == UnitAction.Attack)
            targetUnitParty = UnitParty.EnemyParty;

        ui.SetUIToSelectTargetUnit();
        units.NavigateUnit(targetUnitParty, 0, ref selectedTargetUnitIndex);
    }

    private void SelectSkill()
    {
        state = BattleState.SelectSkill;
        ui.SetUIToSelectSkill();
        string description = units.GetSkillDescription(UnitParty.PlayerParty, selectedPlayerUnitIndex, 0);
        ui.DisplaySentence(description);
    }

    private void SelectSkillTarget()
    {
        StartCoroutine(ui.TypeSentence("타겟을 정합니다."));
        state = BattleState.SelectSkillTarget;

        if (selectedAction == UnitAction.Skill)
            targetUnitParty = UnitParty.EnemyParty;

        ui.SetUIToSelectSkillTarget();
        units.NavigateUnit(targetUnitParty, 0, ref selectedTargetUnitIndex);
    }

    private void ProgressRound()
    {
        StartCoroutine(ui.TypeSentence("행동을 시작 합니다."));
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
            StartCoroutine(BattleEndRoutine());
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

    private IEnumerator BattleEndRoutine()
    {
        ResetRound();

        yield return StartCoroutine(ui.TypeSentence("전투가 종료 되었습니다."));

        Managers.Ins.Stat.BattleEnd();

        BattleCondition condition = units.CheckBattleCondition();

        if (condition == BattleCondition.Win)
        {
            Destroy(enemyUnitObject);
        }

        OnBattleEnd?.Invoke(condition);
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
                string description = units.GetSkillDescription(UnitParty.PlayerParty, selectedPlayerUnitIndex, selectedSkillIndex);
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
                SetPlayerAction();
            }
        }
        else if (state == BattleState.SelectTarget)
        {
            units.SetTarget(UnitParty.PlayerParty, selectedPlayerUnitIndex, targetUnitParty, selectedTargetUnitIndex);
            SetPlayerAction();
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
            SetPlayerAction();
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

    private void SetPlayerAction()
    {
        bool allPlayerUnitSelectingAction = units.SetPlayerAction(selectedPlayerUnitIndex, selectedAction);
        ui.SetPlayerAction(ItemState.Deactivated);

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
        SetItemInfoList();
        ui.CreateBattleMenu(unitNameList, targetNameList, actionNameList, skillNameList);
    }
}
