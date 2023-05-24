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
    [SerializeField] int curve;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        rb.velocity = (Camera.main.transform.forward * speed);
        rb.velocity += new Vector3(0, curve, 0);

        yield return new WaitForSeconds(timer);

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
