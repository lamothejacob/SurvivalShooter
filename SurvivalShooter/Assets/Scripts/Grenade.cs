using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] Rigidbody rb;
    [SerializeField] int timer;
    [SerializeField] int speed;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        rb.velocity = Camera.main.transform.forward * speed;

        yield return new WaitForSeconds(timer);

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
