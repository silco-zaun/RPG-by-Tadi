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
        "미설정", "기사", "도적", "마법사", "오크", "스켈레톤 메이지"
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
