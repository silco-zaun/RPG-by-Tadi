using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Combat Skill", menuName = "Scriptable Objects/Combat Skill")]
public class CombatSkillSO : ScriptableObject
{
    [SerializeField] private string skillName;
    [SerializeField] private List<CombatSkillAbilityByLevel> ability;
    [SerializeField] private int cooltime;
    [SerializeField] private CombatSKillPowerType powerType;
    [SerializeField] private CombatSkillTarget target;
    [SerializeField] private CombatSkillRange range;
    [SerializeField] private CombatSkillHitType hitType;
    // Action
    [SerializeField] private CombatSkillActionType actionType;
    [SerializeField] private Datas.DamageType attackDamageType;
    [SerializeField] private Datas.PercentageType percentageValue;
    [SerializeField] private float bossModifier;
    [SerializeField] private Datas.BossModifierType bossModifierType;
    [SerializeField] private Datas.DamageType defenseDamageType;
    [SerializeField] private Datas.BulletType skillBulletType;
    [SerializeField] private Datas.AttackType attackType;
    // Effect
    [SerializeField] private CombatSkillEffectType effectType;
    // Cure
    [SerializeField] private CombatSkillCureType cureType;
    // Etc..
    [SerializeField] private Datas.PropertyType propertyType;
    [SerializeField] private Datas.StateType stateType;

    public string Name { get { return skillName; } }
    public int Cooltime { get { return cooltime; } }
    public CombatSKillPowerType PowerType { get { return powerType; } }
    public CombatSkillTarget Target { get { return target; } }
    public CombatSkillRange Range { get { return range; } }
    public CombatSkillHitType HitType { get { return hitType; } }
    // Action
    public CombatSkillActionType ActionType { get { return actionType; } }
    public Datas.DamageType AttackDamageType { get { return attackDamageType; } }
    public Datas.DamageType DefenseDamageType { get { return defenseDamageType; } }
    public Datas.BulletType Bullet { get { return skillBulletType; } }
    public Datas.AttackType AttackType { get { return attackType; } }

    public CombatSkillEffectType EffectType { get { return effectType; } }
    public CombatSkillCureType CureType { get { return cureType; } }
    public Datas.PropertyType PropertyType { get { return propertyType; } }
    public Datas.StateType StateType { get { return stateType; } }

    public List<CombatSkillAbilityByLevel> Ability
    {
        get { return ability; }
        set
        {
            // Ensure that the count of elements doesn't exceed the limit
            if (value.Count <= Datas.Bat.COMBAT_SKILL_MAX_LEVEL)
            {
                ability = value;
            }
            else
            {
                Debug.LogError("Attempted to set list with more elements than allowed.");
                // Optionally, you could truncate the list or take other action here
            }
        }
    }
}

[System.Serializable]
public class CombatSkillAbilityByLevel
{
    [SerializeField][TextArea] private string description;
    [SerializeField] private float physicalPower;
    [SerializeField] private float magicPower;
    [SerializeField] private float defensePower;
    [SerializeField] private float magicDefensePower;
    [SerializeField] private float fixedDamage;
    [SerializeField] private float percentageAmount;
    [SerializeField] private float accuracy;
    [SerializeField] private float evade;

    public string Description { get { return description; } }
    public float PhysicalPower {  get { return physicalPower; } }
    public float MagicPower {  get { return magicPower; } }
    public float DefensePower {  get { return defensePower; } }
    public float MagicDefensePower {  get { return magicDefensePower; } }
    public float FixedDamage {  get { return fixedDamage; } }
    public float PercentageAmount {  get { return percentageAmount; } }
    public float Accuracy {  get { return accuracy; } }
    public float Evade {  get { return evade; } }
}

public enum CombatSkillActionType
{
    None, Attack, Defense, AttackAndDefense
}

public enum CombatSkillEffectType
{
    None, Buffs, Debuffs, BuffsAndDebuffs
}

public enum CombatSkillCureType
{
    None, Healing, Recovery, Revival
}

public enum CombatSKillPowerType
{
    None, Normal, Ultimate
}

public enum CombatSkillTarget
{
    Ally, Enemy, All
}

public enum CombatSkillRange
{
    One, Self, Front, Rear, FrontAll, RearAll, HLine, VLine, All, RandomOne
}

public enum CombatSkillHitType
{
    Always,
    CharacterAccuracy,
    SkillAccuracy
}

