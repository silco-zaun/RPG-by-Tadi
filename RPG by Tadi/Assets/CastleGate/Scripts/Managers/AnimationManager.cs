using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private List<AnimatorController> characterAnimator;
    [SerializeField] private List<AnimatorController> bulletAnimator;

    private Dictionary<string, AnimationEvent> animationEventDictionary = new Dictionary<string, AnimationEvent>();
    //private List<float[]> eventTimes = new List<float[]>();

    public List<AnimatorController> CharacterAnimator { get { return characterAnimator; } }
    public List<AnimatorController> BulletAnimator { get { return bulletAnimator; } }

    private void Awake()
    {
        bool check = CheckList();

        if (!check)
            return;

        InitializeAnimationEvent();
    }

    private bool CheckList()
    {
        int enumCount = Enum.GetValues(typeof(CharacterDataManager.CharacterType)).Length;

        if (characterAnimator.Count != enumCount - 1)
        {
            Debug.LogError("Character animator must to be set.");
            return false;
        }

        for (int i = 0; i < characterAnimator.Count; i++)
        {
            string name = ((CharacterDataManager.CharacterType)(i + 1)).ToString();
            string animator = characterAnimator[i].name;
            bool check = animator.Equals(name);

            if (!check)
            {
                Debug.LogError($"Character name and animator name are not matched.\nCharacter name : {name}\nAnimator name : {animator}");
                return false;
            }
        }

        enumCount = Enum.GetValues(typeof(WeaponDataManager.MagicBulletType)).Length;

        if (bulletAnimator.Count != enumCount - 1)
        {
            Debug.LogError("Character animator must to be set.");
            return false;
        }

        for (int i = 0; i < bulletAnimator.Count; i++)
        {
            string name = ((WeaponDataManager.MagicBulletType)(i + 1)).ToString();
            string animator = bulletAnimator[i].name;
            bool check = animator.Equals(name);

            if (!check)
            {
                Debug.LogError($"Bullet name and animator name are not matched.\nBullet name : {name}\nAnimator name : {animator}");
                return false;
            }
        }

        return true;
    }

    public void InitializeAnimationEvent()
    {
        foreach (AnimatorController controller in characterAnimator)
        {
            AnimationClip[] clips = controller.animationClips;

            for (int i = 0; i < clips.Length; i++)
            {
                AnimationClip animationClip = clips[i];

                if (animationClip.name.Equals("LeftAttack") ||
                    animationClip.name.Equals("RightAttack"))
                {
                    AddAnimationEvent(animationClip, $"OnAttackAnimationStart", 0);

                    AddAnimationEvent(animationClip, $"OnAttackAnimationComplete", animationClip.length);
                }
            }
        }

        foreach (AnimatorController controller in bulletAnimator)
        {
            AnimationClip[] clips = controller.animationClips;

            for (int i = 0; i < clips.Length; i++)
            {
                AnimationClip animationClip = clips[i];

                if (animationClip.name.Equals("Explosion"))
                {
                    AddAnimationEvent(animationClip, $"OnExplosionAnimationStart", 0);

                    AddAnimationEvent(animationClip, $"OnExplosionAnimationComplete", animationClip.length);
                }
            }
        }


        // Loop through all values of the CharacterClass enum
        //foreach (DataManager.CharacterType characterType in Enum.GetValues(typeof//(DataManager.CharacterType)))
        //{
        //    Debug.Log("Class: " + characterType);
        //}
    }

    public void AddAnimationEvent(AnimationClip animationClip, string functionName, float eventTime)
    {
        // Check if animationClip and functionName are assigned
        if (animationClip == null || string.IsNullOrEmpty(functionName))
        {
            Debug.LogError("Animation clip or function skillName not assigned.");
            
            return;
        }
        
        // Check if an animatione vent already exists at the specified time
        foreach (AnimationEvent animationEvent in animationClip.events)
        {
            if (Mathf.Approximately(animationEvent.time, eventTime))
            {
                Debug.LogError($"{functionName} function already exist in this clip.");
                
                return;
            }
        }

        // Create a new AnimationEvent
        AnimationEvent newEvent = new AnimationEvent
        {
            time = eventTime,
            functionName = functionName,
            stringParameter = functionName
        };

        // Add the new animationEvent to the clip
        animationClip.AddEvent(newEvent);

        string keyString = animationClip.GetInstanceID().ToString() + functionName + eventTime;
        animationEventDictionary.Add(keyString, newEvent);

        // Refresh the animation clip
        //AnimationUtility.SetAnimationClipSettings(animationClip, new AnimationClipSettings());
    }

    public void RemoveAnimationEvent(AnimationClip animationClip, string functionName)
    {
        // Check if animationClip and functionName are assigned
        if (animationClip == null || string.IsNullOrEmpty(functionName))
        {
            Debug.LogWarning("Animation clip or function skillName not assigned.");
        }

        // Loop through all animationEvents in the animation clip
        for (int i = 0; i < animationClip.events.Length; i++)
        {
            AnimationEvent animationEvent = animationClip.events[i];
            // If the function skillName matches, remove the animationEvent
            if (animationEvent.functionName == functionName)
            {
                float eventTime = animationEvent.time;
                //string keyString = animationClip.GetInstanceID().ToString() + eventTime.ToString("F2");
                animationEventDictionary.Remove(functionName);

                animationClip.events[i] = null; // Remove the animationEvent by battleSetting it to null

                break; // Exit the loop once the animationEvent is removed
            }
        }
    }
}
