using UnityEngine;
using Tadi.Datas.Combat;
using Tadi.Datas.Skill;
using Tadi.Interface.unit;
using System.Collections.Generic;

namespace Tadi.Datas.Unit
{
    public enum UnitType
    {
        None, 
        Knight, Rogue, Wizzard, // Player
        Orc, SkeletonMage, // Enemy
        LizardMan, TurtleKing // NPC
    }

    public static class UnitData
    {
        public static UnitTypeData Knight { get; private set; }
        public static UnitTypeData Rogue { get; private set; }
        public static UnitTypeData Wizzard { get; private set; }
        public static UnitTypeData Orc { get; private set; }
        public static UnitTypeData SkeletonMage { get; private set; }
        public static UnitTypeData LizardMan { get; private set; }
        public static UnitTypeData TurtleKing { get; private set; }

        static UnitData()
        {
            Init();
        }

        public static void Init()
        {
            Knight = new UnitTypeData();
            Knight.UnitType = UnitType.Knight;
            Knight.Name = "기사";
            Knight.Description = "왕국을 수호하는 기사. 왕국의 체계적인 훈련을 받아 체력이 좋고 공격과 수비 모두에 능하다. 근접 단일 공격에 특화 돼있다.";
            Knight.UnitAnimRes = new UnitAnimRes(
                Managers.Ins.Res.UnitRes.KnightAnimator,
                Managers.Ins.Res.WeaponRes.MagicBolt,
                Managers.Ins.Res.UnitRes.KnightBody,
                Managers.Ins.Res.UnitRes.HumanLeftHand,
                Managers.Ins.Res.UnitRes.HumanRightHand,
                Managers.Ins.Res.WeaponRes.WoodSword,
                Managers.Ins.Res.WeaponRes.WoodShild);
            Knight.CombatSkills = new List<UnitCombatSkill>()
            {
                new UnitCombatSkill(CombatSkillData.Smash, 1, 1),
                new UnitCombatSkill(CombatSkillData.GuardBreak, 1, 5),
                new UnitCombatSkill(CombatSkillData.CounterAttack, 1, 10),
                new UnitCombatSkill(CombatSkillData.FinalAttack, 1, 20)
            };
            Knight.DamageType = DamageType.Physical;
            Knight.AttackType = AttackType.Melee;
            Knight.BaseHP = 100;
            Knight.BasePhysicalAttack = 120;
            Knight.BasePhysicalDefense = 120;
            Knight.BaseMagicAttack = 80;
            Knight.BaseMagicDefense = 80;
            Knight.BaseSpeed = 100;
            Knight.IVHP = 20;
            Knight.IVPhysicalAttack = 25;
            Knight.IVPhysicalDefense = 25;
            Knight.IVMagicAttack = 15;
            Knight.IVMagicDefense = 15;
            Knight.IVSpeed = 20;

            Rogue = new UnitTypeData();
            Rogue.UnitType = UnitType.Rogue;
            Rogue.Name = "도적";
            Rogue.Description = "각종 불법적인 일을 하며 돈을 버는 도적. 왕실의 골칫거리. 움직임이 빠르고 은신, 잠입 및 도구 사용에 능하다.";
            Rogue.UnitAnimRes = new UnitAnimRes(
                Managers.Ins.Res.UnitRes.RogueAnimator,
                Managers.Ins.Res.WeaponRes.MagicBolt,
                Managers.Ins.Res.UnitRes.RogueBody,
                Managers.Ins.Res.UnitRes.HumanLeftHand,
                Managers.Ins.Res.UnitRes.HumanRightHand,
                Managers.Ins.Res.WeaponRes.WoodArrow,
                Managers.Ins.Res.WeaponRes.WoodBow);
            Rogue.CombatSkills = new List<UnitCombatSkill>()
            {
                new UnitCombatSkill(CombatSkillData.HawkWing, 1, 1),
                new UnitCombatSkill(CombatSkillData.PoisonAttack, 1, 5),
                new UnitCombatSkill(CombatSkillData.SpinSlash, 1, 10),
                new UnitCombatSkill(CombatSkillData.GrimReaper, 1, 20)
            };
            Rogue.DamageType = DamageType.Physical;
            Rogue.AttackType = AttackType.Ranged;
            Rogue.BaseHP = 90;
            Rogue.BasePhysicalAttack = 130;
            Rogue.BasePhysicalDefense = 90;
            Rogue.BaseMagicAttack = 80;
            Rogue.BaseMagicDefense = 80;
            Rogue.BaseSpeed = 130;
            Rogue.IVHP = 20;
            Rogue.IVPhysicalAttack = 25;
            Rogue.IVPhysicalDefense = 20;
            Rogue.IVMagicAttack = 15;
            Rogue.IVMagicDefense = 15;
            Rogue.IVSpeed = 25;

            Wizzard = new UnitTypeData();
            Wizzard.UnitType = UnitType.Wizzard;
            Wizzard.Name = "마법사";
            Wizzard.Description = "왕국을 수호하는 마법사. 모든것을 파괴하는 힘을 가졌지만 방어력이 약하다.";
            Wizzard.UnitAnimRes = new UnitAnimRes(
                Managers.Ins.Res.UnitRes.WizzardAnimator,
                Managers.Ins.Res.WeaponRes.MagicBolt,
                Managers.Ins.Res.UnitRes.WizzardBody,
                Managers.Ins.Res.UnitRes.HumanLeftHand,
                Managers.Ins.Res.UnitRes.HumanRightHand,
                Managers.Ins.Res.WeaponRes.WoodGrimoire,
                Managers.Ins.Res.WeaponRes.WoodWand);
            Wizzard.CombatSkills = new List<UnitCombatSkill>()
            {
                new UnitCombatSkill(CombatSkillData.FireBall, 1, 1),
                new UnitCombatSkill(CombatSkillData.Barrier, 1, 5),
                new UnitCombatSkill(CombatSkillData.FlameStorm, 1, 10),
                new UnitCombatSkill(CombatSkillData.DesolateTouch, 1, 20)
            };
            Wizzard.DamageType = DamageType.Magic;
            Wizzard.AttackType = AttackType.Ranged;
            Wizzard.BaseHP = 100;
            Wizzard.BasePhysicalAttack = 80;
            Wizzard.BasePhysicalDefense = 80;
            Wizzard.BaseMagicAttack = 120;
            Wizzard.BaseMagicDefense = 120;
            Wizzard.BaseSpeed = 100;
            Wizzard.IVHP = 20;
            Wizzard.IVPhysicalAttack = 15;
            Wizzard.IVPhysicalDefense = 15;
            Wizzard.IVMagicAttack = 25;
            Wizzard.IVMagicDefense = 25;
            Wizzard.IVSpeed = 20;

            Orc = new UnitTypeData();
            Orc.UnitType = UnitType.Orc;
            Orc.Name = "오크";
            Orc.Description = "인간과 오랜기간 전쟁중인 오크. 인간이 자신들의 영역을 침범한다고 주장한다.";
            Orc.UnitAnimRes = new UnitAnimRes(
                Managers.Ins.Res.UnitRes.OrcAnimator,
                Managers.Ins.Res.WeaponRes.MagicBolt,
                Managers.Ins.Res.UnitRes.OrcBody,
                Managers.Ins.Res.UnitRes.OrcLeftHand,
                Managers.Ins.Res.UnitRes.OrcRightHand,
                Managers.Ins.Res.WeaponRes.WoodSword,
                Managers.Ins.Res.WeaponRes.WoodShild);
            Orc.CombatSkills = new List<UnitCombatSkill>()
            {
                new UnitCombatSkill(CombatSkillData.Smash, 1, 1),
                new UnitCombatSkill(CombatSkillData.GuardBreak, 1, 5),
                new UnitCombatSkill(CombatSkillData.CounterAttack, 1, 10),
                new UnitCombatSkill(CombatSkillData.FinalAttack, 1, 20)
            };
            Orc.DamageType = DamageType.Physical;
            Orc.AttackType = AttackType.Melee;
            Orc.BaseHP = 10;
            Orc.BasePhysicalAttack = 120;
            Orc.BasePhysicalDefense = 120;
            Orc.BaseMagicAttack = 80;
            Orc.BaseMagicDefense = 80;
            Orc.BaseSpeed = 100;
            Orc.IVHP = 20;
            Orc.IVPhysicalAttack = 25;
            Orc.IVPhysicalDefense = 25;
            Orc.IVMagicAttack = 15;
            Orc.IVMagicDefense = 15;
            Orc.IVSpeed = 20;

            SkeletonMage = new UnitTypeData();
            SkeletonMage.UnitType = UnitType.SkeletonMage;
            SkeletonMage.Name = "해골 마법사";
            SkeletonMage.Description = "살아있을때 전쟁을 승리로 이끌었던 고위 마법사. 하지만 그들의 힘을 두려워한 왕실은 그들을 모두 제거했다.";
            SkeletonMage.UnitAnimRes = new UnitAnimRes(
                Managers.Ins.Res.UnitRes.SkeletonMageAnimator,
                Managers.Ins.Res.WeaponRes.DarkBolt,
                Managers.Ins.Res.UnitRes.SkeletonMageBody,
                Managers.Ins.Res.UnitRes.SkeletonLeftHand,
                Managers.Ins.Res.UnitRes.SkeletonRightHand,
                Managers.Ins.Res.WeaponRes.BoneGrimoire,
                Managers.Ins.Res.WeaponRes.BoneWand);
            SkeletonMage.CombatSkills = new List<UnitCombatSkill>()
            {
                new UnitCombatSkill(CombatSkillData.FireBall, 1, 1),
                new UnitCombatSkill(CombatSkillData.Barrier, 1, 5),
                new UnitCombatSkill(CombatSkillData.FlameStorm, 1, 10),
                new UnitCombatSkill(CombatSkillData.DesolateTouch, 1, 20)
            };
            SkeletonMage.DamageType = DamageType.Magic;
            SkeletonMage.AttackType = AttackType.Ranged;
            SkeletonMage.BaseHP = 100;
            SkeletonMage.BasePhysicalAttack = 80;
            SkeletonMage.BasePhysicalDefense = 80;
            SkeletonMage.BaseMagicAttack = 120;
            SkeletonMage.BaseMagicDefense = 120;
            SkeletonMage.BaseSpeed = 100;
            SkeletonMage.IVHP = 20;
            SkeletonMage.IVPhysicalAttack = 15;
            SkeletonMage.IVPhysicalDefense = 15;
            SkeletonMage.IVMagicAttack = 25;
            SkeletonMage.IVMagicDefense = 25;
            SkeletonMage.IVSpeed = 20;

            LizardMan = new UnitTypeData();
            LizardMan.UnitType = UnitType.LizardMan;
            LizardMan.Name = "리자드맨";
            LizardMan.Description = "서식지가 인간에 의해 파괴되어 동족들 모두 죽을 위기해 처해 있었으나 용왕의 도움을 받아 구사일생한 도마뱀. 용왕에게 충성을 맹세했고 능력을 인정받아 용궁 정예군 대장으로 활동하고 있다.";
            LizardMan.UnitAnimRes = new UnitAnimRes(
                Managers.Ins.Res.UnitRes.LizardManAnimator,
                Managers.Ins.Res.WeaponRes.MagicBolt,
                Managers.Ins.Res.UnitRes.LizardManBody,
                null, null, null, null);
            LizardMan.CombatSkills = new List<UnitCombatSkill>()
            {
                new UnitCombatSkill(CombatSkillData.FireBall, 1, 1),
                new UnitCombatSkill(CombatSkillData.Barrier, 1, 5),
                new UnitCombatSkill(CombatSkillData.FlameStorm, 1, 10),
                new UnitCombatSkill(CombatSkillData.DesolateTouch, 1, 20)
            };
            LizardMan.DamageType = DamageType.Magic;
            LizardMan.AttackType = AttackType.Ranged;
            LizardMan.BaseHP = 100;
            LizardMan.BasePhysicalAttack = 80;
            LizardMan.BasePhysicalDefense = 80;
            LizardMan.BaseMagicAttack = 120;
            LizardMan.BaseMagicDefense = 120;
            LizardMan.BaseSpeed = 100;
            LizardMan.IVHP = 20;
            LizardMan.IVPhysicalAttack = 15;
            LizardMan.IVPhysicalDefense = 15;
            LizardMan.IVMagicAttack = 25;
            LizardMan.IVMagicDefense = 25;
            LizardMan.IVSpeed = 20;


            TurtleKing = new UnitTypeData();
            TurtleKing.UnitType = UnitType.TurtleKing;
            TurtleKing.Name = "용왕";
            TurtleKing.Description = "토끼의 간을 용왕에게 바친 공로를 인정받아 용왕의 자리를 물려받은 거북이. 젊은시절 용궁 최고의 장군 이었다.";
            TurtleKing.UnitAnimRes = new UnitAnimRes(
                Managers.Ins.Res.UnitRes.TurtleKingAnimator,
                Managers.Ins.Res.WeaponRes.MagicBolt,
                Managers.Ins.Res.UnitRes.TurtleKingBody,
                null, null, null, null);
            TurtleKing.CombatSkills = new List<UnitCombatSkill>()
            {
                new UnitCombatSkill(CombatSkillData.FireBall, 1, 1),
                new UnitCombatSkill(CombatSkillData.Barrier, 1, 5),
                new UnitCombatSkill(CombatSkillData.FlameStorm, 1, 10),
                new UnitCombatSkill(CombatSkillData.DesolateTouch, 1, 20)
            };
            TurtleKing.DamageType = DamageType.Magic;
            TurtleKing.AttackType = AttackType.Ranged;
            TurtleKing.BaseHP = 100;
            TurtleKing.BasePhysicalAttack = 80;
            TurtleKing.BasePhysicalDefense = 80;
            TurtleKing.BaseMagicAttack = 120;
            TurtleKing.BaseMagicDefense = 120;
            TurtleKing.BaseSpeed = 100;
            TurtleKing.IVHP = 20;
            TurtleKing.IVPhysicalAttack = 15;
            TurtleKing.IVPhysicalDefense = 15;
            TurtleKing.IVMagicAttack = 25;
            TurtleKing.IVMagicDefense = 25;
            TurtleKing.IVSpeed = 20;
        }
    }

