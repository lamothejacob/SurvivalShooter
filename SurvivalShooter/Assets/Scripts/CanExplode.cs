using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanExplode : MonoBehaviour, IDamage, IPhysics
{
    [SerializeField] int HP;
    [SerializeField] GameObject explosion;


    public void TakeDamage(int amount)
    {
        explode(amount);
    }

    public void takePushBack(Vector3 direc, int damage)
    {
        explode(damage);
    }

    void explode(int damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
