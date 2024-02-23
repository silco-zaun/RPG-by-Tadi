using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleSystem : MonoBehaviour
{
    private BattleControls battleControls;

    private void Awake()
    {
        battleControls = new BattleControls();
        //battleControls.UI.Navigate;
    }

    private void OnEnable()
    {
        battleControls.Enable();
    }

    private void OnDisable()
    {
        battleControls.Disable();
    }

    private void OnNavigate(InputValue value)
    {
        Vector2 vec = value.Get<Vector2>();
    }
}
