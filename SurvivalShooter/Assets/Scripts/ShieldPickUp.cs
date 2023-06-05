using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPickUp : MonoBehaviour
{
    [SerializeField] int addShieldAmount;


    bool isInteracting;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.addShield(addShieldAmount);
            if (!isInteracting)
                StartCoroutine(interactText());
        }
    }

    IEnumerator interactText()
    {
        isInteracting = true;
        gameManager.instance.interactText.text = '+' + addShieldAmount.ToString() + " Shield";

        yield return new WaitForSeconds(1f);

        gameManager.instance.interactText.text = null;
        isInteracting = false;

        Destroy(gameObject);
    }
}
