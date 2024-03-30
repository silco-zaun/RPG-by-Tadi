using System.Collections.Generic;
using Tadi.Datas.Combat;
using Tadi.Interface.CombatSkill;

namespace Tadi.Datas.CombatSkill
{
    public enum ActionType
    {
        None, Attack, Defense, AttackAndDefense
    }

    public enum EffectType
    {
        None, Buffs, Debuffs, BuffsAndDebuffs
    }

    public enum CureType
    {
        None, Healing, Recovery, Revival
    }

    public enum PowerType
    {
        None, Normal, Ultimate
    }

    public enum Target
    {
        Ally, Enemy, All
    }

    public enum Range
    {
        One, Self, FrontOne, RearOne, FrontAll, RearAll, StraightLine, RandomOne, RandomMultiple, All
    }

    public enum HitType
    {
        Always,
        UnitAccuracy,
        SkillAccuracy
    }

    public enum PercentageType
    {
        None, CurrentHP, MaxHP
    }

    public enum BossModifierType
    {
        None, Damage, Accuracy
    }

    public static class CombatSkillData
    {
        // Melee
        public static CombatSkill Smash { get; set; }
        public static CombatSkill GuardBreak { get; set; }
        public static CombatSkill CounterAttack { get; set; }
        public static CombatSkill FinalAttack { get; set; }
        // Rogue
        public static CombatSkill HawkWing { get; set; }
        public static CombatSkill PoisonAttack { get; set; }
        public static CombatSkill SpinSlash { get; set; }
        public static CombatSkill GrimReaper { get; set; }
        // Magic
        public static CombatSkill FireBall { get; set; }
        public static CombatSkill Barrier { get; set; }
        public static CombatSkill FlameStorm { get; set; }
        public static CombatSkill DesolateTouch { get; set; }

        public static void Init()
        {
            // Melee
            Smash = new CombatSkill();
            Smash.Name = "��Ÿ";
            Smash.Cooltime  = 2;
            Smash.Target  = Target.Enemy;
            Smash.Range  = Range.One;
            Smash.DamageType = DamageType.Physical;
            Smash.AttackType = AttackType.Melee;
            Smash.HitType = HitType.UnitAccuracy;
            Smash.PowerType = PowerType.Normal;
            Smash.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "������ 120%�� �������� ���մϴ�.",
                    Modifier  = 1.2f
                },
                new CombatSkillAbility()
                {
                    Description  = "������ 120%�� �������� ���մϴ�.",
                    Modifier  = 1.5f
                },
                new CombatSkillAbility()
                {
                    Description  = "������ 120%�� �������� ���մϴ�.",
                    Modifier  = 1.8f
                }
            };

