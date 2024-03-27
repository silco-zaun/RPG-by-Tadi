using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas
{
    public enum BattleState
    {
        SelectPlayerUnit, SelectAction, SelectTarget, SelectSkill, SelectSkillTarget, ProgressRound
    }

    public enum BattleCondition
    {
        None, Victory, Defeated, Draw
    }

    public enum BattleHorizontalLine
    {
        First, Second, Third
    }

    public enum BattleVertialLine
    {
        First, Second
    }

    public enum BattleFormationRange
    {
        Single, Front, Rear, FirstLine, SecondLine, ThirdLine, Party, All
    }

    public enum BattleUnitParty
    {
        PlayerParty, EnemyParty
    }

    public enum BattleUnitAction
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

    public enum BattleUnitActionKor
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

    public enum BattleUnitActionPriority
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

    public static class Bat
    {
        // Bat info
        public const int HORIZONTAL_LINE_UNIT_COUNT = 2;
        public const int VERTICAL_LINE_UNIT_COUNT = 3;
        public const int PARTY_UNIT_COUNT = 6;
        public const int COMBAT_SKILL_MAX_LEVEL = 3;

        // CharacterType stat
        private const int MIN_LEVEL = 1;
        private const int MAX_LEVEL = 100;
        private const float MIN_BASE_STAT = 1f;
        private const float MAX_BASE_STAT = 200f;
        private const float MIN_IV_STAT = 1f;
        private const float MAX_IV_STAT = 40f;

        public static float GetHP(int level, float baseHP, float ivHP)
        {
            // Base stat values : Average 100 Total 600
            // Individual values : Average 20 Total 120

            float evHP = ivHP; // Temp value

            float hp = ((baseHP * 2f + ivHP + evHP) * level) / 100f + level + 10f;

            return hp;
        }

        public static float GetStat(int level, float baseStat, float ivStat)
        {
            // Base stat values : Average 100 min 0 max 200 Total 600
            // Individual values : Average 20 min 0 max 40 Total 120

            float evStat = ivStat; // Temp value
            float nature = 1f; // Temp value

            float stat = (((baseStat * 2f + ivStat + evStat) * level) / 100f + 5f) * nature;

            return stat;
        }

        private static float GetMinStat(int level)
        {
            float minStat = GetStat(level, MIN_BASE_STAT, MIN_IV_STAT);

            return minStat;
        }

        private static float GetMaxStat(int level)
        {
            float maxStat = GetStat(level, MAX_BASE_STAT, MAX_IV_STAT);

            return maxStat;
        }

        public static float GetDamage(Datas.DamageType attackType, int attackersLevel, float attackersAttack, float defendersDefense, float skillMultiplier, bool defending)
        {
            float type = 1f;

            switch (attackType)
            {
                case Datas.DamageType.Physical:
                    break;
                case Datas.DamageType.Magic:
                    type = 0.8f;
                    defending = false;
                    break;
            }

            float critical = GetCritical(0);
            float defensive = defending ? 0.1f : 1f;
            float random = Random.Range(0.85f, 1.15f);

            float unmodifiedDamage = (attackersLevel * 2 / 5 + 2) * attackersAttack / defendersDefense + 2;
            float modifiers = type * critical * skillMultiplier * defensive * random;
            float damage = Mathf.FloorToInt(unmodifiedDamage * modifiers);

            return damage;
        }

        private static float GetCritical(int stage)
        {
            float critical = 1f;
            float[] criticalChance = { 6.25f, 12.5f, 50f, 100f };

            if (Random.value * 100f <= criticalChance[stage])
            {
                critical = 2f;
            }

            return critical;
        }

        public static float GetHitChance(int attackersLevel, int defendersLevel, float attackersSpeed, float defendersSpeed)
        {
            float hitChanceBasedOnSpeed = GetHitChanceBasedOnSpeed(attackersLevel, attackersSpeed, defendersLevel, defendersSpeed); // 0.8 ~ 1
            float skillAccuracy = GetHitChanceBasedOnSkillAccuracy();
            float random = Random.Range(0.95f, 1.05f);

            // speed 0 ~ 5 : attackersLevel 0 ~ 5 : usingSkill 0 ~ 2
            float hitChance = hitChanceBasedOnSpeed * skillAccuracy * random;

            return hitChance;
        }

        private static float GetHitChanceBasedOnSkillAccuracy()
        {
            // Temp code
            float skillAccuracy = 1f;
            //float[] multipliers =
            //{
            //    3f / 9f, 3f / 8f, 3f / 7f, 3f / 6f, 3f / 5f, 3f / 4f, 3f / 3f,
            //    4f / 3f, 5f / 3f, 6f / 3f, 7f / 3f, 8f / 3f, 9f / 3f
            //}; // 0 ~ 12

            //int stage = evasionStage - accuracyStage;
            //Mathf.Clamp(stage, 0, hitChance.Length);

            return skillAccuracy;
        }

        private static float GetHitChanceBasedOnSpeed(int attackersLevel, float attackersSpeed, int defendersLevel, float defendersSpeed)
        {
            float speedAccuracy = GetAccuracy(attackersLevel, attackersSpeed); // 0.9 ~ 1
            float speedEvasion = GetEvasion(defendersLevel, defendersSpeed); // 0 ~ 0.1
            float hitChance = speedAccuracy - speedEvasion; // 0.8 ~ 1

            return hitChance;
        }

        private static float GetHitChanceModifierBasedOnLevel(int attackersLevel, int defendersLevel)
        {
            float modifier = 1f + (attackersLevel - defendersLevel) / (MAX_LEVEL - MIN_LEVEL) * 0.1f; // 0.9 ~ 1.1

            return modifier;
        }

        private static float GetAccuracy(int level, float speed)
        {
            float accuracy = 1f;
            float baseAccuracy = 0.95f;
            float minSpeed = GetMinStat(level);
            float maxSpeed = GetMaxStat(level);
            float modifier = -0.05f + (speed - minSpeed) / (maxSpeed - minSpeed) * 0.1f; // -0.05 ~ 0.05

            accuracy = baseAccuracy + modifier; // 0.9 ~ 1.0

            return accuracy;
        }

        private static float GetEvasion(int level, float speed)
        {
            float evasion = 1f;
            float baseEvasion = 0.05f;
            float minSpeed = GetMinStat(level);
            float maxSpeed = GetMinStat(level);
            float speedModifier = -0.05f + (speed - minSpeed) / (maxSpeed - minSpeed) * 0.1f; // -0.05 ~ 0.05

            evasion = baseEvasion + speedModifier; // 0 ~ 0.1

            return evasion;
        }

        public static BattleUnitActionPriority GetActionPriority(BattleUnitAction action)
        {
            BattleUnitActionPriority priority;

            switch (action)
            {
                case BattleUnitAction.Defense:
                    priority = BattleUnitActionPriority.Five;
                    break;
                case BattleUnitAction.Attack:
                case BattleUnitAction.Escape:
                //case BattleUnitAction.Formation:
                //case BattleUnitAction.Item:
                //case BattleUnitAction.Party:
                case BattleUnitAction.Skill:
                default:
                    priority = BattleUnitActionPriority.Zero;
                    break;
            }

            return priority;
        }

        public static int GetPriority(BattleUnitAction action, float speed)
        {
            int basePriority = 100;
            int speedFactor = 1;

            switch (action)
            {
                case BattleUnitAction.Defense:
                    basePriority = 0;
                    speedFactor = 0;
                    break;
                case BattleUnitAction.Attack:
                case BattleUnitAction.Escape:
                //case BattleUnitAction.Formation:
                //case BattleUnitAction.Item:
                //case BattleUnitAction.Party:
                case BattleUnitAction.Skill:
                default:
                    break;
            }

            int priority = basePriority - (speedFactor * (int)speed);

            return priority;
        }
    }
}
