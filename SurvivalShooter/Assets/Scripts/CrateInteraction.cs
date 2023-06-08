using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateInteraction : MonoBehaviour
{
    [SerializeField] GameObject crateBuyMenu;
    [SerializeField] GameObject InteractText;

    public bool bPlayerInRange; 
    // Start is called before the first frame update
    void Start()
    {
        bPlayerInRange = false;
        InteractText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(bPlayerInRange)
            InteractText.SetActive(true);
        
        if(!bPlayerInRange)
            InteractText.SetActive(false);

        if(bPlayerInRange && Input.GetButtonDown("Interact"))
        {
           crateBuyMenu.SetActive(true);
           gameManager.instance.pauseState();
           gameManager.instance.activeMenu = crateBuyMenu;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            bPlayerInRange = true;
            InteractText.SetActive(true);

            Debug.Log("The Player is in Range of the crate");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            bPlayerInRange=false;
            InteractText.SetActive(false);

            Debug.Log("The Player is out of range of the crate"); 
        }
    }
}
