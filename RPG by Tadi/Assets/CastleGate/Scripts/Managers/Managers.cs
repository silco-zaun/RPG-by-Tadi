
using UnityEngine;

public class Managers : Singleton<Managers>
{
    private GameStateManager gameStateManager;
    private UnitManager unitManager;
    private ResourceManager resourceManager;
    private AnimationManager animationManager;
    private DialogManager dialogManager;
    private SceneChangeManager sceneChangeManager;

    public GameStateManager Stat { get { return gameStateManager; } }
    public UnitManager Unit { get { return unitManager; } }
    public ResourceManager Res { get { return resourceManager; } }
    public AnimationManager Anim { get { return animationManager; } }
    public DialogManager Dlg { get { return dialogManager; } }
    public SceneChangeManager Scn { get { return sceneChangeManager; } }

    private new void Awake()
    {
        base.Awake();

        gameStateManager = GetComponent<GameStateManager>();
        unitManager = GetComponent<UnitManager>();
        resourceManager = GetComponent<ResourceManager>();
        animationManager = GetComponent<AnimationManager>();
        dialogManager = GetComponent<DialogManager>();
        sceneChangeManager = GetComponent<SceneChangeManager>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        gameStateManager.Init();
        unitManager.Init();
        resourceManager.Init();
        dialogManager.Init();
    }
}
