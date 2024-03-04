using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleCommands : MonoBehaviour
{
    private BattleUI battleUI;

    private BattleControls inputActions;

    private void Awake()
    {
        battleUI = GetComponent<BattleUI>();
        inputActions = new BattleControls();
        //battleControls.UI.Navigate;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnNavigate(InputValue value)
    {
        Vector2 vector = value.Get<Vector2>();

        battleUI.SelectMenu(vector);
    }

    private void OnSubmit()
    {
        battleUI.SubmitItem();
    }
}
