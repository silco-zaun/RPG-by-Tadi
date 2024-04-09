using System.Collections;
using System.Collections.Generic;
using Tadi.Datas.Combat;
using Tadi.Datas.Skill;
using UnityEditor.Animations;
using UnityEngine;

namespace Tadi.Interface.CombatSkill
{
    public interface ICombatSkill
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
        public AnimatorController BulletAnim { get; set; }
    }

    public interface ICombatSkillAbility
    {
        public string Description { get; set; }
        public float Modifier { get; set; }
        public float Accuracy { get; set; }
    }

    public interface IFixedDamage
    {
        public float Modifier { get; set; }
    }

    public interface ICombatSkillState
    {
        public PropertyType PropertyType { get; set; }
        public EffectType EffectType { get; set; }
        public CureType CureType { get; set; }
        public StateType StateType { get; set; }
    }

}