using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class PoolObjectInstructions 
{
    public PooledObjectType pooledObjectType;
    public GameObject prefab;
    public int amount;

}
[Serializable]
public enum PooledObjectType
{
    None,
    Enemy,
    Bullet,
    Blood,
    Sparks,
    WoodChips,
    VegetationChips
}
