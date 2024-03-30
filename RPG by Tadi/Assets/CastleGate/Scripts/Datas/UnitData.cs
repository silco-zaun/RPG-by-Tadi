
using UnityEditor.Animations;
using UnityEngine;
using Tadi.Datas.Combat;
using Tadi.Datas.Weapon;
using Tadi.Interface.unit;

namespace Tadi.Datas.Unit
{
    public enum UnitType
    {
        None, Knight, Rogue, Wizzard, Orc, SkeletonMage
    }

    public static class UnitData
    {
        public static UnitTypeData Knight { get; set; }
        public static UnitTypeData Rogue { get; set; }
        public static UnitTypeData Wizzard { get; set; }
        public static UnitTypeData Orc { get; set; }
        public static UnitTypeData SkeletonMage { get; set; }

        public static void Init()
        {
            Knight = new UnitTypeData();
            Knight.UnitType = UnitType.Knight;
            Knight.Description = "왕국을 수호하는 기사. 왕국의 체계적인 훈련을 받아 체력이 좋고 공격과 수비 모두에 능하다. 근접 단일 공격에 특화 돼있다.";
            Knight.UnitRes = new UnitResData(
                Managers.Ins.Res.UnitRes.KnightAnimator,
                Managers.Ins.Res.UnitRes.KnightBody,
                Managers.Ins.Res.UnitRes.HumanLeftHand,
                Managers.Ins.Res.UnitRes.HumanRightHand,
                Managers.Ins.Res.WeaponRes.WoodSword,
                Managers.Ins.Res.WeaponRes.WoodShild);
            Knight.DamageType = DamageType.Physical;
            Knight.AttackType = AttackType.Melee;
            Knight.BulletType = BulletType.None;
            Knight.BaseHP = 100;
            Knight.BaseAttack = 120;
            Knight.BaseDefense = 120;
            Knight.BaseMagicAttack = 80;
            Knight.BaseMagicDefense = 80;
            Knight.BaseSpeed = 100;
            Knight.IVHP = 20;
            Knight.IVAttack = 25;
            Knight.IVDefense = 25;
            Knight.IVMagicAttack = 15;
            Knight.IVMagicDefense = 15;
            Knight.IVSpeed = 20;

            Rogue = new UnitTypeData();
            Rogue.UnitType = UnitType.Rogue;
            Rogue.Description = "각종 불법적인 일을 하며 돈을 버는 도적. 왕실의 골칫거리. 움직임이 빠르고 은신, 잠입 및 도구 사용에 능하다.";
            Rogue.UnitRes = new UnitResData(
                Managers.Ins.Res.UnitRes.RogueAnimator,
                Managers.Ins.Res.UnitRes.RogueBody,
                Managers.Ins.Res.UnitRes.HumanLeftHand,
                Managers.Ins.Res.UnitRes.HumanRightHand,
                Managers.Ins.Res.WeaponRes.WoodArrow,
                Managers.Ins.Res.WeaponRes.WoodBow);
            Rogue.DamageType = DamageType.Physical;
            Rogue.AttackType = AttackType.Ranged;
            Rogue.BulletType = BulletType.MagicBolt;
            Rogue.BaseHP = 90;
            Rogue.BaseAttack = 130;
            Rogue.BaseDefense = 90;
            Rogue.BaseMagicAttack = 80;
            Rogue.BaseMagicDefense = 80;
            Rogue.BaseSpeed = 130;
            Rogue.IVHP = 20;
            Rogue.IVAttack = 25;
            Rogue.IVDefense = 20;
            Rogue.IVMagicAttack = 15;
            Rogue.IVMagicDefense = 15;
            Rogue.IVSpeed = 25;

            Wizzard = new UnitTypeData();
            Wizzard.UnitType = UnitType.Wizzard;
            Wizzard.Description = "왕국을 수호하는 마법사. 모든것을 파괴하는 힘을 가졌지만 방어력이 약하다.";
            Wizzard.UnitRes = new UnitResData(
                Managers.Ins.Res.UnitRes.WizzardAnimator,
                Managers.Ins.Res.UnitRes.WizzardBody,
                Managers.Ins.Res.UnitRes.HumanLeftHand,
                Managers.Ins.Res.UnitRes.HumanRightHand,
                Managers.Ins.Res.WeaponRes.WoodGrimoire,
                Managers.Ins.Res.WeaponRes.WoodWand);
            Wizzard.DamageType = DamageType.Magic;
            Wizzard.AttackType = AttackType.Ranged;
            Wizzard.BulletType = BulletType.MagicBolt;
            Wizzard.BaseHP = 100;
            Wizzard.BaseAttack = 80;
            Wizzard.BaseDefense = 80;
            Wizzard.BaseMagicAttack = 120;
            Wizzard.BaseMagicDefense = 120;
            Wizzard.BaseSpeed = 100;
            Wizzard.IVHP = 20;
            Wizzard.IVAttack = 15;
            Wizzard.IVDefense = 15;
            Wizzard.IVMagicAttack = 25;
            Wizzard.IVMagicDefense = 25;
            Wizzard.IVSpeed = 20;

            Orc = new UnitTypeData();
            Orc.UnitType = UnitType.Orc;
            Orc.Description = "인간과 오랜기간 전쟁중인 오크. 인간이 자신들의 영역을 침범한다고 주장한다.";
            Orc.UnitRes = new UnitResData(
                Managers.Ins.Res.UnitRes.OrcAnimator,
                Managers.Ins.Res.UnitRes.OrcBody,
                Managers.Ins.Res.UnitRes.OrcLeftHand,
                Managers.Ins.Res.UnitRes.OrcRightHand,
                Managers.Ins.Res.WeaponRes.WoodSword,
                Managers.Ins.Res.WeaponRes.WoodShild);
            Orc.DamageType = DamageType.Physical;
            Orc.AttackType = AttackType.Melee;
            Orc.BulletType = BulletType.None;
            Orc.BaseHP = 100;
            Orc.BaseAttack = 120;
            Orc.BaseDefense = 120;
            Orc.BaseMagicAttack = 80;
            Orc.BaseMagicDefense = 80;
            Orc.BaseSpeed = 100;
            Orc.IVHP = 20;
            Orc.IVAttack = 25;
            Orc.IVDefense = 25;
            Orc.IVMagicAttack = 15;
            Orc.IVMagicDefense = 15;
            Orc.IVSpeed = 20;

            SkeletonMage = new UnitTypeData();
            SkeletonMage.UnitType = UnitType.SkeletonMage;
            SkeletonMage.Description = "살아있을때 전쟁을 승리로 이끌었던 고위 마법사. 하지만 그들의 힘을 두려워한 왕실은 그들을 모두 제거했다.";
            SkeletonMage.UnitRes = new UnitResData(
                Managers.Ins.Res.UnitRes.SkeletonMageAnimator,
                Managers.Ins.Res.UnitRes.SkeletonMageBody,
                Managers.Ins.Res.UnitRes.SkeletonLeftHand,
                Managers.Ins.Res.UnitRes.SkeletonRightHand,
                Managers.Ins.Res.WeaponRes.BoneGrimoire,
                Managers.Ins.Res.WeaponRes.BoneWand);
            SkeletonMage.DamageType = DamageType.Magic;
            SkeletonMage.AttackType = AttackType.Ranged;
            SkeletonMage.BulletType = BulletType.DarkBolt;
            SkeletonMage.BaseHP = 100;
            SkeletonMage.BaseAttack = 80;
            SkeletonMage.BaseDefense = 80;
            SkeletonMage.BaseMagicAttack = 120;
            SkeletonMage.BaseMagicDefense = 120;
            SkeletonMage.BaseSpeed = 100;
            SkeletonMage.IVHP = 20;
            SkeletonMage.IVAttack = 15;
            SkeletonMage.IVDefense = 15;
            SkeletonMage.IVMagicAttack = 25;
            SkeletonMage.IVMagicDefense = 25;
            SkeletonMage.IVSpeed = 20;
        }
    }

