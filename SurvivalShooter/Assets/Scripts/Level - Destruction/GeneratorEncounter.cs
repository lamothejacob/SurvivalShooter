using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Android;

public class GeneratorEncounter : MonoBehaviour
{
    [SerializeField] List<GameObject> generators;


    public int generatorsLeft;

    // Start is called before the first frame update
    void Start()
    {
        generatorsLeft = transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (generators[0] != null)
        {
            generators[0].GetComponent<Light>().enabled = true;
            generators[0].GetComponent<CapsuleCollider>().enabled = true;
        }
        else if (gameManager.instance.GetEnemiesKilled() >= 40 && generators[1] != null)
        {
            generators[1].GetComponent<Light>().enabled = true;
            generators[1].GetComponent<CapsuleCollider>().enabled = true;
        }
        else if (gameManager.instance.GetEnemiesKilled() >= 70 && generators[2] != null)
        {
            generators[2].GetComponent<Light>().enabled = true;
            generators[2].GetComponent<CapsuleCollider>().enabled = true;
        }
    }
}
