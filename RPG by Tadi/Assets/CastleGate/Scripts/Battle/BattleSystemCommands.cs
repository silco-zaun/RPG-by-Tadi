using Tadi.Data.State;
using Tadi.Data.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleSystemCommands : MonoBehaviour
{
    private BattleSystemController battleController;

    private void Awake()
    {
        battleController = GetComponent<BattleSystemController>();
    }

    private void OnEnable()
    {

        PlayerInputData.navigateAction.performed += OnNavigate;
        PlayerInputData.submitAction.performed += OnSubmit;
        PlayerInputData.cancelAction.performed += OnCancel;
    }

    private void OnDisable()
    {
        PlayerInputData.navigateAction.performed -= OnNavigate;
        PlayerInputData.submitAction.performed -= OnSubmit;
        PlayerInputData.cancelAction.performed -= OnCancel;
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        if (StateData.PlayerState == PlayerState.Battle)
        {
            Vector2 vector = context.ReadValue<Vector2>();

            if (vector.x != 0 || vector.y != 0)
            {
                battleController.Navigate(vector);
            }
        }
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        if (StateData.PlayerState == PlayerState.Battle)
            battleController.SubmitUI();
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if (StateData.PlayerState == PlayerState.Battle)
            battleController.CancelUI();
    }
}
