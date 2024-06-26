
using System.Collections.Generic;
using Tadi.Datas.Res;
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

    public void Init()
    {
        InitAnim();
        InitObjectPool();
    }

    private void InitAnim()
    {
        Managers.Ins.Anim.AddAnimEvents(unitRes.KnightAnimator, UnitRes.KNIGHT_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);
        //Managers.Ins.Anim.AddAnimEvents(unitRes.RogueAnimator, UnitAnimRes.ROGUE_ATTACK_CLIP_NAME, UnitAnimRes.ATTACK_CLIP_KEYWORD);
        Managers.Ins.Anim.AddAnimEvents(unitRes.WizzardAnimator, UnitRes.WIZZARD_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);
        Managers.Ins.Anim.AddAnimEvents(unitRes.OrcAnimator, UnitRes.ORC_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);
        Managers.Ins.Anim.AddAnimEvents(unitRes.SkeletonMageAnimator, UnitRes.SKELETON_MAGE_ATTACK_CLIP_NAME, UnitRes.ATTACK_CLIP_KEYWORD);

        Managers.Ins.Anim.AddAnimEvents(weaponRes.MagicBolt, WeaponRes.BULLET_EXPLOSION_CLIP_NAME, WeaponRes.BULLET_EXPLOSION_CLIP_NAME);
        Managers.Ins.Anim.AddAnimEvents(weaponRes.DarkBolt, WeaponRes.BULLET_EXPLOSION_CLIP_NAME, WeaponRes.BULLET_EXPLOSION_CLIP_NAME);
        Managers.Ins.Anim.AddAnimEvents(weaponRes.FireBall, WeaponRes.BULLET_EXPLOSION_CLIP_NAME, WeaponRes.BULLET_EXPLOSION_CLIP_NAME);

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
                GameObject obj = Instantiate(objectPrefabs[i], ObjectPoolController.Ins.transform);
                obj.SetActive(false);
                objectPool.Add(obj);
            }
            
            objectPools.Add(objectPool);
        }
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
