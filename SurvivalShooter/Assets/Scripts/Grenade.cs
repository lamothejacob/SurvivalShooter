using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] protected GameObject explosion;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected int timer;
    [SerializeField] protected int speed;
    [SerializeField] protected int curve;

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
