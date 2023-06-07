using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

            if (SceneManager.GetActiveScene().name == "Boss")
            {
                GoToNextLevel.gameObject.SetActive(true);
                gameManager.instance.pauseState();
            }
            else
            {
                gameManager.instance.WinState(0);
            }
        }
    }
}