            // --
            GuardBreak = new CombatSkill();
            GuardBreak.Name = "���� �극��ũ";
            GuardBreak.Cooltime = 1;
            GuardBreak.Target = Target.Enemy;
            GuardBreak.Range = Range.One;
            GuardBreak.DamageType = DamageType.Physical;
            GuardBreak.AttackType = AttackType.Melee;
            GuardBreak.HitType = HitType.Always;
            GuardBreak.PowerType = PowerType.Normal;
            GuardBreak.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "������� ������ 150%�� �������� ���մϴ�.",
                    Modifier  = 1.5f
                },
                new CombatSkillAbility()
                {
                    Description  = "������� ������ 200%�� �������� ���մϴ�.",
                    Modifier  = 2.0f
                },
                new CombatSkillAbility()
                {
                    Description  = "������� ������ 250%�� �������� ���մϴ�.",
                    Modifier  = 2.5f
                }
            };

            // -- ���� �ʿ�
            CounterAttack = new CombatSkill();
            CounterAttack.Name = "�ݰ�";
            CounterAttack.Cooltime = 2;
            CounterAttack.Target = Target.Ally;
            CounterAttack.Range = Range.Self;
            CounterAttack.DamageType = DamageType.Physical;
            CounterAttack.AttackType = AttackType.Melee;
            CounterAttack.HitType = HitType.UnitAccuracy;
            CounterAttack.PowerType = PowerType.Normal;
            CounterAttack.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "������ �޴� ���� �������� 50% ���� ��Ű�� 80%�� �������� ���մϴ�.",
                    Modifier  = 0.8f
                },
                new CombatSkillAbility()
                {
                    Description  = "������ �޴� ���� �������� 55% ���� ��Ű�� 85%�� �������� ���մϴ�.",
                    Modifier  = 0.85f
                },
                new CombatSkillAbility()
                {
                    Description  = "������ �޴� ���� �������� 60% ���� ��Ű�� 90%�� �������� ���մϴ�.",
                    Modifier  = 0.9f
                }
            };

            // --
            FinalAttack = new CombatSkill();
            FinalAttack.Name = "�ǰ���ô";
            FinalAttack.Cooltime = 4;
            FinalAttack.Target = Target.Enemy;
            FinalAttack.Range = Range.One;
            FinalAttack.DamageType = DamageType.Physical;
            FinalAttack.AttackType = AttackType.Melee;
            FinalAttack.HitType = HitType.SkillAccuracy;
            FinalAttack.PowerType = PowerType.Ultimate;
            FinalAttack.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "������ 80%�� Ȯ���� 250%�� �������� ���մϴ�.",
                    Modifier  = 2.5f,
                    Accuracy  = 0.8f
                },
                new CombatSkillAbility()
                {
                    Description  = "������ 85%�� Ȯ���� 300%�� �������� ���մϴ�.",
                    Modifier  = 3f,
                    Accuracy  = 0.85f
                },
                new CombatSkillAbility()
                {
                    Description  = "������ 90%�� Ȯ���� 350%�� �������� ���մϴ�.",
                    Modifier  = 3.5f,
                    Accuracy  = 0.9f
                }
            };

            // -- Ranged ���� �ʿ�
            HawkWing = new CombatSkill();
            HawkWing.Name = "���� ����";
            HawkWing.Cooltime = 2;
            HawkWing.Target = Target.Enemy;
            HawkWing.Range = Range.One;
            HawkWing.DamageType = DamageType.Physical;
            HawkWing.AttackType = AttackType.Ranged;
            HawkWing.HitType = HitType.SkillAccuracy;
            HawkWing.PowerType = PowerType.Normal;
            HawkWing.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "���� ������ �������� ���ϰ� 50%�� Ȯ���� ���� ���� ������ ȸ���մϴ�. �� ������ ȸ���� �� �����ϴ�.",
                    Accuracy  = 1f
                },
                new CombatSkillAbility()
                {
                    Description  = "���� ������ �������� ���ϰ� 70%�� Ȯ���� ���� ���� ������ ȸ���մϴ�. �� ������ ȸ���� �� �����ϴ�.",
                    Accuracy  = 1f
                },
                new CombatSkillAbility()
                {
                    Description  = "���� ������ �������� ���ϰ� 90%�� Ȯ���� ���� ���� ������ ȸ���մϴ�. �� ������ ȸ���� �� �����ϴ�.",
                    Accuracy  = 1f
                }
            };

            // -- ���� �ʿ�
            PoisonAttack = new CombatSkill();
            PoisonAttack.Name = "�� ����";
            PoisonAttack.Cooltime = 2;
            PoisonAttack.Target = Target.Enemy;
            PoisonAttack.Range = Range.One;
            PoisonAttack.DamageType = DamageType.Physical;
            PoisonAttack.AttackType = AttackType.Ranged;
            PoisonAttack.HitType = HitType.SkillAccuracy;
            PoisonAttack.PowerType = PowerType.Normal;
            PoisonAttack.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "������ 90%�� �������� ���ϰ� 3�ϵ��� �� ���¿� ������ �մϴ�. �� ���¿����� ���� ���� ü���� 10%(���� 3%)�� �������� �޽��ϴ�.",
                    Modifier = 0.9f
                },
                new CombatSkillAbility()
                {
                    Description  = "������ 110%�� �������� ���ϰ� 3�ϵ��� �� ���¿� ������ �մϴ�. �� ���¿����� ���� ���� ü���� 20%(���� 5%)�� �������� �޽��ϴ�.",
                    Modifier = 1.1f
                },
                new CombatSkillAbility()
                {
                    Description  = "������ 130%�� �������� ���ϰ� 3�ϵ��� �� ���¿� ������ �մϴ�. �� ���¿����� ���� ���� ü���� 30%(���� 7%)�� �������� �޽��ϴ�..",
                    Modifier = 1.3f
                }
            };

            // -- 
            SpinSlash = new CombatSkill();
            SpinSlash.Name = "ȸ�� ����";
            SpinSlash.Cooltime = 3;
            SpinSlash.Target = Target.Enemy;
            SpinSlash.Range = Range.All;
            SpinSlash.DamageType = DamageType.Physical;
            SpinSlash.AttackType = AttackType.Ranged;
            SpinSlash.HitType = HitType.UnitAccuracy;
            SpinSlash.PowerType = PowerType.Normal;
            SpinSlash.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "��� ������ 50%�� �������� ���մϴ�.",
                    Modifier = 0.5f
                },
                new CombatSkillAbility()
                {
                    Description  = "��� ������ 65%�� �������� ���մϴ�.",
                    Modifier = 0.65f
                },
                new CombatSkillAbility()
                {
                    Description  = "��� ������ 80%�� �������� ���մϴ�.",
                    Modifier = 0.8f
                }
            };

            // -- ���� �ʿ�
            GrimReaper = new CombatSkill();
            GrimReaper.Name = "�׸� ����";
            GrimReaper.Cooltime = 3;
            GrimReaper.Target = Target.Enemy;
            GrimReaper.Range = Range.One;
            GrimReaper.DamageType = DamageType.Physical;
            GrimReaper.AttackType = AttackType.Ranged;
            GrimReaper.HitType = HitType.SkillAccuracy;
            GrimReaper.PowerType = PowerType.Ultimate;
            GrimReaper.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "50%(���� : 5%)�� Ȯ���� ���� �ϻ��մϴ�.",
                    Accuracy = 0.5f,
                },
                new CombatSkillAbility()
                {
                    Description  = "70%(���� : 10%)�� Ȯ���� ���� �ϻ��մϴ�.",
                    Accuracy = 0.7f,
                },
                new CombatSkillAbility()
                {
                    Description  = "90%(���� : 15%)�� Ȯ���� ���� �ϻ��մϴ�.",
                    Accuracy = 0.9f,
                }
            };

            // -- Magic
            FireBall = new CombatSkill();
            FireBall.Name = "���̾� ��";
            FireBall.Cooltime = 2;
            FireBall.Target = Target.Enemy;
            FireBall.Range = Range.One;
            FireBall.DamageType = DamageType.Magic;
            FireBall.AttackType = AttackType.Ranged;
            FireBall.HitType = HitType.Always;
            FireBall.PowerType = PowerType.Normal;
            FireBall.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "������ 120%�� ȭ�� �������� ���մϴ�.",
                    Modifier = 1.2f,
                },
                new CombatSkillAbility()
                {
                    Description  = "������ 150%�� ȭ�� �������� ���մϴ�.",
                    Modifier = 1.5f,
                },
                new CombatSkillAbility()
                {
                    Description  = "������ 180%�� ȭ�� �������� ���մϴ�.",
                    Modifier = 1.8f,
                }
            };

            // -- ���� �ʿ�
            Barrier = new CombatSkill();
            Barrier.Name = "��ȣ��";
            Barrier.Cooltime = 2;
            Barrier.Target = Target.Ally;
            Barrier.Range = Range.Self;
            Barrier.DamageType = DamageType.None;
            Barrier.AttackType = AttackType.None;
            Barrier.HitType = HitType.Always;
            Barrier.PowerType = PowerType.Normal;
            Barrier.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "������ �޴� ���� ���ݰ� ���� ���� �������� 70% ���� ��ŵ�ϴ�.",
                    Modifier = 0.3f,
                },
                new CombatSkillAbility()
                {
                    Description  = "������ �޴� ���� ���ݰ� ���� ���� �������� 80% ���� ��ŵ�ϴ�.",
                    Modifier = 0.2f,
                },
                new CombatSkillAbility()
                {
                    Description  = "������ �޴� ���� ���ݰ� ���� ���� �������� 90% ���� ��ŵ�ϴ�.",
                    Modifier = 0.1f,
                }
            };

            // -- 
            FlameStorm = new CombatSkill();
            FlameStorm.Name = "ȭ�� ��ǳ";
            FlameStorm.Cooltime = 2;
            FlameStorm.Target = Target.Enemy;
            FlameStorm.Range = Range.All;
            FlameStorm.DamageType = DamageType.Magic;
            FlameStorm.AttackType = AttackType.Ranged;
            FlameStorm.HitType = HitType.Always;
            FlameStorm.PowerType = PowerType.Normal;
            FlameStorm.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "��� ������ 50%�� ȭ�� �������� ���մϴ�.",
                    Modifier = 0.5f,
                },
                new CombatSkillAbility()
                {
                    Description  = "�� ��ü���� 65%�� ȭ�� �������� ���մϴ�.",
                    Modifier = 0.65f,
                },
                new CombatSkillAbility()
                {
                    Description  = "�� ��ü���� 80%�� ȭ�� �������� ���մϴ�.",
                    Modifier = 0.8f,
                }
            };

            // -- 
            DesolateTouch = new CombatSkill();
            DesolateTouch.Name = "Ȳ���� �ձ�";
            DesolateTouch.Cooltime = 5;
            DesolateTouch.Target = Target.Enemy;
            DesolateTouch.Range = Range.All;
            DesolateTouch.DamageType = DamageType.Magic;
            DesolateTouch.AttackType = AttackType.Ranged;
            DesolateTouch.HitType = HitType.Always;
            DesolateTouch.PowerType = PowerType.Normal;
            DesolateTouch.Ability = new List<CombatSkillAbility>()
            {
                new CombatSkillAbility()
                {
                    Description  = "��� ������ ���� �������� 30%(���� 10%)�� �ش��ϴ� �������� ���մϴ�.",
                    Modifier = 0.7f,
                },
                new CombatSkillAbility()
                {
                    Description  = "��� ������ ���� �������� 50%(���� 20%)�� �ش��ϴ� �������� ���մϴ�.",
                    Modifier = 0.5f,
                },
                new CombatSkillAbility()
                {
                    Description  = "��� ������ ���� �������� 70%(���� 30%)�� �ش��ϴ� �������� ���մϴ�.",
                    Modifier = 0.3f,
                }
            };
        }
    }


    public class CombatSkill : ICombatSkill
    {
        public string Name { get; set; }
        public int Cooltime { get; set; }
        public Target Target { get; set; }
        public Range Range { get; set; }
        public DamageType DamageType { get; set; }
        public AttackType AttackType { get; set; }
        public HitType HitType { get; set; }
        public PowerType PowerType { get; set; }
        public List<CombatSkillAbility> Ability { get; set; }
    }

    public class CombatSkillAbility : ICombatSkillAbility
    {
        public string Description { get; set; }
        public float Modifier { get; set; }
        public float Accuracy { get; set; }

        public CombatSkillAbility()
        {
            Modifier = 1f;
            Accuracy = 1f;
        }
    }

    //public class RangedCombatSkill : ICombatSkill
    //{
    //    public string Name { get; set; }
    //    public int Cooltime { get; set; }
    //    public Target Target { get; set; }
    //    public Range Range { get; set; }
    //    public HitType HitType { get; set; }
    //    public PowerType PowerType { get; set; }
    //    public List<RangedCombatSkillAbility> Ability { get; set; }
    //}
    //
    //public class RangedCombatSkillAbility : ICombatSkillAbility, //IRangedCombatSkillAbility
    //{
    //    public string Description { get; set; }
    //    public float Modifier { get; set; }
    //    public float Accuracy { get; set; }
    //
    //    public RangedCombatSkillAbility()
    //    {
    //        Modifier = 1f;
    //        Accuracy = 1f;
    //    }
    //}
    //
    //public class MagicCombatSkill : ICombatSkill
    //{
    //    public string Name { get; set; }
    //    public int Cooltime { get; set; }
    //    public Target Target { get; set; }
    //    public Range Range { get; set; }
    //    public HitType HitType { get; set; }
    //    public PowerType PowerType { get; set; }
    //}
    //
    //public class CombatSkill
    //{
    //    public const int COMBAT_SKILL_MAX_LEVEL = 3;
    //
    //    public string Name { get; set; }
    //    public List<CombatSkillAbility> Ability { get; set; }
    //    public int Cooltime { get; set; }
    //    public Target Target { get; set; }
    //    public Range Range { get; set; }
    //    public PowerType PowerType { get; set; }
    //
    //    public PropertyType PropertyType { get; set; }
    //    public EffectType EffectType { get; set; }
    //    public CureType CureType { get; set; }
    //    public StateType StateType { get; set; }
    //}
    //
    //public class CombatSkillAbility
    //{
    //    public string Description { get; set; }
    //
    //    public float AttackModifier { get; set; }
    //
    //    public float DefenseModifier { get; set; }
    //
    //    public float FixedDamage { get; set; }
    //    public PercentageType PercentageType { get; set; }
    //
    //    public float PercentageAmount { get; set; }
    //    public float AccuracyModifier { get; set; }
    //    public float EvadeModifier { get; set; }
    //    public BossModifierType BossModifierType { get; set; }
    //    public float BossModifier { get; set; }
    //    public BulletType BulletType { get; set; }
    //}
}