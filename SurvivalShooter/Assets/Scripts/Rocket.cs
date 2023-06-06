using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] GameObject hitEffect;
    [SerializeField] float speed;

    private void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * speed;

        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            return;
        }

        Destroy(Instantiate(hitEffect, transform.position, Quaternion.identity), hitEffect.GetComponentInChildren<ParticleSystem>().main.duration);

        Destroy(gameObject);
    }
}
