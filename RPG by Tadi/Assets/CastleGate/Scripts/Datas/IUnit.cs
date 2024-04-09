

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

        public UnitAnimRes UnitAnimRes { get; set; }
    }

    public interface IUnitStats
    {
        public DamageType DamageType { get; set; }
        public AttackType AttackType { get; set; }

        //public float UnitLevel { get; set; }

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
