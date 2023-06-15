using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayObjective : MonoBehaviour
{

    [SerializeField] GameObject objectiveText;

    private void OnTriggerEnter(Collider other)
    {
           objectiveText.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        objectiveText.SetActive(false);
    }
}
