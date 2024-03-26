using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.Playables;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterType", menuName = "Scriptable Objects/CharacterType", order = 1)]
public class CharacterBaseData : ScriptableObject
{
    // -- Variables --
    [SerializeField] public CharacterDataManager.CharacterType characterType;
    [SerializeField] public CharacterDataManager.DamageType damageType;
    [SerializeField] public CharacterDataManager.AttackType attackType;
    [SerializeField][TextArea] private string description;

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
    [SerializeField] private List<CombatSkill> skills;

    // -- Properties --
    public CharacterDataManager.CharacterType CharacterType { get { return characterType; } }
    public CharacterDataManager.DamageType DamageType { get { return damageType; } }
    public CharacterDataManager.AttackType AttackType { get { return attackType; } }

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

    public List<CombatSkill> Skills { get { return skills; } }
}

[System.Serializable]
public class CombatSkill
{
    [SerializeField] private CombatSkillBaseData skillBaseData;
    [SerializeField] private int learnLevel = 1;
    [SerializeField] private int skillLevel = 1;

    //public CombatSkillBaseData BaseData { get { return skillBaseData; } }
    public string Name { get { return skillBaseData.Name; } }
    public string Description { get { return skillBaseData.Ability[skillLevel].Description; } }
    public CharacterDataManager.DamageType AttackType { get {  return skillBaseData.AttackType; } }
    public float Power
    {
        get
        {
            float power = 1f;

            switch (skillBaseData.AttackType)
            {
                case CharacterDataManager.DamageType.Physical:
                    power = skillBaseData.Ability[skillLevel].PhysicalPower;
                    break;
                case CharacterDataManager.DamageType.Magic:
                    power = skillBaseData.Ability[skillLevel].MagicPower;
                    break;
            }

            return power;
        }
    }
    public int LearnLevel { get { return learnLevel; } }
    public int SkillLevel
    {
        get { return skillLevel; }
        set
        {
            // Ensure that the count of elements doesn't exceed the limit
            if (value <= BattleDataManager.COMBAT_SKILL_MAX_LEVEL)
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
