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

    public void interact()
    {
        if (gameManager.instance.playerScript.getPoints() >= cost)
        {
            gameManager.instance.playerScript.addPoints(-cost);
            changeDoorState(false);
        }
    }

    void changeDoorState(bool state)
    {
        model.enabled = state;
        coll.enabled = state;
        obstacle.enabled = state;
    }
}
