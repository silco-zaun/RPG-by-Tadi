using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tadi.Datas.Combat;
using Tadi.Datas.CombatSkill;
using Tadi.Datas.Weapon;

[CreateAssetMenu(fileName = "New Combat Skill", menuName = "Scriptable Objects/Combat Skill")]
public class CombatSkillSObjects : ScriptableObject
{
    [SerializeField] private string skillName;
    [SerializeField] private List<CombatSkillAbilityByLevel> ability;
    [SerializeField] private int cooltime;
    [SerializeField] private PowerType powerType;
    [SerializeField] private Target target;
    [SerializeField] private Range range;
    [SerializeField] private HitType hitType;
    // Action
    [SerializeField] private ActionType actionType;
    [SerializeField] private DamageType attackDamageType;
    [SerializeField] private PercentageType percentageValue;
    [SerializeField] private float bossModifier;
    [SerializeField] private BossModifierType bossModifierType;
    [SerializeField] private DamageType defenseDamageType;
    [SerializeField] private BulletType skillBulletType;
    [SerializeField] private AttackType attackType;
    // Effect
    [SerializeField] private EffectType effectType;
    // Cure
    [SerializeField] private CureType cureType;
    // Etc..
    [SerializeField] private PropertyType propertyType;
    [SerializeField] private StateType stateType;

    public string Name { get { return skillName; } }
    public int Cooltime { get { return cooltime; } }
    public PowerType PowerType { get { return powerType; } }
    public Target Target { get { return target; } }
    public Range Range { get { return range; } }
    public HitType HitType { get { return hitType; } }
    // Action
    public ActionType ActionType { get { return actionType; } }
    public DamageType AttackDamageType { get { return attackDamageType; } }
    public DamageType DefenseDamageType { get { return defenseDamageType; } }
    public BulletType Bullet { get { return skillBulletType; } }
    public AttackType AttackType { get { return attackType; } }

    public EffectType EffectType { get { return effectType; } }
    public CureType CureType { get { return cureType; } }
    public PropertyType PropertyType { get { return propertyType; } }
    public StateType StateType { get { return stateType; } }

    public List<CombatSkillAbilityByLevel> Ability
    {
        get { return ability; }
        set
        {
            // Ensure that the count of elements doesn't exceed the limit
            if (value.Count <= 3)
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


