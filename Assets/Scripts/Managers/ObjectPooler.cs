using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ObjectPooler : Singleton<ObjectPooler>
{
    [SerializeField] List<PoolObjectInstructions> poolInstructions;
    Dictionary<PooledObjectType, List<GameObject>> poolDict = new Dictionary<PooledObjectType, List<GameObject>>();

    private void Awake()
    {
        CreatePools();
    }

    public GameObject GetFromPoolAtPosition(PooledObjectType pooledObjectType, Vector3 position,bool disabled = false)
    {
        GameObject pickedObject = null;
        foreach (var pool in poolDict)
        {
            if (pool.Key == pooledObjectType)
            {
                for (int i = 0; i < pool.Value.Count; i++)
                {
                    if (!pool.Value[i].activeInHierarchy)
                    {
                        pickedObject = pool.Value[i];
                        break;
                    }
                }
            }
        }
        pickedObject ??= CreateAdditionalObject(pooledObjectType);
        pickedObject.transform.position = position;
        if(!disabled) pickedObject.SetActive(true);
        return pickedObject;
    }

    private GameObject CreateAdditionalObject(PooledObjectType pooledObjectType)
    {
        foreach (var poolInstruction in poolInstructions)
        {
            if (poolInstruction.pooledObjectType == pooledObjectType)
            {
                GameObject temp = Instantiate(poolInstruction.prefab, transform);
                temp.SetActive(false);
                foreach (var pool in poolDict)
                {
                    if (pool.Key == pooledObjectType)
                    {
                        pool.Value.Add(temp);
                        return temp;
                    }
                }
            }
        }
        return null;
    }

    public void ReturnToPool(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private void CreatePools()
    {
        foreach (var poolInstruction in poolInstructions)
        {
            List<GameObject> pool = new List<GameObject>();
            for (int i = 0; i < poolInstruction.amount; i++)
            {
                GameObject temp = Instantiate(poolInstruction.prefab, transform);
                temp.SetActive(false);
                pool.Add(temp);
            }
            poolDict.Add(poolInstruction.pooledObjectType, pool);
        }
    }

    public void ResetPools()
    {
        foreach (var pool in poolDict)
        {
            foreach (var item in pool.Value)
            {
                item.SetActive(false);
            }
        }
    }



}
