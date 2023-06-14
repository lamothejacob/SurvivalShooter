using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire : MonoBehaviour
{
    [SerializeField] int damagePerSecond;
    bool points;

    public void OnTriggerEnter(Collider other)
    {
        IDamage damageable = other.GetComponent<IDamage>();
        if (damageable != null && transform.parent != other.transform)
        {
            transform.SetParent(other.transform);
            StartCoroutine(Damage(damageable));
            if (other.CompareTag("Enemy")) {
                points = true;
            } else {
                points = false;
            }
        }
    }

    IEnumerator Damage(IDamage damagable)
    {
        float dpt = 1f, damageDealt = 0f;

        while(damageDealt < damagePerSecond)
        {
            damagable.TakeDamage((int)dpt);

            if (points) {
                gameManager.instance.playerScript.addPoints(10);
            }

            damageDealt += dpt;
            yield return new WaitForSeconds(dpt / damagePerSecond);
        }
    }
}
