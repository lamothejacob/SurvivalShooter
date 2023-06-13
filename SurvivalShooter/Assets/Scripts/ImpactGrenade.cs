using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactGrenade : Grenade
{

    IEnumerator Start()
    {
        rb.velocity = (Camera.main.transform.forward * speed);
        rb.velocity += new Vector3(0, curve, 0);

        yield return new WaitForSeconds(timer);

        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
