using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{
    [Header("----- Components -----")]
    [SerializeField] Renderer color;
    [SerializeField] NavMeshAgent agent;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;

    [Header("----- Enemy Weapon -----")]
    [Range(2, 300)] [SerializeField] int shootDist;
    [Range(0.1f, 3)] [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;

    [Header("----- Nav Mesh Stats -----")]
    [SerializeField] float speed;
    [SerializeField] float speedVariance;
    [SerializeField] float avoidRadius;
    [SerializeField] float avoidRadiusVariance;

    Vector3 playerDir;
    bool isShooting;
    Color colorOrig;

    private void Start()
    {
        colorOrig = color.material.color;
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
        agent.radius = Random.Range(avoidRadius - avoidRadiusVariance, avoidRadius + avoidRadiusVariance);
        HP = gameManager.instance.enemySpawnerScript.GetCurrentWave().getHealth();
    }

    void Update()
    {
        playerDir = gameManager.instance.player.transform.position - transform.position;

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            facePLayer();
        }

        agent.SetDestination(gameManager.instance.player.transform.position);
        if (!isShooting)
            StartCoroutine(shoot());
    }

    private void Death()
    {
        gameManager.instance.enemySpawnerScript.EnemyDecrement();
        Destroy(gameObject);
    }

    IEnumerator shoot()
    {
        isShooting = true;

        Instantiate(bullet, transform.position, transform.rotation);
        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    void facePLayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        StartCoroutine(flashColor());

        if (HP <= 0)
        {
            Death();
        }
    }
    IEnumerator flashColor()
    {
        color.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        color.material.color = colorOrig;
    }
}
