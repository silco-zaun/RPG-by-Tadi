using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUnitCommands : MonoBehaviour
{
    [SerializeField] private PlayerUnitMovement playerMovement;

    private PlayerControls inputActions;

    private void Awake()
    {
        inputActions = new PlayerControls();
        //playerControls.Player.FireBulletToTarget.performed += OnFire; // example
    }

    private void OnEnable()
    {
        //playerControls.Player.FireBulletToTarget.Enable(); // example
        inputActions.Enable();
    }

    private void OnDisable()
    {
        //playerControls.Player.FireBulletToTarget.Disable(); // example
        inputActions.Disable();
    }

    private void FixedUpdate()
    {

    }

    private void LateUpdate()
    {
        HandleDefence();
    }

    private void OnMove(InputValue value)
    {
        Vector2 moveVec = value.Get<Vector2>();
        playerMovement.SetMoveVector(moveVec);
    }

    private void OnFire()
    {
        playerMovement.Fire();
    }

    private void OnDash()
    {
        playerMovement.Dash();
    }

    private void HandleDefence()
    {
        bool isDefenceBtnPressed = inputActions.Player.Defense.IsPressed();
        playerMovement.IsDefencing = isDefenceBtnPressed;
    }
}
