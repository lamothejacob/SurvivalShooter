using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanExplode : MonoBehaviour, IDamage
{
    [SerializeField] int HP;
    [SerializeField] GameObject explosion;


    public void TakeDamage(int amount)
    {
        HP -= amount;
        if (HP <= 0)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public int getHP()
    {
        return HP;
    }
}
