
using UnityEngine;
using Tadi.Utils;
using Tadi.Datas.Unit;
using Tadi.Datas.Combat;
using Tadi.Datas.Weapon;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;

public class UnitController : MonoBehaviour
{
    [SerializeField] private SlideBarController healthBar;

    private UnitAnimation unitAnim;
    private DamageFlash damageFlash;

    private UnitTypeData unitTypeData;

    public System.Action OnFainted;

    public UnitType UnitType { get; private set; }
    public string UnitName { get; private set; }
    public AnimatorController BulletAnim { get; private set; }
    public DamageType DamageType { get; private set; }
    public AttackType AttackType { get; private set; }
    // Stats
    public int Level { get; private set; }
    public float MaxHP { get; private set; }
    public float CurHP { get; private set; }
    public float PhysicalAttack { get; private set; }
    public float PhysicalDefense { get; private set; }
    public float MagicAttack { get; private set; }
    public float MagicDefense { get; private set; }
    public float Speed { get; private set; }
    public List<UnitCombatSkill> CombatSkills { get; private set; }

    private void Awake()
    {
        unitAnim = GetComponent<UnitAnimation>();
        damageFlash = GetComponent<DamageFlash>();
    }

    public void Init(UnitType type, int level)
    {
        unitTypeData = Managers.Ins.Unit.GetUnitTypeData(type);
        UnitType = unitTypeData.UnitType;
        UnitName = unitTypeData.Name;
        BulletAnim = unitTypeData.UnitAnimRes.BulletAnimator;
        DamageType = unitTypeData.DamageType;
        AttackType = unitTypeData.AttackType;
        unitAnim.SetAnimRes(unitTypeData.UnitAnimRes);
        SetUnit(level);
    }

    private void SetUnit(int level)
    {
        Level = level;
        CurHP = MaxHP = Battle.GetHP(level, unitTypeData.BaseHP, unitTypeData.IVHP);
        PhysicalAttack = Battle.GetStat(level, unitTypeData.BasePhysicalAttack, unitTypeData.IVPhysicalAttack);
        PhysicalDefense = Battle.GetStat(level, unitTypeData.BasePhysicalDefense, unitTypeData.IVPhysicalDefense);
        MagicAttack = Battle.GetStat(level, unitTypeData.BaseMagicAttack, unitTypeData.IVMagicAttack);
        MagicDefense = Battle.GetStat(level, unitTypeData.BaseMagicDefense, unitTypeData.IVMagicDefense);
        Speed = Battle.GetStat(level, unitTypeData.BaseSpeed, unitTypeData.IVSpeed);
        CombatSkills = unitTypeData.CombatSkills.Where(s => s.LearnLevel >= level).ToList();
    }

    public void TakeDamage(UnitController attacker, bool defending, UnitCombatSkill skill = null)
    {
        damageFlash.CallDamageFlash();

        float damage = GetDamage(attacker, defending);

        CurHP -= damage;
        float normalizedHP = System.Math.Clamp(CurHP / MaxHP, 0f, MaxHP);
        healthBar.SetBar(normalizedHP);

        bool isFainted = CheckIsFainted();

        if (isFainted)
        {
            OnFainted?.Invoke();
        }
    }

    public float GetDamage(UnitController attacker, bool defending, UnitCombatSkill skill = null)
    {
        float skillMultiplier = 1f;
        float damage = 0f;
        
        if (skill != null)
        {
            skillMultiplier = skill.Multiplier;
        }

        switch (attacker.DamageType)
        {
            case DamageType.Physical:
                damage = Battle.GetDamage(attacker.Level, attacker.PhysicalAttack, PhysicalDefense, defending, skillMultiplier);
                break;
            case DamageType.Magic:
                damage = Battle.GetDamage(attacker.Level, attacker.MagicAttack, MagicDefense, false, skillMultiplier);
                break;
        }

        return damage;
    }

    public bool CheckIsFainted()
    {
        if (CurHP < 1f)
            return true;

        return false;
    }
}
