using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    #region Instance Variables
    [SerializeField] List<WaveEditorSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 1f;
    [SerializeField] int HpMod;
    private WaveEditorSO currentWave;
    private bool isLooping = true;
    private int enemyCounter = 0;
    private int waveNumber = 1;
    #endregion

    private void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }
    public WaveEditorSO GetCurrentWave()
    {
        return currentWave;
    }

    public int GetWaveNumber()
    {
        return waveNumber;
    }

    public int GetWaveAmount()
    {
        return waveConfigs.Count;
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
                    GameObject newEnemy = Instantiate(currentWave.GetEnemyPrefab(i), currentWave.GetSpawnPosition(spawnPos).position, Quaternion.identity, transform);
                    newEnemy.GetComponent<Enemy>().SetHP(HpMod);
                    EnemyIncrement();

                    yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
                }
                yield return new WaitUntil(() => EnemiesLeft() == 0);
                waveNumber++;
                HpMod += HpMod;
                gameManager.instance.updateGameGoal();
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);

    }

    private void EnemyIncrement()
    {
        enemyCounter++;
    }

    public void EnemyDecrement()
    {
        enemyCounter--;
    }

    public int EnemiesLeft()
    {
        return enemyCounter;
    }
}
