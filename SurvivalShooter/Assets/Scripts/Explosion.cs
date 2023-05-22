using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int pushBack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        IPhysics phys = other.GetComponent<IPhysics>();

        if (phys != null)
        {
            Vector3 direc = other.transform.position - transform.position;
            phys.takePushBack(pushBack * direc);
        }
    }
}
