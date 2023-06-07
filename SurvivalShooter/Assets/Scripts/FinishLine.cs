using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLine : MonoBehaviour
{
    [SerializeField] GameObject GoToNextLevel;

    private bool mFinished; 
    // Start is called before the first frame update
    void Start()
    {
        mFinished = false; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("Player has passed the level. ");
            mFinished = true;

            //GoToNextLevel.gameObject.SetActive(true);
            gameManager.instance.WinState(0);
            gameManager.instance.pauseState();     
        }
    }
}
