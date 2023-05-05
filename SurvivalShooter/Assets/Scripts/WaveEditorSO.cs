using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Wave Config", fileName = "ScriptableObjects/Wave Config")]
public class WaveEditorSO : ScriptableObject
{

    #region Instance Variables
    [SerializeField] Transform spawnPoint;
    [SerializeField] List<GameObject> enemyPrefabs;

    [SerializeField] float timeBetweenEnemySpawns = 1f;
    [SerializeField] float spawnTimeVariance = 0f;
    [SerializeField] float minimumSpawnTime = 1f;

    #endregion

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public Transform GetSpawnPosition()
    {
        return spawnPoint;
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
}
