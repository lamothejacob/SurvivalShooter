using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] int totalObjectsNeeded = 3;
    int objectsCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectableObject co = gameManager.instance.player.GetComponentInChildren<CollectableObject>();
            if (co != null)
            {
                Destroy(co.gameObject);
                objectsCollected++;
                if(objectsCollected == totalObjectsNeeded)
                {
                    gameManager.instance.WinState(3);
                }
                else
                {
                    StartCoroutine(FlashText("Collect " + (totalObjectsNeeded - objectsCollected) + " More Engine Parts!"));
                }
            }
        }
    }

    IEnumerator FlashText(string text)
    {
        gameManager.instance.interactText.text = text;

        yield return new WaitForSeconds(2);

        gameManager.instance.interactText.text = null;
    }
}
