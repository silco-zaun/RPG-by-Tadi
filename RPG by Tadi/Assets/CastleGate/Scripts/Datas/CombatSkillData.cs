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
            Smash.Name = "강타";
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
                    Description  = "적에게 120%의 데미지를 가합니다.",
                    Modifier  = 1.2f
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 120%의 데미지를 가합니다.",
                    Modifier  = 1.5f
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 120%의 데미지를 가합니다.",
                    Modifier  = 1.8f
                }
            };

            // --
            GuardBreak = new CombatSkill();
            GuardBreak.Name = "가드 브레이크";
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
                    Description  = "방어중인 적에게 150%의 데미지를 가합니다.",
                    Modifier  = 1.5f
                },
                new CombatSkillAbility()
                {
                    Description  = "방어중인 적에게 200%의 데미지를 가합니다.",
                    Modifier  = 2.0f
                },
                new CombatSkillAbility()
                {
                    Description  = "방어중인 적에게 250%의 데미지를 가합니다.",
                    Modifier  = 2.5f
                }
            };

            // -- 수정 필요
            CounterAttack = new CombatSkill();
            CounterAttack.Name = "반격";
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
                    Description  = "적에게 받는 물리 데미지를 50% 감소 시키고 80%의 데미지를 가합니다.",
                    Modifier  = 0.8f
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 받는 물리 데미지를 55% 감소 시키고 85%의 데미지를 가합니다.",
                    Modifier  = 0.85f
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 받는 물리 데미지를 60% 감소 시키고 90%의 데미지를 가합니다.",
                    Modifier  = 0.9f
                }
            };

            // --
            FinalAttack = new CombatSkill();
            FinalAttack.Name = "건곤일척";
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
                    Description  = "적에게 80%의 확율로 250%의 데미지를 가합니다.",
                    Modifier  = 2.5f,
                    Accuracy  = 0.8f
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 85%의 확율로 300%의 데미지를 가합니다.",
                    Modifier  = 3f,
                    Accuracy  = 0.85f
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 90%의 확율로 350%의 데미지를 가합니다.",
                    Modifier  = 3.5f,
                    Accuracy  = 0.9f
                }
            };

            // -- Ranged 수정 필요
            HawkWing = new CombatSkill();
            HawkWing.Name = "매의 날개";
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
                    Description  = "단일 적에게 데미지를 가하고 50%의 확율로 적의 물리 공격을 회피합니다. 이 공격은 회피할 수 없습니다.",
                    Accuracy  = 1f
                },
                new CombatSkillAbility()
                {
                    Description  = "단일 적에게 데미지를 가하고 70%의 확율로 적의 물리 공격을 회피합니다. 이 공격은 회피할 수 없습니다.",
                    Accuracy  = 1f
                },
                new CombatSkillAbility()
                {
                    Description  = "단일 적에게 데미지를 가하고 90%의 확율로 적의 물리 공격을 회피합니다. 이 공격은 회피할 수 없습니다.",
                    Accuracy  = 1f
                }
            };

            // -- 수정 필요
            PoisonAttack = new CombatSkill();
            PoisonAttack.Name = "독 공격";
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
                    Description  = "적에게 90%의 데미지를 가하고 3턴동안 독 상태에 빠지게 합니다. 독 상태에서는 매턴 남은 체력의 10%(보스 3%)의 데미지를 받습니다.",
                    Modifier = 0.9f
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 110%의 데미지를 가하고 3턴동안 독 상태에 빠지게 합니다. 독 상태에서는 매턴 남은 체력의 20%(보스 5%)의 데미지를 받습니다.",
                    Modifier = 1.1f
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 130%의 데미지를 가하고 3턴동안 독 상태에 빠지게 합니다. 독 상태에서는 매턴 남은 체력의 30%(보스 7%)의 데미지를 받습니다..",
                    Modifier = 1.3f
                }
            };

            // -- 
            SpinSlash = new CombatSkill();
            SpinSlash.Name = "회전 베기";
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
                    Description  = "모든 적에게 50%의 데미지를 가합니다.",
                    Modifier = 0.5f
                },
                new CombatSkillAbility()
                {
                    Description  = "모든 적에게 65%의 데미지를 가합니다.",
                    Modifier = 0.65f
                },
                new CombatSkillAbility()
                {
                    Description  = "모든 적에게 80%의 데미지를 가합니다.",
                    Modifier = 0.8f
                }
            };

            // -- 수정 필요
            GrimReaper = new CombatSkill();
            GrimReaper.Name = "그림 리퍼";
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
                    Description  = "50%(보스 : 5%)의 확율로 적을 암살합니다.",
                    Accuracy = 0.5f,
                },
                new CombatSkillAbility()
                {
                    Description  = "70%(보스 : 10%)의 확율로 적을 암살합니다.",
                    Accuracy = 0.7f,
                },
                new CombatSkillAbility()
                {
                    Description  = "90%(보스 : 15%)의 확율로 적을 암살합니다.",
                    Accuracy = 0.9f,
                }
            };

            // -- Magic
            FireBall = new CombatSkill();
            FireBall.Name = "파이어 볼";
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
                    Description  = "적에게 120%의 화염 데미지를 가합니다.",
                    Modifier = 1.2f,
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 150%의 화염 데미지를 가합니다.",
                    Modifier = 1.5f,
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 180%의 화염 데미지를 가합니다.",
                    Modifier = 1.8f,
                }
            };

            // -- 수정 필요
            Barrier = new CombatSkill();
            Barrier.Name = "보호막";
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
                    Description  = "적에게 받는 물리 공격과 마법 공격 데미지를 70% 감소 시킵니다.",
                    Modifier = 0.3f,
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 받는 물리 공격과 마법 공격 데미지를 80% 감소 시킵니다.",
                    Modifier = 0.2f,
                },
                new CombatSkillAbility()
                {
                    Description  = "적에게 받는 물리 공격과 마법 공격 데미지를 90% 감소 시킵니다.",
                    Modifier = 0.1f,
                }
            };

            // -- 
            FlameStorm = new CombatSkill();
            FlameStorm.Name = "화염 폭풍";
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
                    Description  = "모든 적에게 50%의 화염 데미지를 가합니다.",
                    Modifier = 0.5f,
                },
                new CombatSkillAbility()
                {
                    Description  = "적 전체에게 65%의 화염 데미지를 가합니다.",
                    Modifier = 0.65f,
                },
                new CombatSkillAbility()
                {
                    Description  = "적 전체에게 80%의 화염 데미지를 가합니다.",
                    Modifier = 0.8f,
                }
            };

            // -- 
            DesolateTouch = new CombatSkill();
            DesolateTouch.Name = "황폐의 손길";
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
                    Description  = "모든 적에게 남은 생명력의 30%(보스 10%)에 해당하는 데미지를 가합니다.",
                    Modifier = 0.7f,
                },
                new CombatSkillAbility()
                {
                    Description  = "모든 적에게 남은 생명력의 50%(보스 20%)에 해당하는 데미지를 가합니다.",
                    Modifier = 0.5f,
                },
                new CombatSkillAbility()
                {
                    Description  = "모든 적에게 남은 생명력의 70%(보스 30%)에 해당하는 데미지를 가합니다.",
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
