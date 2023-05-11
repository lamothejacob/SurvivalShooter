using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{
    [Header("----- Components -----")]
    [SerializeField] Renderer color;
    [SerializeField] NavMeshAgent agent;

    [SerializeField] Transform headPos;

    [Header("----- Enemy Stats -----")]
    [SerializeField] int HP;
    [SerializeField] float playerFaceSpeed;
    [SerializeField] int ViewCone;

    [Header("----- Enemy Weapon -----")]
    [Range(2, 300)] [SerializeField] int shootDist;
    [Range(0.1f, 3)] [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] int shootAngle;

    [Header("----- Nav Mesh Stats -----")]
    [SerializeField] float speed;
    [SerializeField] float speedVariance;
    [SerializeField] float avoidRadius;
    [SerializeField] float avoidRadiusVariance;

    Vector3 playerDir;
    bool isShooting;
    Color colorOrig;
    float angleToPlayer;

    private void Start()
    {
        colorOrig = color.material.color;
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
        agent.radius = Random.Range(avoidRadius - avoidRadiusVariance, avoidRadius + avoidRadiusVariance);
        HP = gameManager.instance.enemySpawnerScript.GetCurrentWave().getHealth();
    }

    void Update()
    {

        agent.SetDestination(gameManager.instance.player.transform.position);

        if (canSeePlayer())
        {

        }
    }

    bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        Debug.DrawRay(headPos.position, playerDir);
        Debug.Log(angleToPlayer);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= ViewCone)
            {
                agent.SetDestination(gameManager.instance.player.transform.position);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    facePLayer();
                }

                if (!isShooting && angleToPlayer <= shootAngle)
                    StartCoroutine(shoot());

                return true;
            }
        }
        return false;
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
