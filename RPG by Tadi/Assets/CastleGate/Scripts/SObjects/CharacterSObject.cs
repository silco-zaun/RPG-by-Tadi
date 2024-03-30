using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Objects/Character", order = 1)]
public class CharacterSObject : ScriptableObject
{
    // -- Variables --
    [SerializeField] private Tadi.Datas.Unit.UnitType characterType;
    [SerializeField] private Tadi.Datas.Combat.DamageType damageType;
    [SerializeField] private Tadi.Datas.Combat.AttackType attackType;
    [SerializeField] private Tadi.Datas.Weapon.BulletType bulletType;

    // Graphic
    [SerializeField] private Sprite sprBody;
    [SerializeField] private Sprite sprLeftHand;
    [SerializeField] private Sprite sprLeftWeapon;
    [SerializeField] private Sprite sprRightHand;
    [SerializeField] private Sprite sprRightWeapon;

    // Base stat values
    // Average 100 Total 600
    [SerializeField] private int baseHP;
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseDefense;
    [SerializeField] private int baseMagicAtk;
    [SerializeField] private int baseMagicDef;
    [SerializeField] private int baseSpeed;

    // Individual values
    // Average 20 Total 120
    [SerializeField] private int ivHP;
    [SerializeField] private int ivAttack;
    [SerializeField] private int ivDefense;
    [SerializeField] private int ivMagicAtk;
    [SerializeField] private int ivMagicDef;
    [SerializeField] private int ivSpeed;
    
    // Skills
    [SerializeField] private List<CombatSkill_> skills;

    // -- Properties --
    public Tadi.Datas.Unit.UnitType CharacterType { get { return characterType; } }
    public Tadi.Datas.Combat.DamageType DamageType { get { return damageType; } }
    public Tadi.Datas.Combat.AttackType AttackType { get { return attackType; } }
    public Tadi.Datas.Weapon.BulletType BulletType { get { return bulletType; } }

    public Sprite SprBody { get { return sprBody; } }
    public Sprite SprLeftHand { get {  return sprLeftHand; } }
    public Sprite SprRightHand { get { return sprRightHand; } }
    public Sprite SprLeftWeapon { get {  return sprLeftWeapon; } }
    public Sprite SprRightWeapon { get { return sprRightWeapon; } }

    public int BaseHP { get { return baseHP; } }
    public int BaseAttack { get { return baseAttack; } }
    public int BaseDefense { get {  return baseDefense; } }
    public int BaseMagicAtk { get {  return baseMagicAtk; } }
    public int BaseMagicDef { get {  return baseMagicDef; } }
    public int BaseSpeed { get {  return baseSpeed; } }

    public int IVHP { get { return ivHP; } }
    public int IVAttack { get { return ivAttack; } }
    public int IVDefense { get { return ivDefense; } }
    public int IVMagicAtk { get { return ivMagicAtk; } }
    public int IVMagicDef { get { return ivMagicDef; } }
    public int IVSpeed { get { return ivSpeed; } }

    public List<CombatSkill_> Skills { get { return skills; } }
}

[System.Serializable]
public class CombatSkill_
{
    [SerializeField] private CombatSkillSObjects skillBaseData;
    [SerializeField] private int learnLevel = 1;
    [SerializeField] private int skillLevel = 1;

    //public CombatSkillSObjects CharacterSObject { get { return skillBaseData; } }
    public string Name { get { return skillBaseData.Name; } }
    public string Description { get { return skillBaseData.Ability[skillLevel].Description; } }
    public Tadi.Datas.Combat.DamageType DamageType { get {  return skillBaseData.AttackDamageType; } }
    public Tadi.Datas.Combat.AttackType AttackType { get {  return skillBaseData.AttackType; } }
    public float Power
    {
        get
        {
            float power = 1f;

            switch (skillBaseData.AttackDamageType)
            {
                case Tadi.Datas.Combat.DamageType.Physical:
                    power = skillBaseData.Ability[skillLevel].PhysicalPower;
                    break;
                case Tadi.Datas.Combat.DamageType.Magic:
                    power = skillBaseData.Ability[skillLevel].MagicPower;
                    break;
            }

            return power;
        }
    }
    public Tadi.Datas.Weapon.BulletType Bullet { get { return skillBaseData.Bullet; } }

    public int LearnLevel { get { return learnLevel; } }
    public int SkillLevel
    {
        get { return skillLevel; }
        set
        {
            // Ensure that the count of elements doesn't exceed the limit
            if (value <= 3)
            {
                skillLevel = value;
            }
            else
            {
                Debug.LogError("Attempted to set value with more number than allowed.");
                // Optionally, you could truncate the list or take other action here
            }
        }
    }
}
