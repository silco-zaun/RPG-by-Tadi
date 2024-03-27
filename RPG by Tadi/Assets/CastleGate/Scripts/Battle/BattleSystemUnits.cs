using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleSystemUnits : MonoBehaviour
{
    [System.Serializable]
    public class BattleUnitInfo
    {
        // -- Variables --
        [SerializeField] private CharacterSO baseData;
        [SerializeField] private Datas.BattleUnitParty battleUnitParty;
        [SerializeField] private Formation formation;

        // -- Formation --
        public CharacterSO Data { get { return baseData; } }
        public Datas.BattleUnitParty Party { get { return battleUnitParty; } }
        public Formation Formation { get { return formation; } }
    }

    // -- Variables --
    [SerializeField] private GameObject battleUnitPrefab;
    [SerializeField] private List<BattleUnitInfo> battleUnitsInfos;
    [SerializeField] private List<Formation> formations;

    private BattleSystemUI battleUI;

    private List<BattleUnitController> battleUnits = new List<BattleUnitController>();
    Queue<BattleUnitController> turnOrder = new Queue<BattleUnitController>();

    // -- Properties --
    public GameObject Prefab { get { return battleUnitPrefab; } }

    private void Awake()
    {
        battleUI = GetComponent<BattleSystemUI>();
    }

    public void InitializeBattleUnits()
    {
        InitializeFormation();
        InstantiatePartyUnits();
    }

    private void InitializeFormation()
    {
        if (formations.Count != Datas.Bat.PARTY_UNIT_COUNT * 2)
        {
            Debug.LogError($"An error occurred: Formations count must be {Datas.Bat.PARTY_UNIT_COUNT * 2}.\ncurrent value : {formations.Count}");

            return;
        }

        for (int i = 0; i < formations.Count; i++)
        {
            formations[i].Party = (Datas.BattleUnitParty)(i / Datas.Bat.PARTY_UNIT_COUNT);
            formations[i].Index = i % Datas.Bat.PARTY_UNIT_COUNT;
            formations[i].HlineIndex = i % Datas.Bat.HORIZONTAL_LINE_UNIT_COUNT;
            formations[i].VlineIndex = (i / Datas.Bat.HORIZONTAL_LINE_UNIT_COUNT) % Datas.Bat.VERTICAL_LINE_UNIT_COUNT;
        }
    }

    private void InstantiatePartyUnits()
    {
        foreach (BattleUnitInfo unitInfo in battleUnitsInfos)
        {
            Transform parent = unitInfo.Formation.transform;
            GameObject instance = Instantiate(battleUnitPrefab, parent);

            BattleUnitController unit = instance.GetComponent<BattleUnitController>();
            unit.CharacterBaseData = unitInfo.Data;
            unit.Formation = unitInfo.Formation;
            unit.Party = unitInfo.Party;
            unit.SetCharacterData(1);

            battleUnits.Add(unit);
        }
    }

    public void ResetBattleUnits()
    {
        battleUnits.ForEach(u => u.ResetBattleUnit());
    }

    public void SetTurnOrder()
    {
        List<BattleUnitController> units =
            battleUnits.OrderByDescending(
            u => u.CharacterController.Speed).OrderByDescending(
            u => u.ActionPriority).ToList();

        turnOrder.Clear();
        turnOrder = new Queue<BattleUnitController>(units);
    }

    public BattleUnitController GetBehaveUnit()
    {
        while (turnOrder.Count > 0)
        {
            BattleUnitController unit = turnOrder.Dequeue();

            if (unit.IsFainted == false)
            {
                SetRandomTarget(unit);

                return unit;
            }
        }

        return null;
    }
    
    public List<string> GetAliveUnitsNames(Datas.BattleUnitParty party)
    {
        List<string> names = new List<string>();
        List<BattleUnitController> alives = GetAliveUnits(party);

        foreach (BattleUnitController unit in alives)
        {
            string name = Datas.Cha.Names[(int)unit.CharacterController.CharacterType];
            names.Add(name);
        }

        return names;
    }

    public List<string> GetAliveUnitsSkillNames(Datas.BattleUnitParty party, int index)
    {
        List<BattleUnitController> alives = GetAliveUnits(party);
        List<string> names = alives[index].GetUsableSkillNameList();

        return names;
    }

    public bool SetPlayerUnitAction(int index, Datas.BattleUnitAction action)
    {
        List<BattleUnitController> players = GetAliveUnits(Datas.BattleUnitParty.PlayerParty);
        players[index].Action = action;
        players[index].SetSelector(false);
        
        bool allPlayersSelectingActions = CheckAllPlayersSelectingActions();

        return allPlayersSelectingActions;
    }

    public void SetUsingSkill(Datas.BattleUnitParty party, int casterIndex, int skillIndex)
    {
        List<BattleUnitController> actors = GetAliveUnits(party);
        actors[casterIndex].SetUsingSkill(skillIndex);
    }

    public void SetTarget(Datas.BattleUnitParty actorParty, int actor, Datas.BattleUnitParty targetParty, int target)
    {
        List<BattleUnitController> actors = GetAliveUnits(actorParty);
        List<BattleUnitController> targets = GetAliveUnits(targetParty);

        targets[target].SetSelector(false);
        actors[actor].Targets.Add(targets[target]);
        battleUI.SetUnitMenuItemColorState(ItemInfo.ItemColorState.DeactivatedColor);
    }

    private void SetRandomTarget(BattleUnitController unit)
    {
        if (unit.Targets.Count > 0)
        {
            for (int i = 0; i < unit.Targets.Count; i++)
            {
                if (unit.Targets[i].IsFainted)
                {
                    unit.Targets[i] = GetRandomAliveUnit(unit.Targets[i].Party);
                }
            }
        }
    }

    public List<BattleUnitController> GetAliveUnits(Datas.BattleUnitParty party)
    {
        List<BattleUnitController> units = battleUnits.Where(u => u.Party == party && u.IsFainted == false).ToList();

        return units;
    }

    public BattleUnitController GetAliveUnit(Datas.BattleUnitParty party, int index)
    {
        List<BattleUnitController> units = battleUnits.Where(u => u.Party == party && u.Formation.Index == index && u.IsFainted == false).ToList();

        return units?[0];
    }

    private BattleUnitController GetRandomAliveUnit(Datas.BattleUnitParty party)
    {
        List<BattleUnitController> alives = GetAliveUnits(party);

        if (alives.Count == 0)
            return null;

        int random = Random.Range(0, alives.Count);

        return alives[random];
    }

    public void SetEnemyUnitBehavior()
    {
        List<BattleUnitController> enemys = GetAliveUnits(Datas.BattleUnitParty.EnemyParty);

        foreach (BattleUnitController unit in enemys)
        {
            if (Random.value * 100 <= 80f)
            {
                BattleUnitController player = GetRandomAliveUnit(Datas.BattleUnitParty.PlayerParty);

                unit.Action = Datas.BattleUnitAction.Attack;
                unit.Targets.Add(player);
            }
            else
            {
                unit.Action = Datas.BattleUnitAction.Defense;
            }
        }
    }

    private bool CheckAllPlayersSelectingActions()
    {
        List<BattleUnitController> alives = GetAliveUnits(Datas.BattleUnitParty.PlayerParty);

        if (alives.Where(u => u.Action == Datas.BattleUnitAction.None).Count() == 0)
        {
            return true;
        }

        return false;
    }

    public Datas.BattleCondition CheckBattleCondition()
    {
        List<BattleUnitController> players = GetAliveUnits(Datas.BattleUnitParty.PlayerParty);
        List<BattleUnitController> enemys = GetAliveUnits(Datas.BattleUnitParty.EnemyParty);

        Datas.BattleCondition battleResult;
        int playersCount = players.Count();
        int enemysCount = enemys.Count();

        if (playersCount == 0 && enemysCount == 0)
        {
            battleResult = Datas.BattleCondition.Draw;
        }
        else if (playersCount == 0)
        {
            battleResult = Datas.BattleCondition.Defeated;
        }
        else if (enemysCount == 0)
        {
            battleResult = Datas.BattleCondition.Victory;
        }
        else
        {
            // Not game over
            battleResult = Datas.BattleCondition.None;
        }

        return battleResult;
    }

    public void NavigateUnit(Datas.BattleUnitParty party, int select, ref int selected)
    {
        List<BattleUnitController> unit = GetAliveUnits(party);

        unit[selected].SetSelector(false);
        unit[select].SetSelector(true);

        selected = select;
    }
}
