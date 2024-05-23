using Tadi.Data.State;
using Tadi.Data.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerUnitCommands : MonoBehaviour
{
    public enum State
    {
        FreeRoam,
        ShowDialogue,
    }

    private PlayerUnitMovement move;

    private void Awake()
    {
        move = GetComponent<PlayerUnitMovement>();
    }

    private void OnEnable()
    {
        PlayerInputData.OnMove += OnMove;
        PlayerInputData.attackAction.performed += OnAttack;
        PlayerInputData.dashAction.performed += OnDash;
    }

    private void OnDisable()
    {
        PlayerInputData.OnMove -= OnMove;
        PlayerInputData.attackAction.performed -= OnAttack;
        PlayerInputData.dashAction.performed -= OnDash;
    }

    private void LateUpdate()
    {
        switch (StateData.PlayerState)
        {
            case PlayerState.FreeRoam:
                HandleDefence();
                break;
            case PlayerState.ShowDialogue:
                break;
        }
    }

    private void OnMove(Vector2 moveVec)
    {
        switch (StateData.PlayerState)
        {
            case PlayerState.FreeRoam:
                move.SetMoveVector(moveVec);
                break;
            case PlayerState.ShowDialogue:
                break;
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        switch (StateData.PlayerState)
        {
            case PlayerState.FreeRoam:
                move.Attack();
                break;
            case PlayerState.ShowDialogue:
                bool endDialogue = Managers.Ins.Dlg.DisplayNextDialogLine();

                if (endDialogue)
                {
                    SetToFreeRoamState();
                }
                break;
        }
    }

    private void OnDash(InputAction.CallbackContext context)
    {
        switch (StateData.PlayerState)
        {
            case PlayerState.FreeRoam:
                move.Dash();
                break;
            case PlayerState.ShowDialogue:
                break;
        }
    }

    private void HandleDefence()
    {
        //bool isDefenceBtnPressed = inputActions.Player.PlayDefenseAnim.IsPressed();
        bool isDefenceBtnPressed = PlayerInputData.defenseAction.IsPressed();
        move.IsDefencing = isDefenceBtnPressed;
    }

    public void SetToFreeRoamState()
    {
        StateData.PlayerState = PlayerState.FreeRoam;
    }

    public void SetToShowDialogState()
    {
        move.SetMoveVector(Vector2.zero);
        StateData.PlayerState = PlayerState.ShowDialogue;
    }
}
