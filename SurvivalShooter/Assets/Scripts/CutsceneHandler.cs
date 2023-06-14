using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
struct cutscene {
    [Header("----- Camera Movement -----")]
    public float timeStay;
    public Vector3 position;
    public Quaternion rotation;
    public float timeFadeNext;

    [Header("----- Audio -----")]
    public AudioClip clipToPlay;
    [Range(0f, 1f)]
    public float volume;

    [Header("----- Text -----")]
    public string textToDisplay;
}

public class CutsceneHandler : MonoBehaviour {
    [SerializeField] Camera cutsceneCamera;
    [SerializeField] TextMeshProUGUI tmp;
    [SerializeField] List<cutscene> cutsceneList;
    Queue<cutscene> cutsceneQueue;

    void Start() {
        cutsceneQueue = new Queue<cutscene>(cutsceneList);

        if (cutsceneCamera == null) {
            cutsceneCamera = Camera.main;
        }

        if(cutsceneCamera.GetComponent<AudioSource>() == null) { 
            cutsceneCamera.AddComponent<AudioSource>();
        }

        StartCoroutine(MoveToPosition(0f));
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    IEnumerator MoveToPosition(float time) {
        float elapsedTime = 0f;
        Vector3 position = cutsceneCamera.transform.position;
        Quaternion rotation = cutsceneCamera.transform.rotation;
        cutscene cut = cutsceneQueue.Dequeue();

        while (elapsedTime < time) {
            cutsceneCamera.transform.SetPositionAndRotation(Vector3.Lerp(position, cut.position, elapsedTime / time), Quaternion.Lerp(rotation, cut.rotation, elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cutsceneCamera.transform.SetPositionAndRotation(cut.position, cut.rotation);

        if (cut.clipToPlay != null) {
            AudioSource a = cutsceneCamera.GetComponent<AudioSource>();

            a.volume = cut.volume;
            a.PlayOneShot(cut.clipToPlay);

            yield return null;
        }

        if (tmp != null) {
            tmp.text = cut.textToDisplay;
        }

        yield return new WaitForSeconds(cut.timeStay);

        if (cutsceneQueue.Count > 0) {
            StartCoroutine(MoveToPosition(cut.timeFadeNext));
        }
    }
}