using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterBaseData baseData;

    private CharacterAnimation characterAnimation;

    private DataManager.CharacterType type;

    private int level;
    private int hp;
    private int attack;
    private int defense;
    private int spAtk;
    private int spDef;
    private int speed;

    private int curHP;

    public CharacterBaseData BaseData
    {
        get { return baseData; }
        set
        {
            baseData = value;
            characterAnimation.SetAnimData(baseData);
            type = baseData.type;
        }
    }

    public DataManager.CharacterType Type { get { return type; } }

    // Stats
    public int Level
    {
        get { return level; }
        set { level = value; SetStats(value); }
    }
    public int CurHP { get { return curHP; } }
    public int Attack { get { return attack; } }
    public int Defense { get { return defense; } }
    public int SpAtk { get { return spAtk; } }
    public int SpDef { get { return spDef; } }
    public int Speed { get { return speed; } }

    private void Awake()
    {
        characterAnimation = GetComponent<CharacterAnimation>();
    }

    private void SetStats(int level)
    {
        curHP = hp = BattleDataManager.Instance.GetHP(level, baseData.BaseHP, baseData.IVHP);
        attack = BattleDataManager.Instance.GetStat(level, baseData.BaseAttack, baseData.IVAttack);
        defense = BattleDataManager.Instance.GetStat(level, baseData.BaseDefense, baseData.IVDefense);
        spAtk = BattleDataManager.Instance.GetStat(level, baseData.BaseSpAtk, baseData.IVSpAtk);
        spDef = BattleDataManager.Instance.GetStat(level, baseData.BaseSpDef, baseData.IVSpDef);
        speed = BattleDataManager.Instance.GetStat(level, baseData.BaseSpeed, baseData.IVSpeed);
    }

    public void TakeDamage(int behaviorLevel, int behaviorAttack)
    {
        int damage = BattleDataManager.Instance.GetDamage(behaviorLevel, behaviorAttack, defense);

        Debug.Log($"hp : {hp}");
        Debug.Log($"curHP : {curHP}");
        Debug.Log($"damage : {damage}");

        curHP -= damage;

        if (curHP < 0)
        {
            curHP = 0;
        }

        Debug.Log($"curHP : {curHP}");
        Debug.Log($"-- Turn Finished --");
    }
}
