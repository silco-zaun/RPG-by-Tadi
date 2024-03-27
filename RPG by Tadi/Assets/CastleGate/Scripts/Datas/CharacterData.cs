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
            "", "���", "����", "������", "��ũ", "���̷��� ������"
        };

        public static List<string> Desc = new List<string>()
        {
            "",
            "�ձ��� ��ȣ�ϴ� ���. ü������ �Ʒ����� ���ݰ� ���� ��ο� ���ϴ�. ���� ���� ���ݿ� Ưȭ ���ִ�.",
            "���� �ҹ����� ���� �ϸ� ���� ���� ����. �ս��� ��ĩ�Ÿ� �̴�. Ư���� ���� �����Ӱ� ����, ���� �ɷ����� ��ǥ�� �޼��Ѵ�.",
            "�ս��� ��ȣ�ϴ� ������. ������ �ı��ϴ� ���� �������� ������ ���ϴ�.",
            "�ΰ��� �����Ⱓ �������� ��ũ��. �ΰ��� �ڽŵ��� ������ ħ���Ѵٰ� �����Ѵ�.",
            "������ �¸��� �̲� �ս� ������. ������ �׵��� ���� �η����� �ս��� �׵��� ��� �����Ͽ���."
        };
    }
}