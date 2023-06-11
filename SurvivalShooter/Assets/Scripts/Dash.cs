using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] Collider coll;
    
    int dashPushBack;
    int dashDamage;

    // Start is called before the first frame update
    void Start()
    {
        dashPushBack = gameManager.instance.playerScript.getDashPushBack();
        dashDamage = gameManager.instance.playerScript.getDashDamage();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.playerScript.getDashState() && gameManager.instance.playerScript.isDashUpgraded())
            coll.enabled = true;
        else
            coll.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IPhysics enemy = other.GetComponent<IPhysics>();
            Vector3 direc = other.transform.position - transform.position;
            enemy.takePushBack(direc * dashPushBack, dashDamage);
        }
    }
}
