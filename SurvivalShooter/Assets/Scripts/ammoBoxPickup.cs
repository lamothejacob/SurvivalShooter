using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoBoxPickup : MonoBehaviour
{
    [SerializeField] int addAmmoAmount;

    Gun currentGun;

    bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        currentGun = gameManager.instance.playerScript.getCurrentGun();
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.getCurrentGun().addAmmo(addAmmoAmount);
            if (!isInteracting)
                StartCoroutine(interactText());
        }
    }

    IEnumerator interactText()
    {
        isInteracting = true;
        gameManager.instance.interactText.text = '+' + addAmmoAmount.ToString() + " Ammo";

        yield return new WaitForSeconds(1f);

        gameManager.instance.interactText.text = null;
        isInteracting = false;

        Destroy(gameObject);
    }
}
