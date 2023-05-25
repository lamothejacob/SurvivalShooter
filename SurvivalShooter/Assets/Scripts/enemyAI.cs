using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class enemyAI : MonoBehaviour, IDamage, IPhysics
{
    [Header("----- Components -----")]
    [SerializeField] Renderer color;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Animator anim;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;
    [SerializeField] AudioSource audioPlayer;

    [Header("----- Enemy Stats -----")]
    int HP;
    [SerializeField] float HPMod;
    [SerializeField] float playerFaceSpeed;
    [SerializeField] int ViewCone;
    [SerializeField] float animTransSpeed;

    [Header("----- Enemy Weapon -----")]
    [Range(2, 300)][SerializeField] int shootDist;
    [Range(0.1f, 3)][SerializeField] float shootRate;
    [SerializeField] GameObject bullet;
    [SerializeField] int shootAngle;
    [SerializeField] GameObject explosion;

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
    bool isDead;
    bool stepping;
    Color colorOrig;
    float angleToPlayer;
    float speedanim;

    private void Start()
    {
        colorOrig = color.material.color;
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
        agent.radius = Random.Range(avoidRadius - avoidRadiusVariance, avoidRadius + avoidRadiusVariance);
        HP = (int)(gameManager.instance.enemySpawnerScript.GetCurrentWave().getHealth() * HPMod);

        audioPlayer = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isDead)
        {
            speedanim = Mathf.Lerp(speedanim, agent.velocity.normalized.magnitude, Time.deltaTime * animTransSpeed);
            anim.SetFloat("Speed", speedanim);

            agent.SetDestination(gameManager.instance.player.transform.position);

            if (canSeePlayer()) { 
            
            }
        }
    }

    IEnumerator PlaySteps()
    {
        stepping = true;

        audioPlayer.PlayOneShot(footsteps[Random.Range(0, footsteps.Length)]);

        yield return new WaitForSeconds(speed/15f);

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
                }else if (!stepping)
                {
                    StartCoroutine(PlaySteps());
                }

                if (shootPos != null && bullet != null)
                {
                    if (!isShooting && angleToPlayer <= shootAngle)
                        StartCoroutine(shoot());
                }
                else if (agent.remainingDistance <= 3)
                {
                    Explode();
                    Death();
                }
                return true;
            }
        }
        return false;
    }

    private void Death()
    {
        isDead = true;
        gameManager.instance.enemySpawnerScript.EnemyDecrement();

        if (shootPos == null && bullet == null)
        {
            Explode();
            Destroy(gameObject);
        }

        Destroy(gameObject, 3);
    }

    IEnumerator shoot()
    {
        isShooting = true;
        anim.SetTrigger("Shoot");

        Instantiate(bullet, shootPos.position, Quaternion.LookRotation(gameManager.instance.player.transform.position + new Vector3(0,.25f,0) - shootPos.transform.position));

        audioPlayer.PlayOneShot(gunshots[Random.Range(0, gunshots.Length)]);

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
        if (isDead) return;

        HP -= damage;

        if (HP <= 0)
        {
            StopAllCoroutines();
            anim.SetBool("Dead", true);
            agent.enabled = false;
            GetComponent<Collider>().enabled = false;
            Death();
        }
        else
        {
            StartCoroutine(flashColor());
        }
    }
    IEnumerator flashColor()
    {
        color.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        color.material.color = colorOrig;
    }

    public void takePushBack(Vector3 direc, int damage)
    {
        agent.velocity += direc;
        TakeDamage(damage);
    }

    void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
