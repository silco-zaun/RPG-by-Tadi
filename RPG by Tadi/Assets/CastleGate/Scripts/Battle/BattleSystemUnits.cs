using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Tadi.Datas.BattleSystem;
using Tadi.Datas.Unit;

public class BattleSystemUnits : MonoBehaviour
{
    private List<BattleUnitController> battleUnits = new List<BattleUnitController>();
    Queue<BattleUnitController> turnOrder = new Queue<BattleUnitController>();

    public void ResetBattleUnits()
    {
        battleUnits.ForEach(u => u.ResetBattleUnit());
    }

    public void SetTurnOrder()
    {
        List<BattleUnitController> units =
            battleUnits.OrderByDescending(
            u => u.UnitController.Speed).OrderByDescending(
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
    
    public List<string> GetAliveUnitsNames(UnitParty party)
    {
        List<string> names = new List<string>();
        List<BattleUnitController> alives = GetAliveUnits(party);

        foreach (BattleUnitController unit in alives)
        {
            string name = "Temp";
        }

        return names;
    }

    public List<string> GetAliveUnitsSkillNames(UnitParty party, int index)
    {
        List<BattleUnitController> alives = GetAliveUnits(party);
        List<string> names = alives[index].GetUsableSkillNameList();

        return names;
    }

    public bool SetPlayerUnitAction(int index, UnitAction action)
    {
        List<BattleUnitController> players = GetAliveUnits(UnitParty.PlayerParty);
        players[index].Action = action;
        players[index].SetSelector(false);
        
        bool allPlayersSelectingActions = CheckAllPlayersSelectingActions();

        return allPlayersSelectingActions;
    }

    public CombatSkill_ GetSkillInfo(UnitParty party, int actorIndex, int skillIndex)
    {
        List<BattleUnitController> actors = GetAliveUnits(party);
        CombatSkill_ skill = actors[actorIndex].GetSkillInfo(skillIndex);

        return skill;
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
        actors[actor].Targets.Add(targets[target]);
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

    public List<BattleUnitController> GetAliveUnits(UnitParty party)
    {
        List<BattleUnitController> units = battleUnits.Where(u => u.Party == party && u.IsFainted == false).ToList();

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
            if (Random.value * 100 <= 80f)
            {
                BattleUnitController player = GetRandomAliveUnit(UnitParty.PlayerParty);

                unit.Action = UnitAction.Attack;
                unit.Targets.Add(player);
            }
            else
            {
                unit.Action = UnitAction.Defense;
            }
        }
    }

    private bool CheckAllPlayersSelectingActions()
    {
        List<BattleUnitController> alives = GetAliveUnits(UnitParty.PlayerParty);

        if (alives.Where(u => u.Action == UnitAction.None).Count() == 0)
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
            battleResult = BattleCondition.Defeated;
        }
        else if (enemysCount == 0)
        {
            battleResult = BattleCondition.Victory;
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
