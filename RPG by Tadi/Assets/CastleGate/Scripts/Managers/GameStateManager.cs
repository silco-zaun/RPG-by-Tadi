using System;
using System.Collections.Generic;
using Tadi.Data.State;
using Tadi.Datas.BattleSystem;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public GameObject FieldCamera { get { return FieldCameraController.Ins.gameObject; } }
    public GameObject PlayerUnit { get { return PlayerUnitController.Ins.gameObject; } }
    private GameObject AIUnits { get; set; }
    public GameObject BattleSystem { get { return BattleSystemController.Ins.gameObject; } }

    private void Update()
    {
        // Example usage: checking the current game state
        //GameState currentState = GameStateManager.GetCurrentState();

        // Example logic based on the current game state
        switch (StateData.GameState)
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
    
    public void Init()
    {
        AIUnits = GameObject.Find("AIUnits");

        TransitionToState(GameState.Exploration);
    }

    public void BattleStart(List<BattleUnitInfo> playersBattleUnitInfo, List<BattleUnitInfo> enemysBattleUnitInfo, GameObject enemyUnitObject, Action<BattleCondition> OnBattleEnd)
    {
        TransitionToState(GameState.Battle);

        BattleSystemController battle = BattleSystem.GetComponent<BattleSystemController>();
        battle.Init(playersBattleUnitInfo, enemysBattleUnitInfo, enemyUnitObject, OnBattleEnd);
    }

    public void BattleEnd()
    {
        TransitionToState(GameState.Exploration);
    }

    // Example method for transitioning to a different game state
    private void TransitionToState(GameState newState)
    {
        StateData.GameState = newState;

        // Additional logic can be added here based on the new state
        switch (StateData.GameState)
        {
            case GameState.Exploration:
                // Execute exploration behaviors
                FieldCamera.SetActive(true);
                PlayerUnit.SetActive(true);
                AIUnits?.SetActive(true);
                BattleSystem.SetActive(false);
                PlayerInputSystemController.Ins.EnablePlayerActionMap();
                StateData.PlayerState = PlayerState.FreeRoam;
                break;
            case GameState.Battle:
                // Execute battle behaviors
                FieldCamera.SetActive(false);
                PlayerUnit.SetActive(false);
                AIUnits.SetActive(false);
                BattleSystem.SetActive(true);
                PlayerInputSystemController.Ins.EnableUIActionMap();
                StateData.PlayerState = PlayerState.Battle;
                break;
                // Add cases for other game states...
        }
    }

    private GameObject FindGameObject(string objectNameToFind)
    {
        // Find all GameObjects with the specified name, including deactivated ones
        GameObject[] objects = Resources.FindObjectsOfTypeAll<GameObject>();

        // Filter out the inactive GameObjects by Name
        foreach (GameObject obj in objects)
        {
            if (obj.name == objectNameToFind)
            {
                // Handle the inactive GameObject
                return obj;
            }
        }

        return null;
    }
}
