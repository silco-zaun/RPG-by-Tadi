using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject units;
    [SerializeField] private GameObject battleSystem;

    private static GameState currentGameState;

    public GameState CurrentState { get { return currentGameState; } }

    private void Awake()
    {
    }

    private void Start()
    {
        // Set initial game state to Exploration
        TransitionToState(GameState.Battle);
    }

    private void Update()
    {
        // Example usage: checking the current game state
        //GameState currentState = GameStateManager.GetCurrentState();

        // Example logic based on the current game state
        switch (currentGameState)
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

    // Example method for transitioning to a different game state
    public void TransitionToState(GameState newState)
    {
        currentGameState = newState;

        // Additional logic can be added here based on the new state
        switch (currentGameState)
        {
            case GameState.Exploration:
                // Execute exploration behaviors
                mainCamera.SetActive(true);
                units.SetActive(true);
                battleSystem.SetActive(false);
                break;
            case GameState.Battle:
                // Execute battle behaviors
                mainCamera.SetActive(false);
                units.SetActive(false);
                battleSystem.SetActive(true);
                break;
                // Add cases for other game states...
        }
    }
}
