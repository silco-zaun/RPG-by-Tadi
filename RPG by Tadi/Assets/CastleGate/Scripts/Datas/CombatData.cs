using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tadi.Datas.Combat
{
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
        None, Melee, Ranged
    }
}
