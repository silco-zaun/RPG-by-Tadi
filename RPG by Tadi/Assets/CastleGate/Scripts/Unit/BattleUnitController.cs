using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleUnitController : MonoBehaviour
{
    // -- Variables --
    [SerializeField] private GameObject selector;

    private BattleUnitMovement battleUnitMovement;
    private CharacterController characterController;

    private CharacterBaseData characterBaseData;
    private Formation formation;
    private BattleDataManager.BattleUnitParty battleUnitParty;
    private BattleDataManager.BattleUnitAction battleUnitAction;
    private BattleDataManager.BattleUnitActionPriority actionPriority = 0; // 0 ~ 10
    private CombatSkill usingSkill;
    private List<BattleUnitController> targetControllers = new List<BattleUnitController>();

    // -- Properties --
    public CharacterController CharacterController { get { return characterController; } }
    public CharacterBaseData CharacterBaseData
    {
        get { return characterBaseData; }
        set
        {
            characterBaseData = value;
            characterController.BaseData = value;
        }
    }
    public Formation Formation { get { return formation; } set { formation = value; } }
    public BattleDataManager.BattleUnitParty Party
    {
        get { return battleUnitParty; }
        set
        {
            battleUnitParty = value;
            bool isFacingLeft = battleUnitParty == BattleDataManager.BattleUnitParty.EnemyParty;
            battleUnitMovement.RotateCharacter(isFacingLeft);
        }
    }
    public BattleDataManager.BattleUnitAction Action
    {
        get { return battleUnitAction; }
        set
        {
            battleUnitAction = value;
            actionPriority = DataManager.Ins.Bat.GetActionPriority(value);
        }
    }
    public BattleDataManager.BattleUnitActionPriority ActionPriority { get { return actionPriority; } }
    public List<BattleUnitController> Targets { get { return targetControllers; } }
    public bool IsFainted { get { return characterController.CheckIsFainted(); } }

    private void Awake()
    {
        battleUnitMovement = GetComponent<BattleUnitMovement>();
        characterController = GetComponentInChildren<CharacterController>();

        characterController.OnFainted = SetFaintedUnit;
    }

    public void ExcuteAction(System.Action OnTurnComplete)
    {
        if (battleUnitAction == BattleDataManager.BattleUnitAction.Attack)
        {
            ExcuteAttackAction(targetControllers, OnTurnComplete);
        }
        else if (battleUnitAction == BattleDataManager.BattleUnitAction.Defense)
        {
            ExcuteDefenseAction(OnTurnComplete);
        }
        else if (battleUnitAction == BattleDataManager.BattleUnitAction.Skill)
        {
            ExcuteSkillAction(usingSkill, targetControllers, OnTurnComplete);
        }
    }

    public void ExcuteAttackAction(List<BattleUnitController> targets, System.Action OnTurnComplete)
    {
        foreach (BattleUnitController target in targets)
        {
            battleUnitMovement.ExcuteAttack(
                characterController.AttackType,
                target.transform.position,
                () => { target.TakeDamage(characterController, characterController.DamageType); },
                OnTurnComplete);
        }
    }

    public void ExcuteDefenseAction(System.Action OnTurnComplete)
    {
        battleUnitMovement.PlayDefenseAnimation(true, OnTurnComplete);
    }

    public void ExcuteSkillAction(CombatSkill skill, List<BattleUnitController> targets, System.Action OnTurnComplete)
    {
        CharacterDataManager.DamageType attackType = skill.AttackType;
        float skillMultiplier = skill.Power;

        foreach (BattleUnitController target in targets)
        {
            battleUnitMovement.ExcuteMeleeAttack(
                target.transform.position,
                () => { target.TakeDamage(characterController, attackType, skillMultiplier); },
                OnTurnComplete);
        }
    }

    private void TakeDamage(CharacterController attacker, CharacterDataManager.DamageType attackType, float skillMultiplier = 1f)
    {
        if (attackType == CharacterDataManager.DamageType.None)
        {
            Debug.LogError($"Enum variable [DamageType] must to be set.");

            return;
        }

        bool defending = battleUnitAction == BattleDataManager.BattleUnitAction.Defense;

        characterController.TakeDamage(attacker, attackType, defending, skillMultiplier);
    }

    public void ResetBattleUnit()
    {
        if (battleUnitAction == BattleDataManager.BattleUnitAction.Defense)
            battleUnitMovement.PlayDefenseAnimation(false);

        battleUnitAction = BattleDataManager.BattleUnitAction.None;
        targetControllers.Clear();
    }

    private void SetFaintedUnit()
    {
        battleUnitMovement.PlayDeathAnimation();
    }

    // Temp method
    public void SetCharacterData(int level)
    {
        characterController.Level = level;
    }

    public void SetSelector(bool activating)
    {
        selector.SetActive(activating);
    }

    public void SetUsingSkill(int index)
    {
        List<CombatSkill> skills = GetUsableSkills();

        usingSkill = skills[index];
    }

    public List<string> GetUsableSkillNameList()
    {
        List<string> names = new List<string>();
        List<CombatSkill> skills = GetUsableSkills();

        foreach (CombatSkill skill in skills)
        {
            names.Add(skill.Name);
        }

        return names;
    }

    private List<CombatSkill> GetUsableSkills()
    {
        List<CombatSkill> skills = characterBaseData.Skills.Where(s => s.LearnLevel <= characterController.Level).ToList();

        return skills;
    }
}
