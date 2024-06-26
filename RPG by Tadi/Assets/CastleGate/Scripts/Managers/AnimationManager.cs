using System;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Dictionary<string, AnimationEvent> animationEventDictionary = new Dictionary<string, AnimationEvent>();

    private void Awake()
    {
        Debug.Log("test1");
    }

    private void Start()
    {
        Debug.Log("test2");
    }

    private bool Check(List<RuntimeAnimatorController> animators, Type type)
    {
        Array enumValues = Enum.GetValues(type);
        string enumName = type.Name;
        int enumCount = enumValues.Length;
        
        if (animators.Count != enumCount - 1)
        {
            Debug.LogError($"{enumName} animator must to be set.");
            return false;
        }

        for (int i = 0; i < animators.Count; i++)
        {
            string enumValueName = enumValues.GetValue(i + 1).ToString();
            string animator = animators[i].name;
            bool check = animator.Equals(enumValueName);

            if (!check)
            {
                Debug.LogError($"{enumName} Name and animator Name are not matched.\n{enumName} Name : {enumValueName}\nAnimator Name : {animator}");
                return false;
            }
        }

        return true;
    }

    public void AddAnimEvents(RuntimeAnimatorController animController, string clipName, string eventKeyword)
    {
        AnimationClip[] clips = animController.animationClips;

        for (int i = 0; i < clips.Length; i++)
        {
            AnimationClip clip = clips[i];

            if (clip.name.Equals(clipName))
            {
                AddAnimEvent(clip, $"On{eventKeyword}AnimStart", 0);

                AddAnimEvent(clip, $"On{eventKeyword}AnimComplete", clip.length);
            }
        }
    }

    private void AddAnimEvent(AnimationClip animationClip, string functionName, float eventTime)
    {
        // Check if clip and functionName are assigned
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
        //AnimationUtility.SetAnimationClipSettings(clip, new AnimationClipSettings());
    }

    private void RemoveAnimEvent(AnimationClip animationClip, string functionName)
    {
        // Check if clip and functionName are assigned
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
                //string keyString = clip.GetInstanceID().ToString() + eventTime.ToString("F2");
                animationEventDictionary.Remove(functionName);

                animationClip.events[i] = null; // Remove the animationEvent by battleSetting it to null

                break; // Exit the loop once the animationEvent is removed
            }
        }
    }
}
