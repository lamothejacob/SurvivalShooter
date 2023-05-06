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
    [SerializeField] float avoidRadius;
    [SerializeField] float avoidRadiusVariance;

    private void Start()
    {
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
        agent.radius = Random.Range(avoidRadius - avoidRadiusVariance, avoidRadius + avoidRadiusVariance);
        //temp line
        StartCoroutine(death());
    }

    void Update()
    {
        agent.SetDestination(new Vector3(0,1,0));
        
    }

    //temp function
    IEnumerator death()
    {
        yield return new WaitForSeconds(10f);
        Death();
    }

    private void Death()
    {
        gameManager.instance.enemySpawnerScript.EnemyDecrement();
        Destroy(gameObject);
    }
}
