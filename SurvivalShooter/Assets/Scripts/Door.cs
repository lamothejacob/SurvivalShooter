using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] int cost;

    [Header("----------")]
    [SerializeField] Renderer model;
    [SerializeField] Collider coll;
    [SerializeField] NavMeshObstacle obstacle;
    [SerializeField] Collider interactTrigger;

    string interactText;

    public void interact()
    {
        if (gameManager.instance.playerScript.getPoints() >= cost)
        {
            gameManager.instance.playerScript.addPoints(-cost);
            changeDoorState(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText = "Press 'E' to open\n Cost: " + cost.ToString();
            gameManager.instance.interactText.text = interactText;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.interactText.text = null;
        }
    }

    void changeDoorState(bool state)
    {
        model.enabled = state;
        coll.enabled = state;
        obstacle.enabled = state;
        interactTrigger.enabled = state;
    }
}
