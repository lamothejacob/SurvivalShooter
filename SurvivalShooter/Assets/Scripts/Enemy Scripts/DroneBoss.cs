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
    
    Vector3 playerDir;
    bool isShooting;
    bool stepping;
    float angleToPlayer;
    int beamCounter = 0;

    private void Start()
    {
        HP = HPMax;
        colorOrig = color.material.color;
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
        agent.radius = Random.Range(avoidRadius - avoidRadiusVariance, avoidRadius + avoidRadiusVariance);
        audioPlayer = GetComponent<AudioSource>();
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
                    if (beamCounter < 10)
                    {
                        StartCoroutine(shoot());
                        beamCounter++;
                    }
                    else
                    {
                        StartCoroutine(Beam());
                        beamCounter = 0;
                    }

                return true;
            }


        }
        return false;
    }

    IEnumerator shoot()
    {
        isShooting = true;
        int num = Random.Range(0, shootPos.Length - 1);
        Transform shootingspot = shootPos[num];
        Instantiate(bullet, shootPos[0].position, Quaternion.LookRotation(gameManager.instance.player.transform.position + new Vector3(0, test, 0) - shootPos[0].position));
        
        //audioPlayer.PlayOneShot(gunshots[Random.Range(0, gunshots.Length)]);

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
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
            Instantiate(beam, BeamPos.position, Quaternion.LookRotation(gameManager.instance.player.transform.position + new Vector3(0, (i/50f), 0) - BeamPos.position));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(beamRate);
        isShooting = false;
    }

    private void BossDeath()
    {
        isDead = true;
        Destroy(gameObject, deathTimer);


    }

    IEnumerator flashColor()
    {
        color.material.color = Color.green;
        yield return new WaitForSeconds(0.1f);
        color.material.color = colorOrig;
    }
}
