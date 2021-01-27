using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;




public static class EnumerableExtension
{
    public static T PickRandom<T>(this IEnumerable<T> source)
    {
        return source.PickRandom(1).Single();
    }

    public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
    {
        return source.Shuffle().Take(count);
    }

    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return source.OrderBy(x => Guid.NewGuid());
    }
}


public class EnemySpawner : MonoBehaviour
{
    private static PlayerResources instance = null;
    public static PlayerResources Instance
    {
        get { return instance; }
    }



    [SerializeField]
    private ScenarioSC scenarioSC;
    public ScenarioSC ScenarioSC
    {
        get { return scenarioSC; }
    }


    [SerializeField]
    private EnemyListSC enemyListSC;
    public EnemyListSC EnemyListSC
    {
        get { return enemyListSC; }
    }

    public List<GameObject> SpawnerList;

    public GameObject TimerLabel;

    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
         StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        timeLeft -= Time.deltaTime;
        TimeSpan t = TimeSpan.FromSeconds(timeLeft);
        TimerLabel.GetComponent<Text>().text = string.Format("{0:D2}:{1:D2}",
        t.Minutes,
        t.Seconds);
    }

    void Spawn(GameObject chosenPrefab, int n, Transform _transform)
    {
        for (int i = 0; i < n; i++)
        {
            Instantiate(chosenPrefab, _transform.position, Quaternion.identity);
        }
        
    }

    IEnumerator SpawnEnemy() 
    {
        foreach (var wave in scenarioSC.Waves)
        {
            Dictionary<GameObject, int> enemies = EnemyKnapsack.MakeVawe(wave.Budget,enemyListSC.Pricing);
            foreach (var enemy in enemies)
            {
                Spawn(enemy.Key,
                 enemy.Value,
                 SpawnerList.PickRandom().transform);
            }

            timeLeft=wave.Time;

            yield return new WaitForSeconds(wave.Time);
        }
    }
}
