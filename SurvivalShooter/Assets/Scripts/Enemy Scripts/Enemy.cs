using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamage, IPhysics
{
    [Header("Components")]
    [SerializeField] protected Renderer color;
    [SerializeField] protected Transform headPos;
    [SerializeField] protected Animator anim;
    [SerializeField] protected NavMeshAgent agent;

    [Header("Enemy Stats")]
    [SerializeField] protected int HPMax;

    [Header("Drops")]
    [SerializeField] GameObject[] drops;
    [SerializeField] int dropRate;

    protected Color colorOrig;
    protected int HP;
    protected bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        //colorOrig = color.material.color;
        //HP = HPMax;
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

    protected void Death()
    {
        isDead = true;
        gameManager.instance.enemySpawnerScript.EnemyDecrement();
        int spawn = Random.Range(0, dropRate);
        if (spawn < 1)
            Instantiate(drops[Random.Range(0, drops.Length)], transform.position, transform.rotation);


        Destroy(gameObject, 3);
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
}
