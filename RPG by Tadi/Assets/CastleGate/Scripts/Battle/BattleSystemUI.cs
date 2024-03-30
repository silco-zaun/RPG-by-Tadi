using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystemUI : MonoBehaviour
{
    [SerializeField] private ScrollViewController battleMenu;
    [SerializeField] private Text dialogText;

    // breadth index = parent breadth index * childs per parent + child index

    // Root
    private const int DEPTH_ROOT_INDEX = 0;
    private int rootBreadthIndex = 0; // 0
    private int unitMenuItemCount;

    // Depth 1
    private const int DEPTH_1_INDEX = 1;
    private int depth1ChildsPerParent; // unitMenuItemCount
    private int depth1BreadthIndex = 0; // unitMenuItemIndex

    // Depth 2
    private const int DEPTH_2_INDEX = 2;
    private const int TARGET_MENU_CHILD_INDEX = 0;
    private const int SKILL_MENU_CHILD_INDEX = 1;
    private int depth2ChildsPerParent = 2; // 2
    private int depth2BreadthIndex = 0; // depth1BreadthIndex * depth2ChildsPerParent + (TARGET_MENU_CHILD_INDEX or SKILL_MENU_CHILD_INDEX)

    // Depth 3
    private const int DEPTH_3_INDEX = 3;
    private const int SKILL_TARGET_MENU_CHILD_INDEX = 0;
    private int depth3ChildsPerParent = 1; // 1
    private int depth3BreadthIndex = 0; // skillMenuBreadthIndex / depth2ChildsPerParent

    public ScrollViewController BattleMenu { get { return battleMenu; } }
    
    private void Awake()
    {
    }

    public void CreateBattleMenu(List<string> unitNames, List<string> actionNames)
    {
        depth1ChildsPerParent = unitNames.Count; // unitMenuItemCount

        battleMenu.RemoveAllMenusInTree();
        battleMenu.SetRootMenu(unitNames); // Root - Unit Menu
        BattleMenu.AddChildMenus(DEPTH_ROOT_INDEX, depth1ChildsPerParent, actionNames); // Depth1 - Action Menu
        BattleMenu.AddChildMenus(DEPTH_1_INDEX, depth2ChildsPerParent, null); // Depth2 - Target Menu, CharacterSkill Menu
        BattleMenu.AddChildMenus(DEPTH_2_INDEX, depth3ChildsPerParent, null); // Depth3 - CharacterSkill Target Menu
    }

    public int SetUIToSelectPlayerUnit()
    {
        int index = battleMenu.ChangeMenu(DEPTH_ROOT_INDEX, rootBreadthIndex);

        return index;
    }
    
    public void SetUIToSelectAction()
    {
        battleMenu.ChangeMenu(DEPTH_1_INDEX, depth1BreadthIndex);
    }

    public void SetUIToSelectTargetUnit(List<string> targets)
    {
        battleMenu.ChangeMenu(DEPTH_2_INDEX, depth2BreadthIndex, targets);
    }

    public void SetUIToSelectSkill(List<string> skills)
    {
        battleMenu.ChangeMenu(DEPTH_2_INDEX, depth2BreadthIndex, skills);
    }

    public void SetUIToSelectSkillTarget(List<string> targets)
    {
        battleMenu.ChangeMenu(DEPTH_3_INDEX, depth3BreadthIndex, targets);
    }

    public void SetUIToProgressRound()
    {
        battleMenu.ClearMenus();
    }

    public void SetUnitMenuItemColorState(ItemInfo.ItemColorState colorState)
    {
        battleMenu.SetItemColorState(DEPTH_ROOT_INDEX, rootBreadthIndex, depth1BreadthIndex, colorState);
    }

    public int NavigateMenu(Vector2 vector)
    {
        int selectedItemIndex = battleMenu.SelectItem(vector);

        return selectedItemIndex;
    }

    public int SubmitMenu(Tadi.Datas.BattleSystem.BattleState state)
    {
        int itemIndex = battleMenu.SubmitItem();

        switch (state)
        {
            case Tadi.Datas.BattleSystem.BattleState.SelectPlayerUnit:
                depth1BreadthIndex = itemIndex;
                break;
            case Tadi.Datas.BattleSystem.BattleState.SelectTarget:
                depth2BreadthIndex = depth1BreadthIndex * depth2ChildsPerParent + TARGET_MENU_CHILD_INDEX;
                break;
            case Tadi.Datas.BattleSystem.BattleState.SelectSkill:
                depth2BreadthIndex = depth1BreadthIndex * depth2ChildsPerParent + SKILL_MENU_CHILD_INDEX;
                break;
            case Tadi.Datas.BattleSystem.BattleState.SelectSkillTarget:
                depth3BreadthIndex = depth2BreadthIndex * depth3ChildsPerParent + SKILL_TARGET_MENU_CHILD_INDEX;
                break;
        }

        return itemIndex;
    }

    public void ResetAllMenu()
    {
        battleMenu.ResetAllMenu();
    }

    public void SetDialogText()
    {
        //Managers.Ins.Dlg.dialogueText = dialogText;
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
