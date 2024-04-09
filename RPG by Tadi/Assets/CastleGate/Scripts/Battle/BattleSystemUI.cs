using System.Collections;
using System.Collections.Generic;
using Tadi.Datas.BattleSystem;
using Tadi.UI.ScrollView;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystemUI : MonoBehaviour
{
    [SerializeField] private MenuController battleMenu;
    [SerializeField] private Text dialogText;

    private const int UNIT_MENU_DEPTH = 0;
    private const int ACTION_MENU_DEPTH = 1;
    private const int TARGET_MENU_DEPTH = 2;
    private const int SKILL_MENU_DEPTH = 2;
    private const int SKILL_TARGET_MENU_DEPTH = 3;
    private int unitIndex = 0;
    private int menuIndex = 0;

    public MenuController BattleMenu { get { return battleMenu; } }
    
    private void Awake()
    {
    }

    public void CreateBattleMenu(List<ItemInfo> unitNameList, List<ItemInfo> targetNameList, List<ItemInfo> actionNameList, List<List<ItemInfo>> skillNameList)
    {
        battleMenu.Tree.SetRootMenu(MenuType.UnitMenu, unitNameList);
        battleMenu.Tree.AddChildMenus(0, MenuType.ActionMenu, actionNameList);
        battleMenu.Tree.AddChildMenus(1, MenuType.TargetMenu, targetNameList);
        battleMenu.Tree.AddChildMenus(1, MenuType.SkillMenu, skillNameList);
        battleMenu.Tree.AddChildMenus(2, MenuType.SkillTargetMenu, targetNameList);
    }

    public int SetUIToSelectPlayerUnit()
    {
        menuIndex = unitIndex = battleMenu.ChangeMenu(MenuType.UnitMenu, UNIT_MENU_DEPTH, 0);

        return unitIndex;
    }
    
    public void SetUIToSelectAction()
    {
        battleMenu.ChangeMenu(MenuType.ActionMenu, ACTION_MENU_DEPTH, menuIndex);
    }

    public void SetUIToSelectTargetUnit()
    {
        battleMenu.ChangeMenu(MenuType.TargetMenu, TARGET_MENU_DEPTH, menuIndex);
    }

    public void SetUIToSelectSkill()
    {
        battleMenu.ChangeMenu(MenuType.SkillMenu, SKILL_MENU_DEPTH, menuIndex);
    }

    public void SetUIToSelectSkillTarget()
    {
        battleMenu.ChangeMenu(MenuType.SkillTargetMenu, SKILL_TARGET_MENU_DEPTH, menuIndex);
    }

    public void SetUIToProgressRound()
    {
        battleMenu.ClearMenus();
    }

    public void SetPlayerAction(ItemState colorState)
    {
        menuIndex = 0;
        SetUnitMenuItemColorState(colorState);
    }

    private void SetUnitMenuItemColorState(ItemState colorState)
    {
        battleMenu.SetItemState(MenuType.UnitMenu, UNIT_MENU_DEPTH, menuIndex, unitIndex, colorState);
    }

    public int NavigateMenu(Vector2 vector)
    {
        int selectedItemIndex = battleMenu.SelectItem(vector);

        return selectedItemIndex;
    }

    public int SubmitMenu(BattleState state)
    {
        int itemIndex = battleMenu.SubmitItem();

        return itemIndex;
    }

    public void ResetAllMenu()
    {
        battleMenu.Tree.ResetAllMenu();
    }

    public void TypeSentence(string line)
    {
        StopAllCoroutines();
        StartCoroutine(Managers.Ins.Dlg.TypeSentence(dialogText, line));
    }

    public void DisplaySentence(string line)
    {
        StopAllCoroutines();
        Managers.Ins.Dlg.DisplaySentence(dialogText, line);
    }
}
