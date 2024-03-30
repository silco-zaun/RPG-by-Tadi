using System.Collections;
using System.Collections.Generic;
using Tadi.Datas.Unit;
using UnityEngine;

namespace Tadi.Datas.BattleSystem
{
    public enum BattleState
    {
        SelectPlayerUnit, SelectAction, SelectTarget, SelectSkill, SelectSkillTarget, ProgressRound
    }

    public enum BattleCondition
    {
        None, Victory, Defeated, Draw
    }

    public enum UnitParty
    {
        PlayerParty, EnemyParty
    }

    public enum UnitAction
    {
        None,
        Attack,
        Defense,
        Skill,
        //Item,
        //Party,
        //Formation,
        Escape
    }

    public enum BattlePos
    {
        Front,
        Rear
    }

    public enum UnitActionKor
    {
        대기,
        공격,
        방어,
        스킬,
        //아이템,
        //파티,
        //진형,
        도망
    }

    public enum UnitActionPriority
    {
        NFive = -5,
        NFour,
        NThree,
        NTwo,
        NOne,
        Zero,
        One,
        Tow,
        Three,
        Four,
        Five,
    }

    [System.Serializable]
    public class BattleUnitInfo
    {
        // -- Variables --
        [SerializeField] private UnitType unitType;
        [SerializeField] private BattlePos unitPos;
        [SerializeField] private int unitLevel;
        [SerializeField] private UnitType partnerType;
        [SerializeField] private int partnerLevel;
        [SerializeField] private UnitParty party;

        // -- Formation --
        public UnitType UnitType { get { return unitType; } }
        public BattlePos UnitPos { get { return unitPos; } }
        public int UnitLevel { get { return unitLevel; } }
        public UnitType PartnerType { get { return partnerType; } }
        public int PartnerLevel { get { return partnerLevel; } }
        public UnitParty Party { get { return party; } }
    }
}
