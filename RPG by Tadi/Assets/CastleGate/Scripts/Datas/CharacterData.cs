using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Datas
{
    public enum CharacterType
    {
        None, Knight, Rogue, Wizzard, Orc, SkeletonMage
    }

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

    public enum PercentageType
    {
        None, CurrentHP, MaxHP
    }

    public enum BossModifierType
    {
        None, Damage, Accuracy
    }

    public static class Cha
    {
        public static List<string> Names = new List<string>()
        {
            "", "기사", "도적", "마법사", "오크", "스켈레톤 메이지"
        };

        public static List<string> Desc = new List<string>()
        {
            "",
            "왕국을 수호하는 기사. 체계적인 훈련으로 공격과 수비 모두에 능하다. 근접 단일 공격에 특화 돼있다.",
            "각종 불법적인 일을 하며 돈을 버는 도적. 왕실의 골칫거리 이다. 특유의 빠른 움직임과 은신, 잠입 능력으로 목표를 달성한다.",
            "왕실을 수호하는 마법사. 모든것을 파괴하는 힘을 가졌지만 방어력이 약하다.",
            "인간과 오랜기간 전쟁중인 오크족. 인간이 자신들의 영역을 침범한다고 주장한다.",
            "전쟁을 승리로 이끈 왕실 마법사. 하지만 그들의 힘을 두려워한 왕실은 그들을 모두 제거하였다."
        };
    }
}
