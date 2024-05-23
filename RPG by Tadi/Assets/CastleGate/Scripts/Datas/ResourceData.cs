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

        [SerializeField] private RuntimeAnimatorController knightAnimator;
        [SerializeField] private RuntimeAnimatorController rogueAnimator;
        [SerializeField] private RuntimeAnimatorController wizzardAnimator;
        [SerializeField] private RuntimeAnimatorController orcAnimator;
        [SerializeField] private RuntimeAnimatorController skeletonMageAnimator;
        [SerializeField] private RuntimeAnimatorController lizardManAnimator;
        [SerializeField] private RuntimeAnimatorController turtleKingAnimator;
        // Body
        [SerializeField] private Sprite knightBody;
        [SerializeField] private Sprite rogueBody;
        [SerializeField] private Sprite wizzardBody;
        [SerializeField] private Sprite orcBody;
        [SerializeField] private Sprite skeletonMageBody;
        [SerializeField] private Sprite lizardManBody;
        [SerializeField] private Sprite turtleKingBody;
        // Hand
        [SerializeField] private Sprite humanLeftHand;
        [SerializeField] private Sprite humanRightHand;
        [SerializeField] private Sprite orcLeftHand;
        [SerializeField] private Sprite orcRightHand;
        [SerializeField] private Sprite skeletonLeftHand;
        [SerializeField] private Sprite skeletonRightHand;

        public RuntimeAnimatorController KnightAnimator { get { return knightAnimator; } }
        public RuntimeAnimatorController RogueAnimator { get { return rogueAnimator; } }
        public RuntimeAnimatorController WizzardAnimator { get { return wizzardAnimator; } }
        public RuntimeAnimatorController OrcAnimator { get { return orcAnimator; } }
        public RuntimeAnimatorController SkeletonMageAnimator { get { return skeletonMageAnimator; } }
        public RuntimeAnimatorController LizardManAnimator { get { return lizardManAnimator; } }
        public RuntimeAnimatorController TurtleKingAnimator { get { return turtleKingAnimator; } }
        // Body
        public Sprite KnightBody { get { return knightBody; } }
        public Sprite RogueBody { get { return rogueBody; } }
        public Sprite WizzardBody { get { return wizzardBody; } }
        public Sprite OrcBody { get { return orcBody; } }
        public Sprite SkeletonMageBody { get { return skeletonMageBody; } }
        public Sprite LizardManBody { get { return lizardManBody; } }
        public Sprite TurtleKingBody { get { return turtleKingBody; } }
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
        [SerializeField] private RuntimeAnimatorController magicBolt;
        [SerializeField] private RuntimeAnimatorController darkBolt;
        [SerializeField] private RuntimeAnimatorController fireBall;
        [SerializeField] private Sprite woodSword;
        [SerializeField] private Sprite woodShild;
        [SerializeField] private Sprite woodBow;
        [SerializeField] private Sprite woodArrow;
        [SerializeField] private Sprite woodWand;
        [SerializeField] private Sprite woodGrimoire;
        [SerializeField] private Sprite boneWand;
        [SerializeField] private Sprite boneGrimoire;

        public GameObject Bullet { get { return bullet; } }
        public RuntimeAnimatorController MagicBolt { get { return magicBolt; } }
        public RuntimeAnimatorController DarkBolt { get { return darkBolt; } }
        public RuntimeAnimatorController FireBall { get { return fireBall; } }
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
