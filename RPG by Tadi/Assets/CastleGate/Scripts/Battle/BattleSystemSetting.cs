using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.InputSystem.OnScreen.OnScreenStick;

public class BattleSystemSetting : MonoBehaviour
{
    // Set battle location to player location
    private PlayerUnitController player;
    private Vector3 battleLocation;

    private void Awake()
    {
        player = FindObjectOfType<PlayerUnitController>();
    }

    private void Start()
    {

    }

    private void Update()
    {
        HandleBattleLocation();
    }

    private void HandleBattleLocation()
    {
        if (player != null)
        {
            battleLocation.Set(player.transform.position.x, player.transform.position.y, transform.position.z);
            transform.position = battleLocation;
        }
    }
}
