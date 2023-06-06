using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileExplosion : MonoBehaviour
{
    [SerializeField] Gun gun;
    [SerializeField] GameObject sound;

    private void Start()
    {
        Destroy(Instantiate(sound, transform.position, Quaternion.identity), sound.GetComponent<AudioSource>().clip.length);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamage damagable = other.GetComponent<IDamage>();
        IPhysics physicsable = other.GetComponent<IPhysics>();

        if (physicsable != null)
        {
            physicsable.takePushBack(-transform.forward, gun.damage);
        }else if (damagable != null)
        {
            damagable.TakeDamage(gun.damage);
        }
    }
}
