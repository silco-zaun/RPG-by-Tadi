using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleSystem : MonoBehaviour
{
    private BattleSetting battleSetting;
    private BattleCommands battleCommands;
    private BattleUI battleUI;

    private GameObject behaveUnitObject;
    private GameObject targetUnitObject;
    private int selectedPlayerUnitIndex;

    private System.Action OnAttackTarget;

    private void Awake()
    {
        battleCommands = GetComponent<BattleCommands>();
        battleSetting = GetComponent<BattleSetting>();
    }

    private void Start()
    {
        //battleUI.BattleMenu.onSubmit += SetPlayersBehavior;

        // -- Temp code --
        // 1. Set battle unit
        battleSetting.SetBattleUnit();

        // 2. Player select command

        // 3. Progress turn

        // 4. If not game over, Player select command (-> 2)
    }

    public void SetSelectedPlayerUnitIndex(int menuIndex)
    {
        selectedPlayerUnitIndex = menuIndex;
    }

    public void SetPlayersBehavior(int menuIndex)
    {
        GameObject selectedPlayerUnit = battleSetting.LeftPartyBattleUnits[selectedPlayerUnitIndex];
        selectedPlayerUnit.GetComponent<BattleUnit>().Behavior = (BattleUnit.BattleUnitBehavior)menuIndex;
    }

    private void SelectTargetUnit()
    {
        // 4. Select targetUnitObject unit
        targetUnitObject = battleSetting.RightPartyBattleUnits[0];
    }

    private void ProgressPlayerTurn()
    {
        battleUI.SetUIToSelectBehavior();
    }

    private void ProgressAITurn()
    {
        Attack();
    }

    private void ChooseNextActiveCharacter()
    {
        if (IsBattleOver())
        {
            return;
        }

        //SetBehaveUnit();
    }

    private bool IsBattleOver()
    {
        return false;
    }

    private void Attack()
    {
        behaveUnitObject.GetComponent<BattleUnitMovement>().Attack(targetUnitObject.transform.position,
            () =>
            {
                AttackTarget();
            },
            () =>
            {
                ChooseNextActiveCharacter();
            });
    }

    private void Defence()
    {

    }

    private void Skill()
    {

    }

    private void Item()
    {

    }

    private void Party()
    {

    }

    private void Formation()
    {

    }

    private void Escape()
    {

    }

    private void AttackTarget()
    {
        BattleUnit behaveUnit = behaveUnitObject.GetComponent<BattleUnit>();
        BattleUnit targetUnit = targetUnitObject.GetComponent<BattleUnit>();

        behaveUnit.AttackTarget(targetUnit);
    }

}