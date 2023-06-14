using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauntletEnemySpawner : MonoBehaviour
{
    [Header("=== Enemies ===")]
    [SerializeField] GameObject seekerEnemy;
    [SerializeField] GameObject robotSeeker;

    [Header("=== Spawn Points ===")]
    [SerializeField] GameObject[] spawnPoints;

    bool bHasBeenPassed;

    // Start is called before the first frame update
    void Start()
    {
        bHasBeenPassed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enemy Spawn Trigger has been succesfully entered. "); 
        //if(bHasBeenPassed == false) 
        //{
            //bHasBeenPassed = true;
            if (other.CompareTag("Player"))
            {
                foreach (GameObject t in spawnPoints)
                {
                    Vector3 spawnLoc = t.transform.position;
                    Quaternion spawnRot = t.transform.rotation;

                    if (t.CompareTag("SeekerSpawn"))
                    {
                        Instantiate(seekerEnemy, spawnLoc, spawnRot);
                        Debug.Log("A Seeker is born"); 
                    }
                    else if (t.CompareTag("DroneSpawn"))
                    {
                        Instantiate(robotSeeker, t.transform);
                        Debug.Log("A Seeker Drone is born");
                    }
                }
            }
        //}
        
        //Destroy(gameObject);
    }

}
