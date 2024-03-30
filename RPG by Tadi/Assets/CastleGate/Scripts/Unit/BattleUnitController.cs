using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tadi.Utils;
using Tadi.Datas.BattleSystem;
using Tadi.Datas.Unit;

public class BattleUnitController : MonoBehaviour
{
    // -- Variables --
    [SerializeField] private GameObject selector;

    private BattleUnitMovement move;
    private UnitController unitController;

    private UnitParty party;
    private UnitAction action;
    private UnitActionPriority actionPriority = 0; // 0 ~ 10
    private CombatSkill_ usingSkill;
    private List<BattleUnitController> targetControllers = new List<BattleUnitController>();

    // -- Properties --
    public UnitController UnitController { get { return unitController; } }
    public UnitParty Party { get { return party; } }
    public UnitAction Action
    {
        get { return action; }
        set
        {
            action = value;
            actionPriority = Battle.GetActionPriority(value);
        }
    }
    public UnitActionPriority ActionPriority { get { return actionPriority; } }
    public List<BattleUnitController> Targets { get { return targetControllers; } }
    public bool IsFainted { get { return unitController.CheckIsFainted(); } }

    private void Awake()
    {
        move = GetComponent<BattleUnitMovement>();
        unitController = GetComponentInChildren<UnitController>();
        unitController.OnFainted = SetFaintedUnit;
    }

    public void Init(UnitType type, UnitParty party, int level)
    {
        unitController.Init(type, level);
        this.party = party;

        bool isFacingLeft = party == UnitParty.EnemyParty;
        move.RotateCharacter(isFacingLeft);
    }

    public void ExcuteAction(System.Action OnTurnComplete)
    {
        if (action == UnitAction.Attack)
        {
            ExcuteAttackAction(targetControllers, OnTurnComplete);
        }
        else if (action == UnitAction.Defense)
        {
            ExcuteDefenseAction(OnTurnComplete);
        }
        else if (action == UnitAction.Skill)
        {
            ExcuteSkillAction(usingSkill, targetControllers, OnTurnComplete);
        }
    }

    public void ExcuteAttackAction(List<BattleUnitController> targets, System.Action OnTurnComplete)
    {
        foreach (BattleUnitController target in targets)
        {
            move.ExcuteAttack(
                unitController.AttackType,
                unitController.BulletType,
                target.transform.position,
                () => { target.TakeDamage(unitController, unitController.DamageType); },
                OnTurnComplete);
        }
    }

    public void ExcuteDefenseAction(System.Action OnTurnComplete)
    {
        move.PlayDefenseAnimation(true, OnTurnComplete);
    }

    public void ExcuteSkillAction(CombatSkill_ skill, List<BattleUnitController> targets, System.Action OnTurnComplete)
    {
        Tadi.Datas.Combat.DamageType damageType = skill.DamageType;
        Tadi.Datas.Combat.AttackType attackType = skill.AttackType;
        float skillMultiplier = skill.Power;
        Tadi.Datas.Weapon.BulletType bullet = skill.Bullet;

        foreach (BattleUnitController target in targets)
        {
            move.ExcuteAttack(
                attackType,
                bullet,
                target.transform.position,
                () => { target.TakeDamage(unitController, damageType, skillMultiplier); },
                OnTurnComplete);
        }
    }

    private void TakeDamage(UnitController attacker, Tadi.Datas.Combat.DamageType damageType, float skillMultiplier = 1f)
    {
        if (damageType == Tadi.Datas.Combat.DamageType.None)
        {
            Debug.LogError($"Enum variable [DamageType] must to be set.");

            return;
        }

        bool defending = action == UnitAction.Defense;

        unitController.TakeDamage(attacker, damageType, defending, skillMultiplier);
    }

    public void ResetBattleUnit()
    {
        if (action == UnitAction.Defense)
            move.PlayDefenseAnimation(false);

        action = UnitAction.None;
        usingSkill = null;
        targetControllers.Clear();
    }

    private void SetFaintedUnit()
    {
        move.PlayDeathAnimation();
    }

    public void SetSelector(bool activating)
    {
        selector.SetActive(activating);
    }

    public CombatSkill_ GetSkillInfo(int index)
    {
        List<CombatSkill_> skills = GetUsableSkills();

        return skills[index];
    }

    public void SetUsingSkill(int index)
    {
        List<CombatSkill_> skills = GetUsableSkills();

        usingSkill = skills[index];
    }

    public List<string> GetUsableSkillNameList()
    {
        List<string> names = new List<string>();
        List<CombatSkill_> skills = GetUsableSkills();

        foreach (CombatSkill_ skill in skills)
        {
            names.Add(skill.Name);
        }

        return names;
    }

    private List<CombatSkill_> GetUsableSkills()
    {
        List<CombatSkill_> skills = null;// = characterSO.Skills.Where(s => s.LearnLevel <= unitController.Level).ToList();

        return skills;
    }
}
