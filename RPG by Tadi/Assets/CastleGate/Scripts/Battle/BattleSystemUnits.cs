using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSystemUnits : MonoBehaviour
{
    [System.Serializable]
    public class PartyUnit
    {
        [SerializeField] private GameObject formation;
        [SerializeField] private CharacterBaseData data;

        private GameObject battleUnit;
        private UnitController unitController;
        private BattleUnitController battleUnitController;
        private string name;

        public GameObject Formation { get { return formation; } }
        public CharacterBaseData Data { get { return data; } }
        public GameObject BattleUnit
        {
            get { return battleUnit; }
            set
            {
                battleUnit = value;
                unitController = battleUnit.GetComponent<UnitController>();
                battleUnitController = battleUnit.GetComponent<BattleUnitController>();
            }
        }
        public UnitController UnitController { get { return unitController; } }
        public BattleUnitController BattleUnitController { get { return battleUnitController; } }
        public string Name { get { return name; } set { name = value; } }
        public bool IsDefeated { get { return battleUnitController.IsDefeated; } }
    }

    [SerializeField] private GameObject battleUnitPrefab;
    [SerializeField] private List<PartyUnit> leftPartyUnits;
    [SerializeField] private List<PartyUnit> rightPartyUnits;

    private List<BattleUnitController> battleUnitsOrderBySpeed = new List<BattleUnitController>();
    private BattleSystemUI battleUI;

    private int selectedBehaviorUnitIndex = 0;
    private int selectedTargetUnitIndex = 0;
    private BattleUnitController selectedTargetUnitController;

    public List<BattleUnitController> BattleUnitsOrderBySpeed { get { return battleUnitsOrderBySpeed; } }

    private void Awake()
    {
        battleUI = GetComponent<BattleSystemUI>();
    }

    public void SetBattleUnit()
    {
        SetPartyUnis(ref leftPartyUnits, true);
        SetPartyUnis(ref rightPartyUnits, false);
        SetPlayerUnitsMenu();

        List<PartyUnit> partyUnitsOrderBySpeed = leftPartyUnits.Concat(rightPartyUnits).OrderBy(u => u.BattleUnitController.Character.Speed).ToList();

        foreach (PartyUnit partyUnit in partyUnitsOrderBySpeed)
        {
            battleUnitsOrderBySpeed.Add(partyUnit.BattleUnitController);
        }
    }

    private void SetPartyUnis(ref List<PartyUnit> partyUnits, bool isPlayerUnit)
    {
        foreach (PartyUnit unit in partyUnits)
        {
            Transform parent = unit.Formation.transform;
            unit.BattleUnit = Instantiate(battleUnitPrefab, parent);

            unit.UnitController.CharacterBaseData = unit.Data;
            unit.UnitController.SetCharacterData(1);
            unit.BattleUnitController.IsPlayerParty = isPlayerUnit;
            DataManager.CharacterTypeKor type = (DataManager.CharacterTypeKor)unit.BattleUnitController.Character.Type;
            unit.Name = type.ToString();
        }
    }

    private void SetPlayerUnitsMenu()
    {
        List<ItemInfo> infos = new List<ItemInfo>();

        foreach (PartyUnit unit in leftPartyUnits)
        {
            ItemInfo info = new ItemInfo(unit.Name);
            infos.Add(info);
        }
        
        battleUI.UnitMenuItemsInfo = infos;
    }

    public void SetSelectedPlayerUnitIndex(int itemIndex)
    {
        selectedBehaviorUnitIndex = itemIndex;
    }

    public bool ResetPlayersBehavior()
    {
        battleUnitsOrderBySpeed.ForEach(u => u.Behavior = BattleDataManager.BattleUnitBehavior.None);

        battleUI.SetUnitMenuItemColorState(ItemInfo.ItemColorState.OriginColor);

        bool selectBehaviorComplete = CheckSelectBehaviorComplete();

        return selectBehaviorComplete;
    }

    public bool SetPlayersBehavior(BattleDataManager.BattleUnitBehavior behavior)
    {
        BattleUnitController unitController = leftPartyUnits[selectedBehaviorUnitIndex].BattleUnitController;

        unitController.Behavior = behavior;
        battleUI.SetUnitMenuItemColorState(ItemInfo.ItemColorState.DeactivatedColor);

        bool selectBehaviorComplete = CheckSelectBehaviorComplete();

        return selectBehaviorComplete;
    }

    public void SetTargetUnit()
    {
        selectedTargetUnitController.SetSelector(false);

        BattleUnitController behaveUnitController = leftPartyUnits[selectedBehaviorUnitIndex].BattleUnitController;

        behaveUnitController.AddTarget(selectedTargetUnitController);
        battleUI.SetUnitMenuItemColorState(ItemInfo.ItemColorState.DeactivatedColor);
    }

    public void SetAIUnitBehavior()
    {
        foreach (PartyUnit unit in rightPartyUnits)
        {
            unit.BattleUnitController.Behavior = BattleDataManager.BattleUnitBehavior.Attack;

            unit.BattleUnitController.AddTarget(leftPartyUnits[0].BattleUnitController);
        }
    }

    private bool CheckSelectBehaviorComplete()
    {
        if (leftPartyUnits.Where(u => u.BattleUnitController.Behavior == BattleDataManager.BattleUnitBehavior.None).Count() == 0)
        {
            return true;
        }

        return false;
    }

    public BattleDataManager.BattleResult CheckBattleResult()
    {
        BattleDataManager.BattleResult battleResult = BattleDataManager.BattleResult.None;
        int leftPartyCount = leftPartyUnits.Where(u => !u.IsDefeated).Count();
        int rightPartyCount = leftPartyUnits.Where(u => !u.IsDefeated).Count();

        if (leftPartyCount == 0 && rightPartyCount == 0)
        {
            // Tie
            battleResult = BattleDataManager.BattleResult.Tie;
        }
        else if (leftPartyCount == 0)
        {
            // Right win
            battleResult = BattleDataManager.BattleResult.RightWin;
        }
        else if (rightPartyCount == 0)
        {
            // Left win
            battleResult = BattleDataManager.BattleResult.LeftWin;
        }
        else
        {
            // Not game over
            battleResult = BattleDataManager.BattleResult.None;
        }

        return battleResult;
    }

    public void SelectTargetUnit(bool selectEnemy, Vector2 vector)
    {
        if (selectEnemy)
        {
            SelectTargetUnit(ref rightPartyUnits, vector);
        }
        else
        {
            // Select ally
            SelectTargetUnit(ref leftPartyUnits, vector);
        }
    }

    private void SelectTargetUnit(ref List<PartyUnit> partyUnits, Vector2 vector)
    {
        // 2 3
        // 0 1
        // 4 5
        int selectTargetUnitIndex = selectedTargetUnitIndex;

        if (vector.x != 0)
        {
            selectTargetUnitIndex = selectedTargetUnitIndex % 2 == 0 ? selectedTargetUnitIndex + 1 : selectedTargetUnitIndex - 1;
        }
        else if (vector.y > 0)
        {
            selectTargetUnitIndex = (selectedTargetUnitIndex + 2) % 6;
        }
        else if (vector.y < 0)
        {
            selectTargetUnitIndex = (selectedTargetUnitIndex + 4) % 6;
        }

        if (selectTargetUnitIndex < partyUnits.Count && 
            partyUnits[selectTargetUnitIndex] != null)
        {
            partyUnits[selectedTargetUnitIndex].BattleUnitController.SetSelector(false);
            partyUnits[selectTargetUnitIndex].BattleUnitController.SetSelector(true);
            selectedTargetUnitIndex = selectTargetUnitIndex;
            selectedTargetUnitController = partyUnits[selectedTargetUnitIndex].BattleUnitController;
        }
    }
}
