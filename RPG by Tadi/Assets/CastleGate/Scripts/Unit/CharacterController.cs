using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterBaseData baseData;
    [SerializeField] private SlideBarController healthBar;

    private CharacterAnimation characterAnimation;

    private CharacterDataManager.CharacterType characterType;
    private CharacterDataManager.DamageType damageType;
    private CharacterDataManager.AttackType attackType;

    private int level;
    private float hp;
    private float attack;
    private float defense;
    private float magicAttack;
    private float magicDefense;
    private float speed;

    private float curHP;

    public System.Action OnFainted;

    public CharacterBaseData BaseData
    {
        get { return baseData; }
        set
        {
            baseData = value;
            characterAnimation.SetAnimationData(baseData);
            characterType = baseData.characterType;
            damageType = baseData.damageType;
            attackType = baseData.attackType;

            if (baseData.characterType == CharacterDataManager.CharacterType.None)
                Debug.LogError($"Enum variable [CharacterType] must to be set.");
            if (baseData.damageType == CharacterDataManager.DamageType.None)
                Debug.LogError($"Enum variable [DamageType] must to be set.");
            if (baseData.attackType == CharacterDataManager.AttackType.None)
                Debug.LogError($"Enum variable [AttackType] must to be set.");
        }
    }

    public CharacterDataManager.CharacterType CharacterType { get { return characterType; } }
    public CharacterDataManager.DamageType DamageType { get { return damageType; } }
    public CharacterDataManager.AttackType AttackType { get { return attackType; } }

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
        characterAnimation = GetComponent<CharacterAnimation>();
    }

    private void SetStats(int level)
    {
        curHP = hp = DataManager.Ins.Bat.GetHP(level, baseData.BaseHP, baseData.IVHP);
        attack = DataManager.Ins.Bat.GetStat(level, baseData.BaseAttack, baseData.IVAttack);
        defense = DataManager.Ins.Bat.GetStat(level, baseData.BaseDefense, baseData.IVDefense);
        magicAttack = DataManager.Ins.Bat.GetStat(level, baseData.BaseMagicAtk, baseData.IVMagicAtk);
        magicDefense = DataManager.Ins.Bat.GetStat(level, baseData.BaseMagicDef, baseData.IVMagicDef);
        speed = DataManager.Ins.Bat.GetStat(level, baseData.BaseSpeed, baseData.IVSpeed);
    }

    public void TakeDamage(CharacterController attacker, CharacterDataManager.DamageType attackType, bool defending, float skillMultiplier = 1f)
    {
        float attackersAttack = 0f;
        float defense = 0f;

        switch (attackType)
        {
            case CharacterDataManager.DamageType.Physical:
                attackersAttack = attacker.Attack;
                defense = this.defense;
                break;
            case CharacterDataManager.DamageType.Magic:
                attackersAttack = attacker.MagicAttack;
                defense = this.magicDefense;
                break;
            default:
                Debug.LogError("enum variable [DamageType] must be set.");
                return;
        }

        float damage = DataManager.Ins.Bat.GetDamage(attackType, attacker.Level, attackersAttack, defense, skillMultiplier, defending);

        curHP -= damage;
        float normalizedHP = System.Math.Clamp(curHP / hp, 0f, hp);
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
