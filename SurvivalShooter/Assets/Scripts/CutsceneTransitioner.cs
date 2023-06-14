using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTransitioner : MonoBehaviour
{
    [SerializeField] float waitTime;
    [SerializeField] TextMeshProUGUI textMeshPro;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitContinue());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator WaitContinue() {
        yield return new WaitForSeconds(waitTime);

        textMeshPro.text = "Press Escape to Continue";
    }
}
