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
    [SerializeField] GameObject GoToNextLevel;

    [Header("Enemy Stats")]
    [SerializeField] protected int HPMax;
    [SerializeField] protected int deathTimer;

    [Header("Drops")]
    [SerializeField] GameObject[] drops;

    protected Color colorOrig;
    protected int HP;
    protected bool isDead;
    protected bool isBoss = false;


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
        if (drops.Length > 0)
        {
            int spawn = Random.Range(0, 1000);
            if (spawn % 2 == 0)
                Instantiate(drops[Random.Range(0, drops.Length)], transform.position, transform.rotation);
        }

        if (isBoss)
        {
            GoToNextLevel.gameObject.SetActive(true);
            gameManager.instance.pauseState();
        }

        if (GameObject.FindGameObjectWithTag("EnemySpawner"))
            gameManager.instance.enemySpawnerScript.EnemyDecrement();

        gameManager.instance.SubEnemiesAlive(1);

        Destroy(gameObject, deathTimer);
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

    public int GetHP()
    {
        return HPMax;
    }

    public void SetHP(int newHP)
    {
        HPMax = newHP;
    }
}
