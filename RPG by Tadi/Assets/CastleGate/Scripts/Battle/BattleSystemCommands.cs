using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleSystemCommands : MonoBehaviour
{
    private BattleSystemController battleController;

    private BattleControls inputActions;

    private void Awake()
    {
        battleController = GetComponent<BattleSystemController>();
        inputActions = new BattleControls();
        //battleControls.UI.NavigateMenu;
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

        if (vector.x != 0 || vector.y != 0)
        {
            battleController.NavigateUI(vector);
        }
    }

    private void OnSubmit()
    {
        battleController.SubmitUI();
    }

    private void OnCancel()
    {
        battleController.CancelUI();
    }
}
