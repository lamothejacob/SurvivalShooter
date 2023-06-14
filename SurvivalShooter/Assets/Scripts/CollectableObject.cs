using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.player.GetComponentInChildren<CollectableObject>() == null)
        {
            transform.parent = gameManager.instance.player.transform;
            transform.localPosition = gameManager.instance.playerScript.getCurrentGun().handOffset - Vector3.right * .5f;
            gameManager.instance.interactText.text = "Return " + gameObject.name + " to Ship!";
            Destroy(gameObject.GetComponent<Collider>());
        }
    }
}
