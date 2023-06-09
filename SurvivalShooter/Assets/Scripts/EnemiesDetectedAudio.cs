using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesDetectedAudio : MonoBehaviour
{

    [SerializeField] AudioSource audioS;
    [SerializeField] AudioClip audioT; 

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            audioS.PlayOneShot(audioT);
        }
    }
}
