using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tadi.Datas.Unit;

public class UnitManager : MonoBehaviour
{
    [SerializeField] private GameObject battleUnitPrefab;

    private GameObject battleUnitPool;
    private int battleUnitObjectPoolSize = 10;
    private List<GameObject> pooledbattleUnitObjects = new List<GameObject>();

    private void Awake()
    {
        battleUnitPool = GameObject.Find("BattleUnitPool");
    }

    private void Start()
    {
        //UnitData.Init();
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < battleUnitObjectPoolSize; i++)
        {
            // Instantiate objects and add them to the pool
            GameObject obj = Instantiate(battleUnitPrefab, battleUnitPool.transform);
            obj.SetActive(false);
            pooledbattleUnitObjects.Add(obj);
        }
    }

    public GameObject GetBattleUnitObject()
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
    public void ReturnBattleUnitObject(GameObject obj)
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
                data = UnitData.Rogue;
                break;
            case UnitType.Wizzard:
                data = UnitData.Wizzard;
                break;
            case UnitType.Orc:
                data = UnitData.Orc;
                break;
            case UnitType.SkeletonMage:
                data = UnitData.SkeletonMage;
                break;
            case UnitType.LizardMan:
                data = UnitData.LizardMan;
                break;
            case UnitType.TurtleKing:
                data = UnitData.TurtleKing;
                break;
            default:
                Debug.LogError("[UnitType] must to be set.");
                data = null;
                break;
        }

        return data;
    }
}
