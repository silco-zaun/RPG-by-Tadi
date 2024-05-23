using Tadi.Data.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystemController : Singleton<PlayerInputSystemController>
{
    private void OnEnable()
    {
        PlayerInputData.playerControls.Enable();
    }

    private void OnDisable()
    {
        PlayerInputData.playerControls.Disable();
    }

    private void Update()
    {
        Vector2 move = PlayerInputData.moveAction.ReadValue<Vector2>();

        PlayerInputData.OnMove?.Invoke(move);
    }

    public void EnablePlayerActionMap()
    {
        PlayerInputData.playerControls.UI.Disable();
        PlayerInputData.playerControls.Player.Enable();
    }

    public void EnableUIActionMap()
    {
        PlayerInputData.playerControls.Player.Disable();
        PlayerInputData.playerControls.UI.Enable();
    }
}
