using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers instance;
    private GameStateManager gameStateManager;
    private UnitManager unitManager;
    private ResourceManager resourceManager;
    private AnimationManager animationManager;
    private DialogueManager dialogManager;

    public static Managers Ins { get { return instance; } }
    public GameStateManager Stat { get { return gameStateManager; } }
    public UnitManager Unit { get { return unitManager; } }
    public ResourceManager Res { get { return resourceManager; } }
    public AnimationManager Anim { get { return animationManager; } }
    public DialogueManager Dlg { get { return dialogManager; } }

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

        gameStateManager = GetComponent<GameStateManager>();
        unitManager = GetComponent<UnitManager>();
        resourceManager = GetComponent<ResourceManager>();
        animationManager = GetComponent<AnimationManager>();
        dialogManager = GetComponent<DialogueManager>();
    }
}
