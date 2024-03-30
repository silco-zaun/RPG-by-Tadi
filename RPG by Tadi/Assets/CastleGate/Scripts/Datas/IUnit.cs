

using Tadi.Datas.Unit;
using Tadi.Datas.Combat;
using Tadi.Datas.Weapon;
using UnityEngine;

namespace Tadi.Interface.unit
{
    public interface IUnit
    {
        public UnitType UnitType { get; set; }
        public string Description { get; set; }

        public UnitResData UnitRes { get; set; }
    }

    public interface IUnitStats
    {
        public DamageType DamageType { get; set; }
        public AttackType AttackType { get; set; }
        public BulletType BulletType { get; set; }

        //public float UnitLevel { get; set; }

        //public float MaxHP { get; set; }
        //public float CurHP { get; set; }
        //public float Attack { get; set; }
        //public float Defense { get; set; }
        //public float MagicAttack { get; set; }
        //public float MagicDefense { get; set; }
        //public float Speed { get; set; }

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
