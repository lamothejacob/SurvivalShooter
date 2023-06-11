using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    [Header("----- Enemy -----")]
    [SerializeField] GameObject[] enemy;
    [SerializeField] Transform[] enemySpawnLoc;

    [Header("----- Spawn Parameters -----")]
    [SerializeField] int spawnNumber;
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] bool continuousSpawn;

    private bool isSpawning;
    private int numberSpawned;
    private bool playerInRange;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (continuousSpawn)
            numberSpawned = transform.childCount;

        if (!isSpawning && playerInRange && numberSpawned < spawnNumber)
        {
            StartCoroutine(spawnEnemy());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    IEnumerator spawnEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeBetweenSpawn);
        GameObject newEnemy = Instantiate(enemy[Random.Range(0, enemy.Length)], enemySpawnLoc[Random.Range(0, enemySpawnLoc.Length)].position, transform.rotation);
        newEnemy.transform.parent = transform;
        numberSpawned++;
        gameManager.instance.AddEnemiesAlive(1);
        isSpawning = false;
    }
}
