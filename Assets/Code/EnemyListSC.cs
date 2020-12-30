using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[Serializable]
public class EnemyPricing
{
    public GameObject Enemy;
    public int Price;

    public EnemyPricing(GameObject enemy, int price)
    {
        Enemy = enemy;
        Price = price;
    }

    public EnemyPricing(){}
}


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CreateEnemyListSC", order = 1)]
public class EnemyListSC : ScriptableObject
{
    public List<EnemyPricing> Pricing = new List<EnemyPricing>();
}
