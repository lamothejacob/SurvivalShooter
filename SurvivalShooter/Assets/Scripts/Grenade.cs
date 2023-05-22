using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] int timer;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    IEnumerator Start()
    {

        yield return new WaitForSeconds(timer);

        Instantiate(explosion);
        Destroy(gameObject);

    }
}
