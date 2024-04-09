using System.Collections.Generic;
using UnityEngine;
using Tadi.Utils;
using Tadi.Datas.BattleSystem;
using Tadi.Datas.Unit;
using Tadi.UI.ScrollView;

public class BattleUnitController : MonoBehaviour
{
    // -- Variables --
    [SerializeField] private GameObject selector;

    private BattleUnitMovement move;
    private UnitController unitController;

    // -- Properties --
    public UnitParty Party { get; private set; }
    public UnitAction UnitAction { get; private set; } // Reset every round
    public UnitActionPriority ActionPriority { get; private set; } // Reset every round
    public List<BattleUnitController> TargetControllers { get; private set; } = new List<BattleUnitController>(); // Reset every round
    public UnitCombatSkill UsingSkill { get; private set; } // Reset every round
    public float Speed { get { return unitController.Speed; } }
    public bool IsFainted { get { return unitController.CheckIsFainted(); } }
    public string Name { get { return unitController.UnitName; } }

    private void Awake()
    {
        move = GetComponent<BattleUnitMovement>();
        unitController = GetComponentInChildren<UnitController>();
        unitController.OnFainted = SetFaintedUnit;
    }

    public void Init(UnitType type, UnitParty party, int level)
    {
        unitController.Init(type, level);
        Party = party;

        bool isFacingLeft = party == UnitParty.EnemyParty;
        move.RotateCharacter(isFacingLeft);
    }

    public void ExcuteAction(System.Action OnTurnComplete)
    {
        if (UnitAction == UnitAction.Attack)
        {
            ExcuteAttackAction(TargetControllers, OnTurnComplete);
        }
        else if (UnitAction == UnitAction.Defense)
        {
            ExcuteDefenseAction(OnTurnComplete);
        }
        else if (UnitAction == UnitAction.Skill)
        {
            ExcuteSkillAction(UsingSkill, TargetControllers, OnTurnComplete);
        }
    }

    public void ExcuteAttackAction(List<BattleUnitController> targets, System.Action OnTurnComplete)
    {
        foreach (BattleUnitController target in targets)
        {
            move.ExcuteAttack(
                unitController,
                target.unitController,
                () => { target.TakeDamage(unitController); },
                OnTurnComplete);
        }
    }

    private void TakeDamage(UnitController attacker, UnitCombatSkill skill = null)
    {
        bool defending = UnitAction == UnitAction.Defense;

        unitController.TakeDamage(attacker, defending);
    }

    public void ExcuteDefenseAction(System.Action OnTurnComplete)
    {
        move.PlayDefenseAnimation(true, OnTurnComplete);
    }

    public void ExcuteSkillAction(UnitCombatSkill skill, List<BattleUnitController> targets, System.Action OnTurnComplete)
    {
        foreach (BattleUnitController target in targets)
        {
            move.ExcuteSkillAttack(
                unitController,
                target.unitController,
                skill,
                () => { target.TakeDamage(unitController, skill); },
                OnTurnComplete);
        }
    }

    public void ResetBattleUnit()
    {
        if (UnitAction == UnitAction.Defense)
            move.PlayDefenseAnimation(false);

        UnitAction = UnitAction.None;
        UsingSkill = null;
        TargetControllers.Clear();
    }

    public void SetUnitAction(UnitAction action)
    {
        UnitAction = action;
        ActionPriority = Battle.GetActionPriority(action);
    }

    private void SetFaintedUnit()
    {
        move.PlayDeathAnimation();
    }

    public void SetSelector(bool activating)
    {
        selector.SetActive(activating);
    }

    public string GetSkillDescription(int index)
    {
        return unitController.CombatSkills[index].Description;
    }

    public void SetUsingSkill(int index)
    {
        UsingSkill = unitController.CombatSkills[index];
    }

    public List<ItemInfo> GetUsableSkillInfoList()
    {
        List<ItemInfo> itemInfoList = new List<ItemInfo>();

        foreach (UnitCombatSkill skill in unitController.CombatSkills)
        {
            if (unitController.Level >= skill.LearnLevel)
                itemInfoList.Add(new ItemInfo(skill.Skill.Name, MenuType.SkillTargetMenu));
        }

        return itemInfoList;
    }
}
