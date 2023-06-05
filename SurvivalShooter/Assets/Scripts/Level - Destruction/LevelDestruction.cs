using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDestruction : MonoBehaviour
{
    [SerializeField] float timer;
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
        timer -= Time.deltaTime;

        if (totalHealth <= 0)
        {
            Destroy(gameObject);
            gameManager.instance.loseState();
        }
        else if (timer <= 0)
        {
            gameManager.instance.WinState(0);
        }
    }

    public void updateHealth(int amount)
    {
        totalHealth += amount;
    }
}
