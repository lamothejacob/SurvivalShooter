using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBoss : Enemy
{
    [Header("Components")]
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] Transform[] shootPos;
    [SerializeField] Transform BeamPos;

    [Header("Enemy Stats")]
    [SerializeField] float playerFaceSpeed;
    [SerializeField] int ViewCone;
    [SerializeField] float animTransSpeed;

    [Header("----- Enemy Weapon -----")]
    [Range(2, 300)] [SerializeField] int shootDist;
    [Range(0.1f, 3)] [SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject beam;
    [SerializeField] int shootAngle;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject bomb;

    [Header("----- Nav Mesh Stats -----")]
    [SerializeField] float speed;
    [SerializeField] float speedVariance;
    [SerializeField] float avoidRadius;
    [SerializeField] float avoidRadiusVariance;

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] gunshots;
    [SerializeField] float test;

    [Header("Abilities")]
    [SerializeField] float beamRate;
    [SerializeField] float bombRate;
    
    Vector3 playerDir;
    bool isShooting;
    bool stepping;
    float angleToPlayer;
    int beamCounter = 0;
    int bombCounter = 0;
    int shootinghand= 0;

    private void Start()
    {
        HP = HPMax;
        colorOrig = color.material.color;
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
        agent.radius = Random.Range(avoidRadius - avoidRadiusVariance, avoidRadius + avoidRadiusVariance);
        audioPlayer = GetComponent<AudioSource>();
        isBoss = true;
    }

    void Update()
    {
        if (!isDead)
        {
            agent.SetDestination(gameManager.instance.player.transform.position);

            if (canSeePlayer())
            {

            }
        }

        if (isDead)
        {
            DeathExplosion();
        }
    }

    bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        Debug.DrawRay(headPos.position, playerDir);

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
                {
                    

                    if (beamCounter >= 10)
                    {
                        StartCoroutine(Beam());
                        beamCounter = 0;
                    }
                    else if (bombCounter >= 13)
                    {
                        StartCoroutine(Bombs());
                        bombCounter = 0;
                    }
                    else
                    {
                        StartCoroutine(shoot());
                        beamCounter++;
                        bombCounter++;
                    }
                }

                return true;
            }


        }
        return false;
    }

    IEnumerator shoot()
    {
        isShooting = true;
        if (shootinghand % 2 == 0)
        {
            Instantiate(bullet, shootPos[0].position, Quaternion.LookRotation(gameManager.instance.player.transform.position + new Vector3(0, test, 0) - shootPos[0].position));
        }
        else
        {
            Instantiate(bullet, shootPos[1].position, Quaternion.LookRotation(gameManager.instance.player.transform.position + new Vector3(0, test, 0) - shootPos[1].position));
        }
        shootinghand++;
        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    void DeathExplosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(explosion, transform.position + new Vector3(0,10,0), transform.rotation);
        Instantiate(explosion, transform.position + new Vector3(0, -10, 0), transform.rotation);
    }

    void facePLayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    private IEnumerator Beam()
    {
        isShooting = true;
        int num = Random.Range(0, shootPos.Length - 1);
        Transform shootingspot = gameManager.instance.player.transform;

        for(int i = 0; i < 3; i++)
        {
            StartCoroutine(flashColor());
            yield return new WaitForSeconds(0.75f);
        }

        for (float i = 0f; i < 25f; i++)
        {
            Instantiate(beam, BeamPos.position, Quaternion.LookRotation(gameManager.instance.player.transform.position + new Vector3(0, (i/5f), 0) - BeamPos.position));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(beamRate);
        isShooting = false;
    }

    private IEnumerator Bombs()
    {
        isShooting = true;

        for (int i = 0; i < 3; i++)
        {
            StartCoroutine(flashColor());
            yield return new WaitForSeconds(0.75f);
        }

        for (float i = 0; i < 10; i++)
        {
            Instantiate(bomb, transform.position, Quaternion.LookRotation(new Vector3(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360))));
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(bombRate);
        isShooting = false;
    }


    IEnumerator flashColor()
    {
        color.material.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        color.material.color = colorOrig;
    }
}
