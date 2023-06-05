using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDestruction : MonoBehaviour
{
    [SerializeField] GameObject[] weakPoint;

    public int totalHealth;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weakPoint.Length; i++)
        {
            totalHealth += weakPoint[i].GetComponent<WeakPoint>().getHP();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateHealth(int amount)
    {
        totalHealth += amount;
    }
}
