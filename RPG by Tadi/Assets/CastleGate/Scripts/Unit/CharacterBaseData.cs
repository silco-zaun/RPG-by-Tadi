using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "New CharacterType", menuName = "ScriptableObjects/CharacterType", order = 1)]
public class CharacterBaseData : ScriptableObject
{
    [SerializeField] public DataManager.CharacterType type;

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
    [SerializeField] private int baseSpAtk;
    [SerializeField] private int baseSpDef;
    [SerializeField] private int baseSpeed;

    // Individual values
    // Average 20 Total 120
    [SerializeField] private int ivHP;
    [SerializeField] private int ivAttack;
    [SerializeField] private int ivDefense;
    [SerializeField] private int ivSpAtk;
    [SerializeField] private int ivSpDef;
    [SerializeField] private int ivSpeed;

    public DataManager.CharacterType Character { get { return type; } }

    public Sprite SprBody { get { return sprBody; } }
    public Sprite SprLeftHand { get {  return sprLeftHand; } }
    public Sprite SprRightHand { get { return sprRightHand; } }
    public Sprite SprLeftWeapon { get {  return sprLeftWeapon; } }
    public Sprite SprRightWeapon { get { return sprRightWeapon; } }

    public int BaseHP { get { return baseHP; } }
    public int BaseAttack { get { return baseAttack; } }
    public int BaseDefense { get {  return baseDefense; } }
    public int BaseSpAtk { get {  return baseSpAtk; } }
    public int BaseSpDef { get {  return baseSpDef; } }
    public int BaseSpeed { get {  return baseSpeed; } }

    public int IVHP { get { return ivHP; } }
    public int IVAttack { get { return ivAttack; } }
    public int IVDefense { get { return ivDefense; } }
    public int IVSpAtk { get { return ivSpAtk; } }
    public int IVSpDef { get { return ivSpDef; } }
    public int IVSpeed { get { return ivSpeed; } }
        
}
