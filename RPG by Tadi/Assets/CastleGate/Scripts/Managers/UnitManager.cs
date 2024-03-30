using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tadi.Utils;
using Tadi.Datas.Unit;
using Tadi.Datas.Weapon;
using Tadi.Datas.Combat;
using Tadi.Datas.BattleSystem;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private GameObject battleUnitPrefab;
    [SerializeField] private List<BattleUnitInfo> playersBattleInfo;
    [SerializeField] private List<BattleUnitInfo> enemysBattleInfo;

    private int battleUnitObjectPoolSize = 10;
    private List<GameObject> pooledbattleUnitObjects = new List<GameObject>();
    private List<BattleUnitController> battleUnits = new List<BattleUnitController>();
    
    public List<BattleUnitController> BattleUnits { get { return battleUnits; } }

    private void Awake()
    {
        
    }

    private void Start()
    {
        UnitData.Init();
        InitBattleUnitObjectPool();
    }

    public void InitBattleUnit(List<GameObject> unitPos)
    {
        BattleUnits.Clear();

        for (int i = 0; i < playersBattleInfo.Count; i++)
        {
            int front = i * Battle.PLAYER_UNIT_COUNT;
            int rear = i * Battle.PLAYER_UNIT_COUNT + 1;
            int unitPosIdx;
            int partnerPosIdx;

            if (playersBattleInfo[i].UnitPos == BattlePos.Front)
            {
                unitPosIdx = front;
                partnerPosIdx = rear;
            }
            else
            {
                unitPosIdx = rear;
                partnerPosIdx = front;
            }

            AddBattleUnit(playersBattleInfo[i], unitPos, unitPosIdx, partnerPosIdx);
        }

        for (int i = 0; i < enemysBattleInfo.Count; i++)
        {
            int front = i * Battle.PLAYER_UNIT_COUNT;
            int rear = i * Battle.PLAYER_UNIT_COUNT + 1;
            int unitPosIdx;
            int partnerPosIdx;

            if (enemysBattleInfo[i].UnitPos == BattlePos.Front)
            {
                unitPosIdx = front;
                partnerPosIdx = rear;
            }
            else
            {
                unitPosIdx = rear;
                partnerPosIdx = front;
            }

            AddBattleUnit(enemysBattleInfo[i], unitPos, unitPosIdx, partnerPosIdx);
        }
    }

    private void AddBattleUnit(BattleUnitInfo unitInfo, List<GameObject> unitPos, int unitPosIdx, int partnerPosIdx = 0)
    {
        GameObject unit = GetBattleUnitObject();
        unit.transform.SetParent(unitPos[unitPosIdx].transform);

        BattleUnitController unitController = unit.GetComponent<BattleUnitController>();
        unitController.Init(unitInfo.UnitType, unitInfo.Party, 1);
        battleUnits.Add(unitController);

        if (unitInfo.PartnerType != UnitType.None)
        {
            GameObject partner = GetBattleUnitObject();
            partner.transform.SetParent(unitPos[partnerPosIdx].transform);

            BattleUnitController partnerController = partner.GetComponent<BattleUnitController>();
            partnerController.Init(unitInfo.PartnerType, unitInfo.Party, 1);
            battleUnits.Add(partnerController);
        }
    }

    public void ReturnBattleUnit(GameObject parent)
    {

    }

    private void InitBattleUnitObjectPool()
    {
        for (int i = 0; i < battleUnitObjectPoolSize; i++)
        {
            // Instantiate objects and add them to the pool
            GameObject obj = Instantiate(battleUnitPrefab);
            obj.SetActive(false);
            pooledbattleUnitObjects.Add(obj);
        }
    }

    private GameObject GetBattleUnitObject()
    {
        // Search for an inactive object in the pool
        foreach (GameObject obj in pooledbattleUnitObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // If no inactive objects found, create a new one and add it to the pool
        GameObject newObj = Instantiate(battleUnitPrefab);
        pooledbattleUnitObjects.Add(newObj);
        return newObj;
    }

    // Return an object to the pool
    private void ReturnBattleUnitObject(GameObject obj)
    {
        obj.SetActive(false);
    }

    public UnitTypeData GetUnitTypeData(UnitType type)
    {
        UnitTypeData data;

        switch (type)
        {
            case UnitType.Knight:
                data = UnitData.Knight;
                break;
            case UnitType.Rogue:
                data = UnitData.Knight;
                break;
            case UnitType.Wizzard:
                data = UnitData.Knight;
                break;
            case UnitType.Orc:
                data = UnitData.Knight;
                break;
            case UnitType.SkeletonMage:
                data = UnitData.Knight;
                break;
            default:
                Debug.LogError("[UnitType] must to be set.");
                data = null;
                break;
        }

        return data;
    }
}
