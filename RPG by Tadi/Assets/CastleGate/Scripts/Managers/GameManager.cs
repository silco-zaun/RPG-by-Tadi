using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private ResourceManager resourceManager;
    private AnimationManager animationManager;

    public static GameManager Ins { get { return instance; } }
    public ResourceManager Res { get { return resourceManager; } }
    public AnimationManager Anim { get { return animationManager; } }

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

        resourceManager = GetComponent<ResourceManager>();
        animationManager = GetComponent<AnimationManager>();
    }
}
