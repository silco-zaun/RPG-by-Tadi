using System;
using Tadi.Data.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tadi.Data.UI
{
    public static class PlayerInputData
    {
        public static PlayerControls playerControls = new PlayerControls();
        public static InputAction moveAction = playerControls.Player.Move;
        public static InputAction attackAction = playerControls.Player.Attack;
        public static InputAction defenseAction = playerControls.Player.Defense;
        public static InputAction dashAction = playerControls.Player.Dash;
        public static InputAction navigateAction = playerControls.UI.Navigate;
        public static InputAction submitAction = playerControls.UI.Submit;
        public static InputAction cancelAction = playerControls.UI.Cancel;

        public static Action<Vector2> OnMove;
        public static Action<InputAction.CallbackContext> OnAttack;
        public static Action<InputAction.CallbackContext> OnDash;
        public static Action<InputAction.CallbackContext> OnNavigate;
        public static Action<InputAction.CallbackContext> OnSubmit;
        public static Action<InputAction.CallbackContext> OnCancel;
    }
}
