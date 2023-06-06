using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Charger : Enemy
{
    [Header("Components")]
    [SerializeField] AudioSource audioPlayer;

    [Header("Enemy Stats")]
    [SerializeField] float playerFaceSpeed;
    [SerializeField] int ViewCone;
    [SerializeField] float animTransSpeed;

    [Header("Enemy Weapon")]
    [SerializeField] GameObject explosion;

    [Header("----- Nav Mesh Stats -----")]
    [SerializeField] float speed;
    [SerializeField] float speedVariance;
    [SerializeField] float avoidRadius;
    [SerializeField] float avoidRadiusVariance;

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] footsteps;

    Vector3 playerDir;
    bool stepping;
    float angleToPlayer;
    float speedanim;

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
            speedanim = Mathf.Lerp(speedanim, agent.velocity.normalized.magnitude, Time.deltaTime * animTransSpeed);
            anim.SetFloat("Speed", speedanim);

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
                else if (!stepping)
                {
                    StartCoroutine(PlaySteps());
                }
                
                if (agent.remainingDistance <= 3)
                {
                    Explode();
                }
                return true;
            }
        }
        return false;
    }
    void facePLayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    IEnumerator PlaySteps()
    {
        stepping = true;

        audioPlayer.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);

        yield return new WaitForSeconds(speed / 15f);

        stepping = false;
    }

    void Explode()
    {
        Death();
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
