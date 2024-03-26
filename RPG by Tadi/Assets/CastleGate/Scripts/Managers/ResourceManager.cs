using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourcePrefabIndex
    {
        Bullet
    }

    // -- Variables --
    [SerializeField] private GameObject[] objectPrefabs;

    private const int POOL_SIZE = 1;
    private int[] poolSizes;
    private List<List<GameObject>> objectPools = new List<List<GameObject>>();

    // -- Properties --
    //public GameObject[] BulletPrefabs { get { return objectPrefabs; } }

    private void Awake()
    {
        bool check = CheckList();

        if (!check)
            return;
    }

    private bool CheckList()
    {
        int enumCount = Enum.GetValues(typeof(ResourcePrefabIndex)).Length;

        if (objectPrefabs.Length != enumCount)
        {
            Debug.LogError("Resource prefab must to be set.");
            return false;
        }

        for (int i = 0; i < objectPrefabs.Length; i++)
        {
            // Get the prefab asset path of the nearest instance root
            string prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(objectPrefabs[i]);
            string prefabName;

            if (!string.IsNullOrEmpty(prefabPath))
            {
                // Extract the prefab name from the path
                prefabName = System.IO.Path.GetFileNameWithoutExtension(prefabPath);
            }
            else
            {
                Debug.LogError("The GameObject is not a prefab instance.");
                return false;
            }

            string name = ((ResourcePrefabIndex)i).ToString();
            bool check = prefabName.Equals(name);

            if (!check)
            {
                Debug.LogError($"Prefab name and checked name are not matched.\nPrefab name : {prefabName}\nChecked name : {name}");
                return false;
            }
        }

        return true;
    }

    void Start()
    {
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
