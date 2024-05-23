using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tadi.Datas.BattleSystem;
using Tadi.Datas.Unit;
using Tadi.Utils;
using Tadi.UI.ScrollView;

public class BattleSystemUnits : MonoBehaviour
{
    //[SerializeField] private List<BattleUnitInfo> playersBattleUnitInfo;
    //[SerializeField] private List<BattleUnitInfo> enemysBattleUnitInfo;
    [SerializeField] private List<GameObject> playerUnitPos;
    [SerializeField] private List<GameObject> enemyUnitPos;

    private List<BattleUnitController> playerBattleUnits = new List<BattleUnitController>();
    private List<BattleUnitController> enemyBattleUnits = new List<BattleUnitController>();
    private Queue<BattleUnitController> turnOrder = new Queue<BattleUnitController>();

    public void InitBattleUnit(List<BattleUnitInfo> playersBattleUnitInfo, List<BattleUnitInfo> enemysBattleUnitInfo)
    {
        InitBattleUnits(playersBattleUnitInfo, ref playerBattleUnits, ref playerUnitPos);
        InitBattleUnits(enemysBattleUnitInfo, ref enemyBattleUnits, ref enemyUnitPos);
    }

    //private void AddBattleUnit(BattleUnitInfo unitInfo, List<GameObject> unitPos, int unitPosIdx, int partnerPosIdx = 0)
    private void InitBattleUnits(List<BattleUnitInfo> unitsInfo, ref List<BattleUnitController> battleUnits, ref List<GameObject> unitPos)
    {
        ReturnBattleUnits(ref battleUnits);

        for (int i = 0; i < unitsInfo.Count; i++)
        {
            int front = i * Battle.PLAYER_UNIT_COUNT;
            int rear = i * Battle.PLAYER_UNIT_COUNT + 1;
            int unitPosIdx;
            int partnerPosIdx;
            
            if (unitsInfo[i].UnitPos == BattlePos.Front)
            {
                unitPosIdx = front;
                partnerPosIdx = rear;
            }
            else
            {
                unitPosIdx = rear;
                partnerPosIdx = front;
            }

            GameObject unit = Managers.Ins.Unit.GetBattleUnitObject();
            unit.transform.SetParent(unitPos[unitPosIdx].transform);
            unit.transform.localPosition = Vector3.zero;

            BattleUnitController unitController = unit.GetComponent<BattleUnitController>();
            unitController.Init(unitsInfo[i].UnitType, unitsInfo[i].Party, unitsInfo[i].UnitLevel);
            battleUnits.Add(unitController);

            if (unitsInfo[i].PartnerType != UnitType.None)
            {
                GameObject partner = Managers.Ins.Unit.GetBattleUnitObject();
                partner.transform.SetParent(unitPos[partnerPosIdx].transform);
                partner.transform.localPosition = Vector3.zero;

                BattleUnitController partnerController = partner.GetComponent<BattleUnitController>();
                partnerController.Init(unitsInfo[i].PartnerType, unitsInfo[i].Party, unitsInfo[i].PartnerLevel);
                battleUnits.Add(partnerController);
            }
        }
    }

    public void ReturnBattleUnits(ref List<BattleUnitController> battleUnits)
    {
        foreach (BattleUnitController battleUnit in battleUnits)
        {
            Managers.Ins.Unit.ReturnBattleUnitObject(battleUnit.gameObject);
        }

        battleUnits.Clear();
    }

    public void ResetBattleUnits()
    {
        playerBattleUnits.ForEach(u => u.ResetBattleUnit());
        enemyBattleUnits.ForEach(u => u.ResetBattleUnit());
    }

