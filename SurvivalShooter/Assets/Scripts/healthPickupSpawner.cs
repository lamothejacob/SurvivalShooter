using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class healthPickupSpawner : MonoBehaviour
{
    [SerializeField] GameObject pickup;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] int spawnCount;

    int numberSpawned;
    bool playerInRange;
    bool isSpawning; 
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && !isSpawning && numberSpawned < spawnCount)
        {
            StartCoroutine(spawn()); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true; 
        }
    }

    IEnumerator spawn()
    {
        isSpawning = true;

        Instantiate(pickup, spawnPoints[Random.Range(0, spawnPoints.Length)].position, transform.rotation); 
        yield return new WaitForSeconds(timeBetweenSpawn);
        isSpawning = false; 
    }
}