    public class UnitAnimRes
    {
        public RuntimeAnimatorController Animator { get; private set; }
        public RuntimeAnimatorController BulletAnimator { get; private set; }
        public Sprite Body { get; private set; }
        public Sprite LeftHand { get; private set; }
        public Sprite RightHand { get; private set; }
        public Sprite LeftWeapon { get; private set; }
        public Sprite RightWeapon { get; private set; }

        public UnitAnimRes(RuntimeAnimatorController animator, RuntimeAnimatorController bulletAnimator, Sprite body, Sprite leftHand, Sprite rightHand, Sprite leftWeapon, Sprite rightWeapon)
        {
            Animator = animator;
            BulletAnimator = bulletAnimator;
            Body = body;
            LeftHand = leftHand;
            RightHand = rightHand;
            LeftWeapon = leftWeapon;
            RightWeapon = rightWeapon;
        }
    }

    public class UnitCombatSkill
    {
        public CombatSkill Skill { get; private set; }
        public int SkillLevel { get; private set; }
        public int LearnLevel { get; private set; }

        public DamageType DamageType { get { return Skill.DamageType; } }
        public AttackType AttackType { get { return Skill.AttackType; } }
        public string Description { get { return Skill.Ability[SkillLevel].Description; } }
        public float Multiplier { get { return Skill.Ability[SkillLevel].Modifier; } }
        public RuntimeAnimatorController BulletAnim { get { return Skill.BulletAnim; } }

        public UnitCombatSkill(CombatSkill skill, int skillLevel, int learnLevel)
        {
            Skill = skill;
            SkillLevel = skillLevel;
            LearnLevel = learnLevel;
        }
    }

    public class UnitTypeData : IUnit, IUnitStats
    {
        // Unit
        public UnitType UnitType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public UnitAnimRes UnitAnimRes { get; set; }
        public List<UnitCombatSkill> CombatSkills { get; set; }
        public DamageType DamageType { get; set; }
        public AttackType AttackType { get; set; }

        // Base stat values
        // Average 100 Total 600
        public float BaseHP { get; set; }
        public float BasePhysicalAttack { get; set; }
        public float BasePhysicalDefense { get; set; }
        public float BaseMagicAttack { get; set; }
        public float BaseMagicDefense { get; set; }
        public float BaseSpeed { get; set; }

        // Individual values
        // Average 20 Total 120
        public float IVHP { get; set; }
        public float IVPhysicalAttack { get; set; }
        public float IVPhysicalDefense { get; set; }
        public float IVMagicAttack { get; set; }
        public float IVMagicDefense { get; set; }
        public float IVSpeed { get; set; }
    }
}
