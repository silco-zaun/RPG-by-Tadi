using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleUI : MonoBehaviour
{
    public enum BattleUIState
    {
        SelectUnit,
        SelectBehavior,
        ProgressTurn
    }

    [SerializeField] private BattleMenu battleUnitMenu;
    [SerializeField] private BattleMenu battleBehaviorMenu;

    private BattleSystem battleSystem;
    private BattleMenu battleMenu;
    private BattleUIState uiState;
    private List<string> behaviorMenu = new List<string>()
    {
        "공격", "방어", "스킬", "아이템", "파티", "진형", "도망"
    };

    public BattleMenu BattleMenu { get { return battleMenu; } }
    public BattleUIState UIState { get {  return uiState; } }

    private void Awake()
    {
        battleSystem = GetComponent<BattleSystem>();
    }

    private void Start()
    {
        SetBattleBehaviorMenu(behaviorMenu);
        SetUIToSelectUnit();
    }

    public void SetBattleUnitMenu(List<string> itemName)
    {
        battleUnitMenu.SetMenu(itemName);
    }

    private void SetBattleBehaviorMenu(List<string> itemName)
    {
        battleBehaviorMenu.SetMenu(itemName);
    }

    public void SetUIToSelectUnit()
    {
        uiState = BattleUIState.SelectUnit;
        battleUnitMenu.gameObject.SetActive(true);
        battleBehaviorMenu.gameObject.SetActive(false);
        battleMenu = battleUnitMenu;
    }

    public void SetUIToSelectBehavior()
    {
        uiState = BattleUIState.SelectBehavior;
        battleUnitMenu.gameObject.SetActive(false);
        battleBehaviorMenu.gameObject.SetActive(true);
        battleMenu = battleBehaviorMenu;
    }

    public void SetUIToProgressTurn()
    {
        uiState = BattleUIState.ProgressTurn;
        battleUnitMenu.gameObject.SetActive(false);
        battleBehaviorMenu.gameObject.SetActive(false);
    }

    public void SelectMenu(Vector2 vector)
    {
        battleMenu.SelectMenu(vector);
    }

    public void SubmitItem()
    {
        int menuIndex = battleMenu.SubmitItem();

        switch (UIState)
        {
            case BattleUI.BattleUIState.SelectUnit:
                battleSystem.SetSelectedPlayerUnitIndex(menuIndex);
                SetUIToSelectBehavior();
                break;
            case BattleUI.BattleUIState.SelectBehavior:
                battleSystem.SetPlayersBehavior(menuIndex);
                SetUIToSelectUnit();
                break;
        }
    }
}
