using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private ScenarioSC scenario;
    public ScenarioSC Scenario
    {
        get { return scenario; }
    }


    [SerializeField]
    private EnemyListSC enemyListSC;
    public EnemyListSC EnemyListSC
    {
        get { return enemyListSC; }
    }

    public float spawnCooldown = 60f;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(scenarioSC);
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(GameObject chosenPrefab, int n)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(chosenPrefab, transform.position, Quaternion.identity);
        }
        
    }

    IEnumerator SpawnEnemy() 
    {
        foreach (var wave in scenario.Waves)
        {
            Debug.Log(wave);
            var enemies = EnemyKnapsack.MakeVawe(wave.Budget,enemyListSC.Pricing);
            foreach (var enemy in enemies)
            {
                Spawn(enemy.Key, enemy.Value);
            }
            yield return new WaitForSeconds(wave.Time);
        }
    }
}
