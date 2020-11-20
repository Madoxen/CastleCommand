﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private EnemyListSC enemyListSC;
    public EnemyListSC EnemyListSC
    {
        get { return enemyListSC; }
    }

    public float spawnCooldown = 2f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy() 
    {
        while (true)
        {
            //Choose the prefab
            GameObject chosenPrefab = enemyListSC.prefabList[Random.Range(0, enemyListSC.prefabList.Count)];
            Instantiate(chosenPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(spawnCooldown);
        }
        
    }
        


}
