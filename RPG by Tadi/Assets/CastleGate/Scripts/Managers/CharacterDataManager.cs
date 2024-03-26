using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDataManager
{
    public enum CharacterType
    {
        None, Knight, Rogue, Wizzard, Orc, SkeletonMage
    }

    public List<string> characterNames = new List<string>()
    {
        "�̼���", "���", "����", "������", "��ũ", "���̷��� ������"
    };

    public enum PropertyType
    {
        None, Earth, Water, Fire, Wind
    }

    public enum StateType
    {
        None, Poison, Burn, Paralysis, Freeze, Confusion, Sleep
    }

    public enum DamageType
    {
        None, Physical, Magic, PhysicalAndMagic, Fixed, Percentage
    }

    public enum AttackType
    {
        None, Melee, Ranged, Magic
    }

    public enum PercentageValue
    {
        None, CurrentHP, MaxHP
    }

    public enum BossModifierType
    {
        None, Damage, Accuracy
    }
}