using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravity : MonoBehaviour
{
    [SerializeField] float modifier;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.LowGravity(modifier);
            gameManager.instance.interactText.color = Color.blue;
            gameManager.instance.interactText.SetText("Low Gravity");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.ResetGravity();
            gameManager.instance.interactText.SetText("");
            gameManager.instance.interactText.color = Color.white;
        }
    }
}
