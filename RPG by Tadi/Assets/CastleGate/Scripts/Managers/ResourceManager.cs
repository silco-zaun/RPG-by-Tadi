using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tadi.Datas.Res;
using Tadi.Datas.Unit;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResPrefabIndex
    {
        Bullet
    }

    // -- Variables --
    [SerializeField] private UnitRes unitRes;
    [SerializeField] private WeaponRes weaponRes;

    private GameObject[] objectPrefabs;
    private const int POOL_SIZE = 1;
    private int[] poolSizes;
    private List<List<GameObject>> objectPools = new List<List<GameObject>>();

    // -- Properties --
    //public GameObject[] BulletPrefabs { get { return objectPrefabs; } }
    public UnitRes UnitRes { get { return unitRes; } }
    public WeaponRes WeaponRes { get { return weaponRes; } }

    private void Awake()
    {

    }

    void Start()
    {
        InitAnim();
        InitObjectPool();
    }

    private void InitObjectPool()
    {
        objectPrefabs = new GameObject[]
        {
            weaponRes.Bullet
        };

        poolSizes = new int[objectPrefabs.Length];

        for (int i = 0; i < poolSizes.Length; i++)
        {
            poolSizes[i] = POOL_SIZE;
        }

        // Create object pools for each prefab
        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            List<GameObject> objectPool = new List<GameObject>();

            for (int j = 0; j < poolSizes[i]; j++)
            {
                GameObject obj = Instantiate(objectPrefabs[i]);
                obj.SetActive(false);
                objectPool.Add(obj);
            }

            objectPools.Add(objectPool);
        }
    }

    private void InitAnim()
    {
        Managers.Ins.Anim.AddAnimEvents(unitRes.KnightAnimator, UnitRes.KNIGHT_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);
        //Managers.Ins.Anim.AddAnimEvents(unitRes.RogueAnimator, UnitRes.ROGUE_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);
        Managers.Ins.Anim.AddAnimEvents(unitRes.WizzardAnimator, UnitRes.WIZZARD_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);
        Managers.Ins.Anim.AddAnimEvents(unitRes.OrcAnimator, UnitRes.ORC_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);
        Managers.Ins.Anim.AddAnimEvents(unitRes.SkeletonMageAnimator, UnitRes.SKELETON_MAGE_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);

        Managers.Ins.Anim.AddAnimEvents(weaponRes.BulletAnimator, WeaponRes.BULLET_EXPLOSION_CLIP_NAME, WeaponRes.BULLET_EXPLOSION_CLIP_NAME);
        
    }

    public GameObject GetObjectFromPool(int prefabIndex)
    {
        List<GameObject> objectPool = objectPools[prefabIndex];

        // Search for inactive object in the pool
        for (int i = 0; i < objectPool.Count; i++)
        {
            if (!objectPool[i].activeInHierarchy)
            {
                objectPool[i].SetActive(true);

                return objectPool[i];
            }
        }

        // If no inactive object is found, create a new one
        GameObject newObj = Instantiate(objectPrefabs[prefabIndex]);
        objectPool.Add(newObj);
        return newObj;
    }

    // You may also want a method to return objects back to the pool if needed
    public void ReturnObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
