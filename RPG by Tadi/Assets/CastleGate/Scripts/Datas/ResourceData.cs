using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Tadi.Datas.Res
{
    [System.Serializable]
    public class UnitRes
    {
        public const string KNIGHT_ATTACK_CLIP_NAME = "LeftAttack";
        public const string ROGUE_ATTACK_CLIP_NAME = "LeftAttack";
        public const string WIZZARD_ATTACK_CLIP_NAME = "RightAttack";
        public const string ORC_ATTACK_CLIP_NAME = "LeftAttack";
        public const string SKELETON_MAGE_ATTACK_CLIP_NAME = "RightAttack";
        public const string ATTACK_CLIP_KEYWORD = "Attack";

        [SerializeField] private AnimatorController knightAnimator;
        [SerializeField] private AnimatorController rogueAnimator;
        [SerializeField] private AnimatorController wizzardAnimator;
        [SerializeField] private AnimatorController orcAnimator;
        [SerializeField] private AnimatorController skeletonMageAnimator;
        // Body
        [SerializeField] private Sprite knightBody;
        [SerializeField] private Sprite rogueBody;
        [SerializeField] private Sprite wizzardBody;
        [SerializeField] private Sprite orcBody;
        [SerializeField] private Sprite skeletonMageBody;
        // Hand
        [SerializeField] private Sprite humanLeftHand;
        [SerializeField] private Sprite humanRightHand;
        [SerializeField] private Sprite orcLeftHand;
        [SerializeField] private Sprite orcRightHand;
        [SerializeField] private Sprite skeletonLeftHand;
        [SerializeField] private Sprite skeletonRightHand;

        public AnimatorController KnightAnimator { get { return knightAnimator; } }
        public AnimatorController RogueAnimator { get { return rogueAnimator; } }
        public AnimatorController WizzardAnimator { get { return wizzardAnimator; } }
        public AnimatorController OrcAnimator { get { return orcAnimator; } }
        public AnimatorController SkeletonMageAnimator { get { return skeletonMageAnimator; } }
        // Body
        public Sprite KnightBody { get { return knightBody; } }
        public Sprite RogueBody { get { return rogueBody; } }
        public Sprite WizzardBody { get { return wizzardBody; } }
        public Sprite OrcBody { get { return orcBody; } }
        public Sprite SkeletonMageBody { get { return skeletonMageBody; } }
        // Hand
        public Sprite HumanLeftHand { get { return humanLeftHand; } }
        public Sprite HumanRightHand { get { return humanRightHand; } }
        public Sprite OrcLeftHand { get { return orcLeftHand; } }
        public Sprite OrcRightHand { get { return orcRightHand; } }
        public Sprite SkeletonLeftHand { get { return skeletonLeftHand; } }
        public Sprite SkeletonRightHand { get { return skeletonRightHand; } }
    }

    [System.Serializable]
    public class WeaponRes
    {
        public const string BULLET_EXPLOSION_CLIP_NAME = "Explosion";

        [SerializeField] private GameObject bullet;
        [SerializeField] private AnimatorController bulletAnimator;
        [SerializeField] private Sprite woodSword;
        [SerializeField] private Sprite woodShild;
        [SerializeField] private Sprite woodBow;
        [SerializeField] private Sprite woodArrow;
        [SerializeField] private Sprite woodWand;
        [SerializeField] private Sprite woodGrimoire;
        [SerializeField] private Sprite boneWand;
        [SerializeField] private Sprite boneGrimoire;

        public GameObject Bullet { get { return bullet; } }
        public AnimatorController BulletAnimator { get { return bulletAnimator; } }
        public Sprite WoodSword { get { return woodSword; } }
        public Sprite WoodShild { get { return woodShild; } }
        public Sprite WoodBow { get { return woodBow; } }
        public Sprite WoodArrow { get { return woodArrow; } }
        public Sprite WoodWand { get { return woodWand; } }
        public Sprite WoodGrimoire { get { return woodGrimoire; } }
        public Sprite BoneWand { get { return boneWand; } }
        public Sprite BoneGrimoire { get { return boneGrimoire; } }
    }
}
