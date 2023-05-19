using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoBoxPickup : MonoBehaviour
{
    [SerializeField] int addAmmoAmount;

    Gun currentGun;

    // Start is called before the first frame update
    void Start()
    {
        currentGun = gameManager.instance.playerScript.getCurrentGun();
        
    }

    private void OnTriggerEnter(Collider other)
    {    
            if(other.CompareTag("Player"))
            {
                if(gameManager.instance.playerScript.hasGun(currentGun))
                {
                    currentGun.addAmmo(addAmmoAmount);
                    Debug.Log("Ammo was added to the current gun");
                    Destroy(gameObject);
                }
                 
            }
    }


}