    public class UnitResData
    {
        public AnimatorController Animator { get; private set; }
        public Sprite Body { get; private set; }
        public Sprite LeftHand { get; private set; }
        public Sprite RightHand { get; private set; }
        public Sprite LeftWeapon { get; private set; }
        public Sprite RightWeapon { get; private set; }

        public UnitResData(AnimatorController animator, Sprite body, Sprite leftHand, Sprite leftWeapon, Sprite rightHand, Sprite rightWeapon)
        {
            Animator = animator;
            Body = body;
            LeftHand = leftHand;
            RightHand = rightHand;
            LeftWeapon = leftWeapon;
            RightWeapon = rightWeapon;
        }
    }

    public class UnitTypeData : IUnit, IUnitStats
    {
        // Unit
        public UnitType UnitType { get; set; }
        public string Description { get; set; }

        public UnitResData UnitRes { get; set; }

        public DamageType DamageType { get; set; }
        public AttackType AttackType { get; set; }
        public BulletType BulletType { get; set; }

        // Base stat values
        // Average 100 Total 600
        public float BaseHP { get; set; }
        public float BaseAttack { get; set; }
        public float BaseDefense { get; set; }
        public float BaseMagicAttack { get; set; }
        public float BaseMagicDefense { get; set; }
        public float BaseSpeed { get; set; }

        // Individual values
        // Average 20 Total 120
        public float IVHP { get; set; }
        public float IVAttack { get; set; }
        public float IVDefense { get; set; }
        public float IVMagicAttack { get; set; }
        public float IVMagicDefense { get; set; }
        public float IVSpeed { get; set; }
    }
}
