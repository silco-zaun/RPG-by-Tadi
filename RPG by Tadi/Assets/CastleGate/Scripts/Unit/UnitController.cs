
using UnityEngine;
using Tadi.Utils;
using Tadi.Datas.Unit;
using Tadi.Datas.Combat;
using Tadi.Datas.Weapon;

public class UnitController : MonoBehaviour
{
    [SerializeField] private SlideBarController healthBar;

    private UnitTypeData unitTypeData;
    private UnitAnimation unitAnim;
    private DamageFlash damageFlash;

    private UnitType unitType;
    private DamageType damageType;
    private AttackType attackType;
    private BulletType bulletType;

    private int level;
    private float maxHP;
    private float curHP;
    private float attack;
    private float defense;
    private float magicAttack;
    private float magicDefense;
    private float speed;

    public System.Action OnFainted;

    public UnitType CharacterType { get { return unitType; } }
    public DamageType DamageType { get { return damageType; } }
    public AttackType AttackType { get { return attackType; } }
    public BulletType BulletType { get { return bulletType; } }

    // Stats
    public int Level
    {
        get { return level; }
        set { level = value; SetStats(value); }
    }
    public float CurHP { get { return curHP; } }
    public float Attack { get { return attack; } }
    public float Defense { get { return defense; } }
    public float MagicAttack { get { return magicAttack; } }
    public float MagicDefense { get { return magicDefense; } }
    public float Speed { get { return speed; } }

    private void Awake()
    {
        unitAnim = GetComponent<UnitAnimation>();
        damageFlash = GetComponent<DamageFlash>();
    }

    public void Init(UnitType type, int level)
    {
        unitTypeData = Managers.Ins.Unit.GetUnitTypeData(type);
        unitType = unitTypeData.UnitType;
        damageType = unitTypeData.DamageType;
        attackType = unitTypeData.AttackType;
        bulletType = unitTypeData.BulletType;
        unitAnim.SetAnimationData(unitTypeData.UnitRes);
        SetStats(level);
    }

    private void SetStats(int level)
    {
        curHP = maxHP = Battle.GetHP(level, unitTypeData.BaseHP, unitTypeData.IVHP);
        attack = Battle.GetStat(level, unitTypeData.BaseAttack, unitTypeData.IVAttack);
        defense = Battle.GetStat(level, unitTypeData.BaseDefense, unitTypeData.IVDefense);
        magicAttack = Battle.GetStat(level, unitTypeData.BaseMagicAttack, unitTypeData.IVMagicAttack);
        magicDefense = Battle.GetStat(level, unitTypeData.BaseMagicDefense, unitTypeData.IVMagicDefense);
        speed = Battle.GetStat(level, unitTypeData.BaseSpeed, unitTypeData.IVSpeed);
    }

    public void TakeDamage(UnitController attacker, DamageType attackType, bool defending, float skillMultiplier = 1f)
    {
        damageFlash.CallDamageFlash();

        float attackersAttack = 0f;
        float defense = 0f;

        switch (attackType)
        {
            case DamageType.Physical:
                attackersAttack = attacker.Attack;
                defense = this.defense;
                break;
            case DamageType.Magic:
                attackersAttack = attacker.MagicAttack;
                defense = this.magicDefense;
                break;
            default:
                Debug.LogError("enum variable [DamageType] must be set.");
                return;
        }

        float damage = Battle.GetDamage(attackType, attacker.Level, attackersAttack, defense, skillMultiplier, defending);

        curHP -= damage;
        float normalizedHP = System.Math.Clamp(curHP / maxHP, 0f, maxHP);
        healthBar.SetBar(normalizedHP);

        bool isFainted = CheckIsFainted();

        if (isFainted)
        {
            OnFainted?.Invoke();
        }
    }

    public bool CheckIsFainted()
    {
        if (curHP < 1f)
            return true;

        return false;
    }
}
