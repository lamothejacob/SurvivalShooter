using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retriever : Enemy
{

    [Header("Components")]
    [SerializeField] AudioSource audioPlayer;
    [SerializeField] Transform shootPos;

    [Header("Enemy Stats")]
    [SerializeField] float playerFaceSpeed;
    [SerializeField] int ViewCone;
    [SerializeField] float animTransSpeed;
    [SerializeField] int distanceFromPlayer;

    [Header("----- Enemy Weapon -----")]
    [Range(2, 300)][SerializeField] int shootDist;
    [Range(0.1f, 3)][SerializeField] float shootRate;
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

    [Header("----- Goal Item -----")]
    [SerializeField] GameObject goalItem;

    Vector3 playerDir;
    bool isShooting;
    bool stepping;
    float angleToPlayer;
    float speedanim;
    bool hasGoalItem;
    float stopDistOrig;

    private void Start()
    {
        HP = HPMax;
        colorOrig = color.material.color;
        agent.speed = Random.Range(speed - speedVariance, speed + speedVariance);
        agent.radius = Random.Range(avoidRadius - avoidRadiusVariance, avoidRadius + avoidRadiusVariance);
        audioPlayer = GetComponent<AudioSource>();
        stopDistOrig = agent.stoppingDistance;
    }

    void Update()
    {
        if (!isDead)
        {
            speedanim = Mathf.Lerp(speedanim, agent.velocity.normalized.magnitude, Time.deltaTime * animTransSpeed);
            anim.SetFloat("Speed", speedanim);

            if (GameObject.Find("Goal Item") && !hasGoalItem)
            {
                agent.stoppingDistance = 0;
                agent.SetDestination(GameObject.Find("Goal Item").transform.position);
            }
            else
            {
                agent.stoppingDistance = 1;
                AvoidPlayer();
            }
        }
        else if (hasGoalItem)
        {
            Instantiate(goalItem, transform.position, transform.rotation);
            hasGoalItem = false;
        }
    }

    void AvoidPlayer()
    {
        if (Vector3.Distance(gameManager.instance.player.transform.position, headPos.position) < distanceFromPlayer)
        {
            agent.SetDestination((gameManager.instance.player.transform.position - headPos.position).normalized * -distanceFromPlayer);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {

            }
            else if (!stepping)
            {
                StartCoroutine(PlaySteps());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Goal Item"))
        {
            Destroy(other.gameObject);
            hasGoalItem = true;
        }
    }
}
