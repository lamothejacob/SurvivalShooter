using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] NavMeshAgent agent;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;

    [Header("----- Nav Mesh Stats -----")]
    [SerializeField] float speed;
    [SerializeField] float speedVariance;

    private void Start()
    {
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
    }

    void Update()
    {
        agent.SetDestination(new Vector3(0,1,0));
    }
}
