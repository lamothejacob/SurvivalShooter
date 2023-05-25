using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactGrenade : MonoBehaviour
{
    [SerializeField] GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
