using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int timer;

    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

    }
}
