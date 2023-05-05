using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Instance Variables
    [SerializeField] List<WaveEditorSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    private WaveEditorSO currentWave;
    private bool isLooping = true;
    #endregion

    private void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }
    public WaveEditorSO GetCurrentWave()
    {
        return currentWave;
    }

    private IEnumerator SpawnEnemyWaves()
    {

        do
        {
            foreach (WaveEditorSO wave in waveConfigs)
            {
                currentWave = wave;
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    int spawnPos = Random.Range(0, currentWave.GetSpawnPointsCount());
                    Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetSpawnPosition(spawnPos).position, Quaternion.identity, transform);

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);

    }
}
