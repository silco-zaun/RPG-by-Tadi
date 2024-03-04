using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class BattleDataManager
{
    private static BattleDataManager instance = null;

    public static BattleDataManager Instance
    {
        get
        {
            if (instance == null)
                instance = new BattleDataManager();

            return instance;
        }
    }

    private BattleDataManager() { }

    private const int MIN_LEVEL = 1;
    private const int MAX_LEVEL = 100;
    private const int MIN_BASE_STAT = 1;
    private const int MAX_BASE_STAT = 200;
    private const int MIN_IV_STAT = 1;
    private const int MAX_IV_STAT = 40;

    public int GetHP(int level, int baseHP, int ivHP)
    {
        // Base stat values : Average 100 Total 600
        // Individual values : Average 20 Total 120

        int evHP = ivHP; // Temp value

        int hp = ((baseHP * 2 + ivHP + evHP) * level) / 100 + level + 10;

        return hp;
    }

    public int GetStat(int level, int baseStat, int ivStat)
    {
        // Base stat values : Average 100 min 0 max 200 Total 600
        // Individual values : Average 20 min 0 max 40 Total 120

        int evStat = ivStat; // Temp value
        int nature = 1; // Temp value

        int stat = (((baseStat * 2 + ivStat + evStat) * level) / 100 + 5) * nature;

        return stat;
    }

    private int GetMinStat(int level)
    {
        int minStat = GetStat(level, MIN_BASE_STAT, MIN_IV_STAT);

        return minStat;
    }

    private int GetMaxStat(int level)
    {
        int maxStat = GetStat(level, MAX_BASE_STAT, MAX_IV_STAT);

        return maxStat;
    }

    public int GetDamage(int behaviorLevel, int behaviorAttack, int targetDefense)
    {
        int skillPower = 50; // Temp value
        float critical = GetCritical(0); // Temp value
        float random = Random.Range(0.85f, 1.15f);

        float unmodifiedDamage = (behaviorLevel * 2 / 5 + 2) * skillPower * behaviorAttack / targetDefense / 50 + 2;
        float modifiers = random * critical;
        int damage = Mathf.FloorToInt(unmodifiedDamage * modifiers);

        return damage;
    }

    private float GetCritical(int stage)
    {
        float critical = 1f;
        float[] criticalChance = { 6.25f, 12.5f, 50f, 100f };

        if (Random.value * 100f <= criticalChance[stage])
        {
            critical = 2f;
        }

        return critical;
    }

    public float GetHitChance(int behaviorLevel, int targetLevel, int behaviorSpeed, int targetSpeed)
    {
        float hitChanceBasedOnSpeed = GetHitChanceBasedOnSpeed(behaviorLevel, behaviorSpeed, targetLevel, targetSpeed); // 0.8 ~ 1
        float skillAccuracy = GetHitChanceBasedOnSkillAccuracy();
        float random = Random.Range(0.95f, 1.05f);

        // speed 0 ~ 5 : behaviorLevel 0 ~ 5 : skill 0 ~ 2
        float hitChance = hitChanceBasedOnSpeed * skillAccuracy * random;

        return hitChance;
    }

    private float GetHitChanceBasedOnSkillAccuracy()
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

    private float GetHitChanceBasedOnSpeed(int behaviorLevel, int behaviorSpeed, int targetLevel, int targetSpeed)
    {
        float speedAccuracy = GetAccuracy(behaviorLevel, behaviorSpeed); // 0.9 ~ 1
        float speedEvasion = GetEvasion(targetLevel, targetSpeed); // 0 ~ 0.1
        float hitChance = speedAccuracy - speedEvasion; // 0.8 ~ 1

        return hitChance;
    }

    private float GetHitChanceModifierBasedOnLevel(int behaviorLevel, int targetLevel)
    {
        float modifier = 1f + (behaviorLevel - targetLevel) / (MAX_LEVEL - MIN_LEVEL) * 0.1f; // 0.9 ~ 1.1

        return modifier;
    }

    private float GetAccuracy(int level, int speed)
    {
        float accuracy = 1f;
        float baseAccuracy = 0.95f;
        float minSpeed = GetMinStat(level);
        float maxSpeed = GetMaxStat(level);
        float modifier = -0.05f + (speed - minSpeed) / (maxSpeed - minSpeed) * 0.1f; // -0.05 ~ 0.05

        accuracy = baseAccuracy + modifier; // 0.9 ~ 1.0

        return accuracy;
    }

    private float GetEvasion(int level, int speed)
    {
        float evasion = 1f;
        float baseEvasion = 0.05f;
        float minSpeed = GetMinStat(level);
        float maxSpeed = GetMinStat(level);
        float speedModifier = -0.05f + (speed - minSpeed) / (maxSpeed - minSpeed) * 0.1f; // -0.05 ~ 0.05

        evasion = baseEvasion + speedModifier; // 0 ~ 0.1

        return evasion;
    }
}
