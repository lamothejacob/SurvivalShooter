using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ammoPickupSpawner : MonoBehaviour
{

    [SerializeField] GameObject ammo;
    [SerializeField] Transform[] ammoSpawnLoc;
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] int spawnNumber;

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
        if(!isSpawning && playerInRange && numberSpawned < spawnNumber) 
        {
            StartCoroutine(spawnAmmo());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    IEnumerator spawnAmmo()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeBetweenSpawn);
        Instantiate(ammo, ammoSpawnLoc[Random.Range(0, ammoSpawnLoc.Length)].position, transform.rotation);
        isSpawning = false; 
    }
}
