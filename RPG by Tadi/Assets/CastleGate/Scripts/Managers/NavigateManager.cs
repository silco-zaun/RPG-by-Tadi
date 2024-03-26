using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static BattleDataManager;

public class NavigateManager : MonoBehaviour
{
    public void NavigateTargetUnitStart(BattleUnitParty party, ref int selected)
    {
        for (int i = 0; i < PARTY_UNIT_COUNT; i++)
        {
            BattleUnitController unit = GetSelectedFormationUnit(party, i);

            if (unit != null)
            {
                selected = i;
                unit.SetSelector(true);

                return;
            }
        }
    }

    public void NavigateUnit(BattleUnitParty party, Vector2 vector, ref int selected)
    {
        int select = selected;

        BattleUnitController unit = GetNavigateUnit(party, vector, ref select);

        if (unit != null)
        {
            unit.SetSelector(true);
            unit = GetSelectedFormationUnit(party, selected);
            unit.SetSelector(false);
        }

        selected = select;
    }

    private BattleUnitController GetNavigateUnit(BattleUnitParty party, Vector2 vector, ref int selected)
    {
        // 4 5
        // 0 1
        // 2 3
        Vector2 positive;
        Vector2 negative;
        int[] lineCount = new int[2];

        if (vector.x != 0)
        {
            lineCount[0] = HORIZONTAL_LINE_UNIT_COUNT;
            lineCount[1] = VERTICAL_LINE_UNIT_COUNT;
            positive = new Vector2(0, 1);
            negative = new Vector2(0, -1);
        }
        else if (vector.y != 0)
        {
            lineCount[0] = VERTICAL_LINE_UNIT_COUNT;
            lineCount[1] = HORIZONTAL_LINE_UNIT_COUNT;
            positive = new Vector2(1, 0);
            negative = new Vector2(-1, 0);
        }
        else
        {
            Debug.LogError($"An error occurred: Variable 'vector' value can't be {vector}");

            return null;
        }

        // Get initial formationIndex
        selected = GetNavigateUnitIndex(party, vector, selected);

        // Line search
        BattleUnitController unit = GetNavigateLineUnit(party, vector, selected, lineCount[0] - 1);

        if (unit != null)
            return unit;

        // Rest lines search
        int startp = selected;
        int startn = selected;

        // Get initial formationIndex

        for (int i = 0; i < lineCount[1] / 2; i++)
        {
            startp = GetNavigateUnitIndex(party, positive, startp);
            startn = GetNavigateUnitIndex(party, negative, startn);

            for (int j = 0; j < lineCount[0] - 1; i++)
            {
                selected = startp;
                unit = GetSelectedFormationUnit(party, selected);

                if (unit != null)
                    return unit;
                else
                    startp = GetNavigateUnitIndex(party, vector, selected);

                if (startp == startn)
                    continue;

                selected = startn;
                unit = GetSelectedFormationUnit(party, selected);

                if (unit != null)
                    return unit;
                else
                    startn = GetNavigateUnitIndex(party, vector, selected);
            }
        }

        return null;
    }

    private BattleUnitController GetNavigateLineUnit(BattleUnitParty party, Vector2 vector, int start, int count)
    {
        int iter = start;

        for (int i = 0; i < count; i++)
        {
            iter = GetNavigateUnitIndex(party, vector, iter);
            BattleUnitController unit = GetSelectedFormationUnit(party, iter);

            if (unit != null)
            {
                return unit;
            }
        }

        return null;
    }

    private int GetNavigateUnitIndex(BattleUnitParty party, Vector2 vector, int selected)
    {
        // 0 1
        // 2 3
        // 4 5
        if (vector.x < 0)
        {
            selected =
                (selected / HORIZONTAL_LINE_UNIT_COUNT * HORIZONTAL_LINE_UNIT_COUNT) +
                ((selected + HORIZONTAL_LINE_UNIT_COUNT - 1) % HORIZONTAL_LINE_UNIT_COUNT);
        }
        else if (vector.x > 0)
        {
            selected =
                (selected / HORIZONTAL_LINE_UNIT_COUNT * HORIZONTAL_LINE_UNIT_COUNT) +
                ((selected + 1) % HORIZONTAL_LINE_UNIT_COUNT);
        }
        else if (vector.y < 0)
        {
            selected = (selected + HORIZONTAL_LINE_UNIT_COUNT) % PARTY_UNIT_COUNT;
        }
        else if (vector.y > 0)
        {
            selected = (selected + PARTY_UNIT_COUNT - HORIZONTAL_LINE_UNIT_COUNT) % PARTY_UNIT_COUNT;
        }

        return selected;
    }

    private BattleUnitController GetSelectedFormationUnit(BattleUnitParty party, int selected)
    {
        int index = 0;

        if (party == BattleUnitParty.PlayerParty)
            index = selected;
        else if (party == BattleUnitParty.EnemyParty)
            index = selected + 6;

        List<BattleUnitController> units = null;
        //List<BattleUnitController> units = battleUnits.Where(u => u.IsFainted == false && u.Formation == formations[formationIndex]).ToList();

        if (units.Count() > 0)
            return units[0];

        return null;
    }
}
