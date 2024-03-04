using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private List<AnimatorController> animatorControllers;

    private Dictionary<string, AnimationEvent> animationEventDictionary = new Dictionary<string, AnimationEvent>();
    //private List<float[]> eventTimes = new List<float[]>();

    public List<AnimatorController> AnimatorControllers { get { return animatorControllers; } }

    private static AnimationManager instance;

    public static AnimationManager Instance { get { return instance; } }

    private void Awake()
    {
        // Check if an instance already exists
        if (instance != null && instance != this)
        {
            // If an instance already exists and it's not this one, destroy this instance
            Destroy(gameObject);

            return;
        }

        // Assign this instance as the singleton instance
        instance = this;

        // Optional: Prevent this object from being destroyed when loading new scenes
        if (transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        InitializeAnimationEvent();
    }

    public void InitializeAnimationEvent()
    {
        foreach (AnimatorController controller in animatorControllers)
        {
            AnimationClip[] clips = controller.animationClips;

            for (int i = 0; i < clips.Length; i++)
            {
                AnimationClip animationClip = clips[i];

                if (animationClip.name.Equals("SwordAttack"))
                {
                    AddAnimationEvent(animationClip, $"Handle{animationClip.name}AnimationStart", 0);

                    AddAnimationEvent(animationClip, $"Handle{animationClip.name}AnimationComplete", animationClip.length);
                }
            }
        }

        // Loop through all values of the CharacterClass enum
        //foreach (DataManager.CharacterType type in Enum.GetValues(typeof//(DataManager.CharacterType)))
        //{
        //    Debug.Log("Class: " + type);
        //}
    }

    public void AddAnimationEvent(AnimationClip animationClip, string functionName, float eventTime)
    {
        // Check if animationClip and functionName are assigned
        if (animationClip == null || string.IsNullOrEmpty(functionName))
        {
            Debug.LogWarning("Animation clip or function name not assigned.");
            
            return;
        }
        
        // Check if an animatione vent already exists at the specified time
        foreach (AnimationEvent animationEvent in animationClip.events)
        {
            if (Mathf.Approximately(animationEvent.time, eventTime))
            {
                Debug.LogWarning($"{functionName} function already exist in this clip.");
                
                return;
            }
        }

        // Create a new AnimationEvent
        AnimationEvent newEvent = new AnimationEvent
        {
            time = eventTime,
            functionName = functionName,
            stringParameter = animationClip.name
        };

        // Add the new animationEvent to the clip
        animationClip.AddEvent(newEvent);

        string keyString = animationClip.GetInstanceID().ToString() + eventTime.ToString("F2");
        animationEventDictionary.Add(keyString, newEvent);

        // Refresh the animation clip
        //AnimationUtility.SetAnimationClipSettings(animationClip, new AnimationClipSettings());
    }

    public void RemoveAnimationEvent(AnimationClip animationClip, string functionName)
    {
        // Check if animationClip and functionName are assigned
        if (animationClip == null || string.IsNullOrEmpty(functionName))
        {
            Debug.LogWarning("Animation clip or function name not assigned.");
        }

        // Loop through all animationEvents in the animation clip
        for (int i = 0; i < animationClip.events.Length; i++)
        {
            AnimationEvent animationEvent = animationClip.events[i];
            // If the function name matches, remove the animationEvent
            if (animationEvent.functionName == functionName)
            {
                float eventTime = animationEvent.time;
                string keyString = animationClip.GetInstanceID().ToString() + eventTime.ToString("F2");
                animationEventDictionary.Remove(keyString);

                animationClip.events[i] = null; // Remove the animationEvent by setting it to null

                break; // Exit the loop once the animationEvent is removed
            }
        }
    }
}
