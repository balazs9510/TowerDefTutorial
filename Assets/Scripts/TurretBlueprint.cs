
using System;
using UnityEngine;

[Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public int cost;

    public GameObject upgradedPrefab;
    public int upgradeCost;

    public int SellAmount { get { return (int)(cost * .6); } }
}
