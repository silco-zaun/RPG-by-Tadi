using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BattleDataManager;

public class BattleSystemUI : MonoBehaviour
{
    [SerializeField] private ScrollViewController battleMenu;

    private BattleSystemController battleController;
    private BattleSystemUnits battleUnits;

    private List<ItemInfo> unitMenuItemsInfo;
    private int unitMenuDepthIndex = 0;
    private int unitMenuBreadthIndex = 0;
    //private int unitMenuBreadth = 1; // root

    private List<ItemInfo> behaviorMenuItemsInfo;
    private int behaviorMenuDepthIndex = 1;
    private int behaviorMenuBreadthIndex = 0; // Parent index
    private int behaviorMenuBreadth; // Parent count

    BattleUnitBehavior selectedBehavior = BattleUnitBehavior.None;

    public ScrollViewController BattleMenu { get { return battleMenu; } }
    public List<ItemInfo> UnitMenuItemsInfo { get { return unitMenuItemsInfo; } set { unitMenuItemsInfo = value; } }
    public List<ItemInfo> BehaviorMenuItemsInfo { get { return behaviorMenuItemsInfo; } set { behaviorMenuItemsInfo = value; } }
    
    private void Awake()
    {
        battleController = GetComponent<BattleSystemController>();
        battleUnits = GetComponent<BattleSystemUnits>();
    }

    public void CreateBattleMenu()
    {
        battleMenu.RemoveAllMenusInTree();
        battleMenu.SetRootMenu(unitMenuItemsInfo);
        BattleMenu.AddChildMenus(unitMenuDepthIndex, unitMenuBreadthIndex, behaviorMenuItemsInfo);

        behaviorMenuBreadth = unitMenuItemsInfo.Count;
    }

    public void SetUIToSelectBehaviorUnit()
    {
        battleMenu.ChangeMenu(unitMenuDepthIndex, unitMenuBreadthIndex);
    }
    
    public void SetUIToSelectBehavior()
    {
        battleMenu.ChangeMenu(behaviorMenuDepthIndex, behaviorMenuBreadthIndex);
    }

    public void SetUIToSelectTargetUnit()
    {
        battleMenu.ClearMenus();
    }

    public void SetUIToDoBehavior()
    {
        battleMenu.ClearMenus();
    }

    public void SetUnitMenuItemColorState(ItemInfo.ItemColorState colorState)
    {
        battleMenu.SetItemColorState(unitMenuDepthIndex, unitMenuBreadthIndex, behaviorMenuBreadthIndex, colorState);
    }

    public void NavigateMenu(Vector2 vector)
    {
        battleMenu.SelectItem(vector);
    }

    public int SubmitMenu(BattleState state)
    {
        int itemIndex = battleMenu.SubmitItem();

        if  (state == BattleState.SelectBehaviorUnit)
            behaviorMenuBreadthIndex = itemIndex;

        return itemIndex;
    }

    public void ResetAllMenu()
    {
        battleMenu.ResetAllMenu();
    }
}
