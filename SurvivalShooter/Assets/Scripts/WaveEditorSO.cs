using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "ScriptableObjects/Wave Config")]
public class WaveEditorSO : ScriptableObject
{

    #region Instance Variables
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] int health; 
    [SerializeField] float timeBetweenEnemySpawns;
    [SerializeField] float spawnTimeVariance;
    [SerializeField] float minimumSpawnTime;

    #endregion

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public int GetSpawnPointsCount()
    {
        return spawnPoints.Count;
    }

    public Transform GetSpawnPosition(int i)
    {
        return spawnPoints[i];
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }

    public int getHealth()
    {
        return health;
    }
}
