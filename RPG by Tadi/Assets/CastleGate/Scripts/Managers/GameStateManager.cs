using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    MainMenu,
    Loading,
    Pause,
    Exploration,
    Cutscene,
    Dialogue,
    Menu,
    Inventory,
    Quest,
    Battle,
    Victory,
    Defeat,
    GameOver
}

public class GameStateManager : MonoBehaviour
{
    private GameObject[] allGameObjects;
    private GameObject mainCamera;
    private GameObject units;
    private GameObject battleSystem;


    public GameState GameState { get; private set; }
    public PlayerControls PlayerActions { get; private set; }
    public BattleControls BattleActions { get; private set; }

    private void Awake()
    {
        PlayerActions = new PlayerControls();
        BattleActions = new BattleControls();

        PlayerActions.Disable();
        BattleActions.Disable();
    }

    private void Start()
    {
        bool isError = Init();

        if (isError)
            return;
    }

    private void Update()
    {
        // Example usage: checking the current game state
        //GameState currentState = GameStateManager.GetCurrentState();

        // Example logic based on the current game state
        switch (GameState)
        {
            case GameState.Exploration:
                // Execute exploration behaviors
                break;
            case GameState.Battle:
                // Execute battle behaviors
                break;
                // Add cases for other game states...
        }
    }

    private bool Init()
    {
        FindAllGameObject();

        mainCamera = FindGameObject("Main Camera");

        if (mainCamera == null)
        {
            Debug.LogError("GameObject with the specified Name not found in the scene!");
            return false;
        }

        units = FindGameObject("Units");

        if (units == null)
        {
            Debug.LogError("GameObject with the specified Name not found in the scene!");
            return false;
        }

        battleSystem = FindGameObject("BattleSystem");

        if (battleSystem == null)
        {
            Debug.LogError("GameObject with the specified Name not found in the scene!");
            return false;
        }

        // Set initial game state to Exploration
        TransitionToState(GameState.Exploration);

        return true;
    }

    // Example method for transitioning to a different game state
    private void TransitionToState(GameState newState)
    {
        GameState = newState;

        // Additional logic can be added here based on the new state
        switch (GameState)
        {
            case GameState.Exploration:
                // Execute exploration behaviors
                mainCamera.SetActive(true);
                units.SetActive(true);
                battleSystem.SetActive(false);
                //BattleActions.Disable();
                PlayerActions.Enable();
                break;
            case GameState.Battle:
                // Execute battle behaviors
                mainCamera.SetActive(false);
                units.SetActive(false);
                battleSystem.SetActive(true);
                //PlayerActions.Disable();
                //BattleActions.Enable();
                break;
                // Add cases for other game states...
        }
    }

    private void SetInputAction()
    {

    }

    private void FindAllGameObject()
    {
        // Find all GameObjects with the specified Name in the scene, including inactive ones
        allGameObjects = Resources.FindObjectsOfTypeAll<GameObject>();
    }

    private GameObject FindGameObject(string objectNameToFind)
    {
        // Filter out the inactive GameObjects by Name
        foreach (GameObject gameObject in allGameObjects)
        {
            if (gameObject.name == objectNameToFind)
            {
                // Handle the inactive GameObject
                return gameObject;
            }
        }

        return null;
    }

    private GameObject FindMainCamera()
    {
        // Get all cameras in the scene
        Camera[] cameras = FindObjectsOfType<Camera>();

        // Iterate through each camera and check if it's tagged as "MainCamera"
        foreach (Camera camera in cameras)
        {
            if (camera.CompareTag("MainCamera"))
            {
                // Return the GameObject associated with the camera
                return camera.gameObject;
            }
        }

        // If no main camera is found, return null
        return null;
    }
}
