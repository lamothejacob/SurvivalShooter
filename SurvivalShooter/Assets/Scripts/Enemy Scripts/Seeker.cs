using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Seeker : Enemy
{
    [Header("Components")]
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] Transform shootPos;

    [Header("Enemy Stats")]
    [SerializeField] float playerFaceSpeed;
    [SerializeField] int ViewCone;
    [SerializeField] float animTransSpeed;

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

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] footsteps;
    [SerializeField] AudioClip[] gunshots;

    Vector3 playerDir;
    bool isShooting;
    bool stepping;
    float angleToPlayer;
    float speedanim;

    private void Start()
    {
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
        agent.radius = Random.Range(avoidRadius - avoidRadiusVariance, avoidRadius + avoidRadiusVariance);
        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isDead)
        {
            speedanim = Mathf.Lerp(speedanim, agent.velocity.normalized.magnitude, Time.deltaTime * animTransSpeed);
            anim.SetFloat("Speed", speedanim);

            agent.SetDestination(gameManager.instance.player.transform.position);

            if (canSeePlayer())
            {

            }
        }
    }

    IEnumerator PlaySteps()
    {
        stepping = true;

        audioPlayer.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);

        yield return new WaitForSeconds(speed / 15f);

        stepping = false;
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
                else if (!stepping)
                {
                    StartCoroutine(PlaySteps());
                }

                if (!isShooting && angleToPlayer <= shootAngle)
                    StartCoroutine(shoot());
                
                return true;
            }


        }
        return false;
    }

    IEnumerator shoot()
    {
        isShooting = true;
        anim.SetTrigger("Shoot");

        Instantiate(bullet, shootPos.position, Quaternion.LookRotation(gameManager.instance.player.transform.position + new Vector3(0, .25f, 0) - shootPos.transform.position));

        audioPlayer.PlayOneShot(gunshots[Random.Range(0, gunshots.Length)]);

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    void facePLayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }


}
