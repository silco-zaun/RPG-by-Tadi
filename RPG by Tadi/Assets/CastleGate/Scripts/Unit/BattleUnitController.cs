using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleUnitController : MonoBehaviour
{
    // -- Variables --
    [SerializeField] private GameObject selector;

    private BattleUnitMovement battleUnitMovement;
    private CharacterController characterController;

    private CharacterSO characterBaseData;
    private Formation formation;
    private Datas.BattleUnitParty battleUnitParty;
    private Datas.BattleUnitAction battleUnitAction;
    private Datas.BattleUnitActionPriority actionPriority = 0; // 0 ~ 10
    private CombatSkill usingSkill;
    private List<BattleUnitController> targetControllers = new List<BattleUnitController>();

    // -- Properties --
    public CharacterController CharacterController { get { return characterController; } }
    public CharacterSO CharacterBaseData
    {
        get { return characterBaseData; }
        set
        {
            characterBaseData = value;
            characterController.CharacterSO = value;
        }
    }
    public Formation Formation { get { return formation; } set { formation = value; } }
    public Datas.BattleUnitParty Party
    {
        get { return battleUnitParty; }
        set
        {
            battleUnitParty = value;
            bool isFacingLeft = battleUnitParty == Datas.BattleUnitParty.EnemyParty;
            battleUnitMovement.RotateCharacter(isFacingLeft);
        }
    }
    public Datas.BattleUnitAction Action
    {
        get { return battleUnitAction; }
        set
        {
            battleUnitAction = value;
            actionPriority = Datas.Bat.GetActionPriority(value);
        }
    }
    public Datas.BattleUnitActionPriority ActionPriority { get { return actionPriority; } }
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
        if (battleUnitAction == Datas.BattleUnitAction.Attack)
        {
            ExcuteAttackAction(targetControllers, OnTurnComplete);
        }
        else if (battleUnitAction == Datas.BattleUnitAction.Defense)
        {
            ExcuteDefenseAction(OnTurnComplete);
        }
        else if (battleUnitAction == Datas.BattleUnitAction.Skill)
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
                characterController.BulletType,
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
        Datas.DamageType damageType = skill.DamageType;
        Datas.AttackType attackType = skill.AttackType;
        float skillMultiplier = skill.Power;
        Datas.BulletType bullet = skill.Bullet;

        foreach (BattleUnitController target in targets)
        {
            battleUnitMovement.ExcuteAttack(
                attackType,
                bullet,
                target.transform.position,
                () => { target.TakeDamage(characterController, damageType, skillMultiplier); },
                OnTurnComplete);
        }
    }

    private void TakeDamage(CharacterController attacker, Datas.DamageType damageType, float skillMultiplier = 1f)
    {
        if (damageType == Datas.DamageType.None)
        {
            Debug.LogError($"Enum variable [DamageType] must to be set.");

            return;
        }

        bool defending = battleUnitAction == Datas.BattleUnitAction.Defense;

        characterController.TakeDamage(attacker, damageType, defending, skillMultiplier);
    }

    public void ResetBattleUnit()
    {
        if (battleUnitAction == Datas.BattleUnitAction.Defense)
            battleUnitMovement.PlayDefenseAnimation(false);

        battleUnitAction = Datas.BattleUnitAction.None;
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
