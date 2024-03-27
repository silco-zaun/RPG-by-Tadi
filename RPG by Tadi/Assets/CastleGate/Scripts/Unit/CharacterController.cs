using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private CharacterSO characterSO;
    [SerializeField] private SlideBarController healthBar;

    private CharacterAnimation characterAnimation;
    private DamageFlash damageFlash;

    private Datas.CharacterType characterType;
    private Datas.DamageType damageType;
    private Datas.AttackType attackType;
    private Datas.BulletType bulletType;

    private int level;
    private float hp;
    private float attack;
    private float defense;
    private float magicAttack;
    private float magicDefense;
    private float speed;

    private float curHP;

    public System.Action OnFainted;

    public CharacterSO CharacterSO
    {
        get { return characterSO; }
        set
        {
            characterSO = value;
            characterAnimation.SetAnimationData(characterSO);
            characterType = characterSO.CharacterType;
            damageType = characterSO.DamageType;
            attackType = characterSO.AttackType;
            bulletType = characterSO.BulletType;

            if (characterSO.CharacterType == Datas.CharacterType.None)
                Debug.LogError($"Enum variable [CharacterType] must to be set.");
            if (characterSO.DamageType == Datas.DamageType.None)
                Debug.LogError($"Enum variable [DamageType] must to be set.");
            if (characterSO.AttackType == Datas.AttackType.None)
                Debug.LogError($"Enum variable [DamageType] must to be set.");
            if ((characterSO.AttackType == Datas.AttackType.Ranged ||
                characterSO.AttackType == Datas.AttackType.Magic) &&
                characterSO.BulletType == Datas.BulletType.None)
                Debug.LogError($"Enum variable [BulletType] must to be set.");
        }
    }

    public Datas.CharacterType CharacterType { get { return characterType; } }
    public Datas.DamageType DamageType { get { return damageType; } }
    public Datas.AttackType AttackType { get { return attackType; } }
    public Datas.BulletType BulletType { get { return bulletType; } }

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
        damageFlash = GetComponent<DamageFlash>();
    }

    private void SetStats(int level)
    {
        curHP = hp = Datas.Bat.GetHP(level, characterSO.BaseHP, characterSO.IVHP);
        attack = Datas.Bat.GetStat(level, characterSO.BaseAttack, characterSO.IVAttack);
        defense = Datas.Bat.GetStat(level, characterSO.BaseDefense, characterSO.IVDefense);
        magicAttack = Datas.Bat.GetStat(level, characterSO.BaseMagicAtk, characterSO.IVMagicAtk);
        magicDefense = Datas.Bat.GetStat(level, characterSO.BaseMagicDef, characterSO.IVMagicDef);
        speed = Datas.Bat.GetStat(level, characterSO.BaseSpeed, characterSO.IVSpeed);
    }

    public void TakeDamage(CharacterController attacker, Datas.DamageType attackType, bool defending, float skillMultiplier = 1f)
    {
        damageFlash.CallDamageFlash();

        float attackersAttack = 0f;
        float defense = 0f;

        switch (attackType)
        {
            case Datas.DamageType.Physical:
                attackersAttack = attacker.Attack;
                defense = this.defense;
                break;
            case Datas.DamageType.Magic:
                attackersAttack = attacker.MagicAttack;
                defense = this.magicDefense;
                break;
            default:
                Debug.LogError("enum variable [DamageType] must be set.");
                return;
        }

        float damage = Datas.Bat.GetDamage(attackType, attacker.Level, attackersAttack, defense, skillMultiplier, defending);

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
