using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System;

public class EnemyKnapsack
{
    public static Dictionary<GameObject, int> MakeVawe(int budget, List<EnemyPricing> pricing)
    { 
    var random = new System.Random();

        Dictionary<GameObject, int> EnemiesCount = pricing.ToDictionary(x => x.Enemy, x => 0);

        List<EnemyPricing> availableEnemies = new List<EnemyPricing>(pricing);

        availableEnemies = (from e in availableEnemies where e.Price < budget select e).ToList();
        while(availableEnemies.Any())
        {
            int index = random.Next(availableEnemies.Count);

            var randomEnemy = availableEnemies[index];
            EnemiesCount[randomEnemy.Enemy]++;
            budget -= randomEnemy.Price;
            availableEnemies = (from e in availableEnemies where e.Price < budget select e).ToList();
        }
        return EnemiesCount;
    }
}