    public void SetTurnOrder()
    {
        List<BattleUnitController> units =
            playerBattleUnits.Concat(enemyBattleUnits).OrderByDescending(
            u => u.Speed).OrderByDescending(
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
    
    public List<ItemInfo> GetAliveUnitInfoList(UnitParty party)
    {
        List<ItemInfo> itemInfoList = new List<ItemInfo>();
        List<BattleUnitController> alives = GetAliveUnits(party);

        foreach (BattleUnitController unit in alives)
        {
            itemInfoList.Add(new ItemInfo(unit.Name, MenuType.ActionMenu));
        }

        return itemInfoList;
    }

    public List<List<ItemInfo>> GetAliveUnitsSkillInfos(UnitParty party)
    {
        List<List<ItemInfo>> skillNameList = new List<List<ItemInfo>>();
        List<BattleUnitController> alives = GetAliveUnits(party);

        foreach (BattleUnitController unit in alives)
        {
            List<ItemInfo> list = unit.GetUsableSkillInfoList();
            skillNameList.Add(list);
        }

        return skillNameList;
    }

    public bool SetPlayerAction(int index, UnitAction action)
    {
        List<BattleUnitController> players = GetAliveUnits(UnitParty.PlayerParty);
        players[index].SetUnitAction(action);
        players[index].SetSelector(false);
        
        bool allPlayersSelectingActions = CheckAllPlayersSelectingActions();

        return allPlayersSelectingActions;
    }

    public string GetSkillDescription(UnitParty party, int actorIndex, int skillIndex)
    {
        List<BattleUnitController> actors = GetAliveUnits(party);
        string desc = actors[actorIndex].GetSkillDescription(skillIndex);

        return desc;
    }

    public void SetUsingSkill(UnitParty party, int actorIndex, int skillIndex)
    {
        List<BattleUnitController> actors = GetAliveUnits(party);
        actors[actorIndex].SetUsingSkill(skillIndex);
    }

    public void SetTarget(UnitParty actorParty, int actor, UnitParty targetParty, int target)
    {
        List<BattleUnitController> actors = GetAliveUnits(actorParty);
        List<BattleUnitController> targets = GetAliveUnits(targetParty);

        targets[target].SetSelector(false);
        actors[actor].TargetControllers.Add(targets[target]);
    }

    private void SetRandomTarget(BattleUnitController unit)
    {
        if (unit.TargetControllers.Count == 0)
            return;

        for (int i = 0; i < unit.TargetControllers.Count; i++)
        {
            if (unit.TargetControllers[i].IsFainted)
            {
                unit.TargetControllers[i] = GetRandomAliveUnit(unit.TargetControllers[i].Party);
            }
        }
    }

    public List<BattleUnitController> GetAliveUnits(UnitParty party)
    {
        List<BattleUnitController> units = null;

        switch (party)
        {
            case UnitParty.PlayerParty:
                units = playerBattleUnits.Where(u => u.IsFainted == false).ToList();
                break;
            case UnitParty.EnemyParty:
                units = enemyBattleUnits.Where(u => u.IsFainted == false).ToList();
                break;
        }

        return units;
    }

    private BattleUnitController GetRandomAliveUnit(UnitParty party)
    {
        List<BattleUnitController> alives = GetAliveUnits(party);

        if (alives.Count == 0)
            return null;

        int random = Random.Range(0, alives.Count);

        return alives[random];
    }

    public void SetEnemyUnitBehavior()
    {
        List<BattleUnitController> enemys = GetAliveUnits(UnitParty.EnemyParty);

        foreach (BattleUnitController unit in enemys)
        {
            if (Random.value * 100 <= 70f)
            {
                BattleUnitController player = GetRandomAliveUnit(UnitParty.PlayerParty);

                unit.SetUnitAction(UnitAction.Attack);
                unit.TargetControllers.Add(player);
            }
            else
            {
                unit.SetUnitAction(UnitAction.Defense);
            }
        }
    }

    private bool CheckAllPlayersSelectingActions()
    {
        List<BattleUnitController> alives = GetAliveUnits(UnitParty.PlayerParty);

        if (alives.Where(u => u.UnitAction == UnitAction.None).Count() == 0)
        {
            return true;
        }

        return false;
    }

    public BattleCondition CheckBattleCondition()
    {
        List<BattleUnitController> players = GetAliveUnits(UnitParty.PlayerParty);
        List<BattleUnitController> enemys = GetAliveUnits(UnitParty.EnemyParty);

        BattleCondition battleResult;
        int playersCount = players.Count();
        int enemysCount = enemys.Count();

        if (playersCount == 0 && enemysCount == 0)
        {
            battleResult = BattleCondition.Draw;
        }
        else if (playersCount == 0)
        {
            battleResult = BattleCondition.Lose;
        }
        else if (enemysCount == 0)
        {
            battleResult = BattleCondition.Win;
        }
        else
        {
            // Not game over
            battleResult = BattleCondition.None;
        }

        return battleResult;
    }

    public void NavigateUnit(UnitParty party, int select, ref int selected)
    {
        List<BattleUnitController> unit = GetAliveUnits(party);

        unit[selected].SetSelector(false);
        unit[select].SetSelector(true);

        selected = select;
    }
}
